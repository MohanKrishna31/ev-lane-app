using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Shared.Services.Storage;

public interface IStorageService
{
    Task SetSecureAsync(string key, string value);

    Task<string?> GetSecureAsync(string key);

    Task RemoveSecureAsync(string key);

    Task RemoveAllSecureAsync();

    void SetPreference<T>(string key, T value);

    T GetPreference<T>(string key, T defaultValue);

    void RemovePreference(string key);

    void ClearPreferences();
}
