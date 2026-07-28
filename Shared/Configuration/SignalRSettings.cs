using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Shared.Configuration;

public class SignalRSettings
{
    public const string SectionName = "SignalR";

    public string HubUrl { get; set; } = string.Empty;
}
