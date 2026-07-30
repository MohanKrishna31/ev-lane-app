using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Settings;

public interface ISettingsService
{
    Task<SettingsDto?> GetAsync();

    Task<SettingsDto?> UpdateAsync(UpdateSettingsRequest request);
}