using Microsoft.AspNetCore.Components;
using nApps.Futs.Mobile.Shared.Constants;
using nApps.Futs.Mobile.Shared.Navigation;
using nApps.Futs.Mobile.Shared.Services.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Splash.ViewModels;

public class SplashViewModel
{
    private readonly IStorageService _storageService;

    public SplashViewModel(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await _storageService.GetSecureAsync(StorageKeys.AccessToken);

        return !string.IsNullOrWhiteSpace(token);
    }
}