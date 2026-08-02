using nApps.Futs.Mobile.Shared.Services.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Settings;

public class SettingsService : ISettingsService
{
    private readonly IApiService _apiService;

    public SettingsService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<SettingsDto?> GetAsync()
    {
        return await _apiService.GetAsync<SettingsDto>(
            nApps.Futs.Mobile.Shared.Constants.ApiRoutes.Customer.Settings);
    }

    public async Task<SettingsDto?> UpdateAsync(UpdateSettingsRequest request)
    {
        return await _apiService.PutAsync<UpdateSettingsRequest, SettingsDto>(
            nApps.Futs.Mobile.Shared.Constants.ApiRoutes.Customer.Settings,
            request);
    }
}
