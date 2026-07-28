using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Authentication;

public class RefreshTokenRequest
{
    public string GrantType { get; set; } = "refresh_token";

    public string ClientId { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;
}
