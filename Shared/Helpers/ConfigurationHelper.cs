using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Shared.Helpers;

public static class ConfigurationHelper
{
    public static IConfiguration LoadConfiguration()
    {
        var assembly = typeof(ConfigurationHelper).Assembly;

        using var stream = assembly.GetManifestResourceStream(
            "nApps.Futs.Mobile.appsettings.json");

        if (stream == null)
            throw new FileNotFoundException("appsettings.json not found.");

        return new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();
    }
}