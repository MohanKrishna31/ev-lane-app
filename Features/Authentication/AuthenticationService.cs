using nApps.Futs.Mobile.Features.Authentication.Constants;
using nApps.Futs.Mobile.Shared.Constants;
using nApps.Futs.Mobile.Shared.Services.Api;
using nApps.Futs.Mobile.Shared.Services.Storage;
using nApps.Futs.Mobile.Shared.State;
using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IApiService _apiService;
    private readonly IStorageService _storageService;
    private readonly AuthenticationState _authenticationState;

    public AuthenticationService(
        IApiService apiService,
        IStorageService storageService,
        AuthenticationState authenticationState)
    {
        _apiService = apiService;
        _storageService = storageService;
        _authenticationState = authenticationState;
    }

    public async Task SendOtpAsync(SendOtpRequest request)
    {
        await _apiService.PostAsync<SendOtpRequest, object>(ApiRoutes.Auth.SendOtp,request);
    }

    public async Task<TokenResponse?> VerifyOtpAsync(VerifyOtpRequest request)
    {
        var form = new Dictionary<string, string>
        {
            ["grant_type"] = request.GrantType,
            ["client_id"] = request.ClientId,
            ["scope"] = request.Scope,
            [AuthenticationConstants.MobileNumber] = request.MobileNumber,
            [AuthenticationConstants.Otp] = request.Otp
        };

        var token = await _apiService.PostFormAsync<TokenResponse>(ApiRoutes.Auth.VerifyOtp,form);

        if (token is null)
            return null;

        await SaveTokenAsync(token);

        return token;
    }

    public async Task<TokenResponse?> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var form = new Dictionary<string, string>
        {
            ["grant_type"] = AuthenticationConstants.RefreshTokenGrant,
            ["client_id"] = request.ClientId,
            ["refresh_token"] = request.RefreshToken
        };

        var token = await _apiService.PostFormAsync<TokenResponse>(
            ApiRoutes.Auth.VerifyOtp,
            form);

        if (token is null)
            return null;

        await SaveTokenAsync(token);

        return token;
    }

    public async Task LogoutAsync()
    {
        try
        {
            await _apiService.PostAsync(ApiRoutes.Auth.Logout);
        }
        finally
        {
            await _storageService.RemoveSecureAsync(StorageKeys.AccessToken);
            await _storageService.RemoveSecureAsync(StorageKeys.RefreshToken);
            await _storageService.RemoveSecureAsync(StorageKeys.UserId);
            await _storageService.RemoveSecureAsync(StorageKeys.TenantId);
            await _authenticationState.SignOutAsync();
        }
    }

    private async Task SaveTokenAsync(TokenResponse token)
    {
        await _storageService.SetSecureAsync(StorageKeys.AccessToken, token.AccessToken);

        if (!string.IsNullOrWhiteSpace(token.RefreshToken))
        {
            await _storageService.SetSecureAsync(StorageKeys.RefreshToken, token.RefreshToken);
        }

        await _authenticationState.SignInAsync();
    }
}
