using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Authentication;

public interface IAuthenticationService
{
    Task SendOtpAsync(SendOtpRequest request);

    Task<TokenResponse?> VerifyOtpAsync(VerifyOtpRequest request);

    Task<TokenResponse?> RefreshTokenAsync(RefreshTokenRequest request);

    Task LogoutAsync();
}