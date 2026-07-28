using nApps.Futs.Mobile.Features.Authentication.Constants;
using nApps.Futs.Mobile.Shared.Constants;
using nApps.Futs.Mobile.Shared.Services.Api;
using nApps.Futs.Mobile.Shared.Services.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IApiService _apiService;
    private readonly IStorageService _storageService;

    public AuthenticationService(IApiService apiService,IStorageService storageService)
    {
        _apiService = apiService;
        _storageService = storageService;
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

        await _storageService.SetSecureAsync(StorageKeys.AccessToken,token.AccessToken);

        await _storageService.SetSecureAsync(StorageKeys.RefreshToken,token.RefreshToken);

        return token;
    }

    public async Task<TokenResponse?> RefreshTokenAsync(RefreshTokenRequest request)
    {
        // Implementation later
        throw new NotImplementedException();
    }

    public async Task LogoutAsync()
    {
        await _storageService.RemoveSecureAsync(StorageKeys.AccessToken);

        await _storageService.RemoveSecureAsync(StorageKeys.RefreshToken);

        await _storageService.RemoveSecureAsync(StorageKeys.UserId);

        await _storageService.RemoveSecureAsync(StorageKeys.TenantId);
    }
}