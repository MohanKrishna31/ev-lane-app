using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Shared.Services.Storage;

public class StorageService : IStorageService
{
    public async Task SetSecureAsync(string key, string value)
    {
        await SecureStorage.Default.SetAsync(key, value);
    }

    public async Task<string?> GetSecureAsync(string key)
    {
        return await SecureStorage.Default.GetAsync(key);
    }

    public Task RemoveSecureAsync(string key)
    {
        SecureStorage.Default.Remove(key);
        return Task.CompletedTask;
    }

    public Task RemoveAllSecureAsync()
    {
        SecureStorage.Default.RemoveAll();
        return Task.CompletedTask;
    }

    public void SetPreference<T>(string key, T value)
    {
        Preferences.Default.Set(key, value);
    }

    public T GetPreference<T>(string key, T defaultValue)
    {
        return Preferences.Default.Get(key, defaultValue);
    }

    public void RemovePreference(string key)
    {
        Preferences.Default.Remove(key);
    }

    public void ClearPreferences()
    {
        Preferences.Default.Clear();
    }
}
