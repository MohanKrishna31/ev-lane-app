using nApps.Futs.Mobile.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Settings;

public class SettingsViewModel : BaseViewModel
{
    private readonly ISettingsService _settingsService;
    private readonly nApps.Futs.Mobile.Shared.Services.Storage.IStorageService _storageService;

    public SettingsViewModel(
        ISettingsService settingsService,
        nApps.Futs.Mobile.Shared.Services.Storage.IStorageService storageService)
    {
        _settingsService = settingsService;
        _storageService = storageService;
    }
    public bool PushNotificationsEnabled { get; set; } = true;

    public bool ChargingNotificationsEnabled { get; set; } = true;

    public bool WalletNotificationsEnabled { get; set; } = true;

    public bool PromotionalNotificationsEnabled { get; set; } = false;

    public bool BiometricLoginEnabled { get; set; }

    public string PreferredLanguage { get; set; } = "English";
    public async Task LoadAsync()
    {
        BiometricLoginEnabled = _storageService.GetPreference(
            nApps.Futs.Mobile.Shared.Constants.StorageKeys.BiometricLoginEnabled,
            false);
        OnPropertyChanged(nameof(BiometricLoginEnabled));

        await ExecuteAsync(async () =>
        {
            var settings = await _settingsService.GetAsync();

            if (settings == null)
                return;

            PreferredLanguage = settings.PreferredLanguage;
            PushNotificationsEnabled = settings.PushNotificationsEnabled;
            ChargingNotificationsEnabled = settings.ChargingNotificationsEnabled;
            WalletNotificationsEnabled = settings.WalletNotificationsEnabled;
            PromotionalNotificationsEnabled = settings.PromotionalNotificationsEnabled;

            OnPropertyChanged(nameof(PreferredLanguage));
            OnPropertyChanged(nameof(PushNotificationsEnabled));
            OnPropertyChanged(nameof(ChargingNotificationsEnabled));
            OnPropertyChanged(nameof(WalletNotificationsEnabled));
            OnPropertyChanged(nameof(PromotionalNotificationsEnabled));
        });
    }
    public async Task SaveAsync()
    {
        await ExecuteAsync(async () =>
        {
            var request = new UpdateSettingsRequest
            {
                PreferredLanguage = PreferredLanguage,
                PushNotificationsEnabled = PushNotificationsEnabled,
                ChargingNotificationsEnabled = ChargingNotificationsEnabled,
                WalletNotificationsEnabled = WalletNotificationsEnabled,
                PromotionalNotificationsEnabled = PromotionalNotificationsEnabled
            };

            var settings = await _settingsService.UpdateAsync(request);

            if (settings == null)
                return;

            PreferredLanguage = settings.PreferredLanguage;
            PushNotificationsEnabled = settings.PushNotificationsEnabled;
            ChargingNotificationsEnabled = settings.ChargingNotificationsEnabled;
            WalletNotificationsEnabled = settings.WalletNotificationsEnabled;
            PromotionalNotificationsEnabled = settings.PromotionalNotificationsEnabled;

            OnPropertyChanged(nameof(PreferredLanguage));
            OnPropertyChanged(nameof(PushNotificationsEnabled));
            OnPropertyChanged(nameof(ChargingNotificationsEnabled));
            OnPropertyChanged(nameof(WalletNotificationsEnabled));
            OnPropertyChanged(nameof(PromotionalNotificationsEnabled));
        });

        _storageService.SetPreference(
            nApps.Futs.Mobile.Shared.Constants.StorageKeys.BiometricLoginEnabled,
            BiometricLoginEnabled);
    }
}
