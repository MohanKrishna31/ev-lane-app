using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Shared.Configuration;

public class AppSettings
{
    public const string SectionName = "App";

    public string Name { get; set; } = string.Empty;

    public string Version { get; set; } = string.Empty;
}
