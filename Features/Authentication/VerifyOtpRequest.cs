using nApps.Futs.Mobile.Features.Authentication.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Authentication;

public class VerifyOtpRequest
{
    public string GrantType { get; set; } = AuthenticationConstants.GrantType;

    public string ClientId { get; set; } = string.Empty;

    public string MobileNumber { get; set; } = string.Empty;

    public string Otp { get; set; } = string.Empty;

    public string Scope { get; set; } = string.Empty;
}