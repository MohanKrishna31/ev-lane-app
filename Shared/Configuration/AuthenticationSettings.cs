using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Shared.Configuration;

public class AuthenticationSettings
{
    public const string SectionName = "Authentication";

    public string ClientId { get; set; } = string.Empty;

    public string Scope { get; set; } = string.Empty;
}
