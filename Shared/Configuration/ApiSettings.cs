using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Shared.Configuration;

public class ApiSettings
{
    public const string SectionName = "Api";

    public string BaseUrl { get; set; } = string.Empty;
}
