using nApps.Futs.Mobile.Shared.Constants;
using nApps.Futs.Mobile.Shared.Services.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Shared.State;

public class AuthenticationState
{
    private readonly IStorageService _storageService;

    public AuthenticationState(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public bool IsAuthenticated { get; private set; }

    public event Action? AuthenticationStateChanged;

    public async Task InitializeAsync()
    {
        var token = await _storageService.GetSecureAsync(StorageKeys.AccessToken);

        IsAuthenticated = !string.IsNullOrWhiteSpace(token);

        AuthenticationStateChanged?.Invoke();
    }

    public async Task SignInAsync()
    {
        IsAuthenticated = true;

        AuthenticationStateChanged?.Invoke();

        await Task.CompletedTask;
    }

    public async Task SignOutAsync()
    {
        IsAuthenticated = false;

        AuthenticationStateChanged?.Invoke();

        await Task.CompletedTask;
    }
}
