using nApps.Futs.Mobile.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Settings;

public class SettingsViewModel : BaseViewModel
{
    private readonly ISettingsService _settingsService;

    public SettingsViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;
    }
    public bool PushNotificationsEnabled { get; set; } = true;

    public bool ChargingNotificationsEnabled { get; set; } = true;

    public bool WalletNotificationsEnabled { get; set; } = true;

    public bool PromotionalNotificationsEnabled { get; set; } = false;

    public bool BiometricLoginEnabled { get; set; }

    public string PreferredLanguage { get; set; } = "English";
    public async Task LoadAsync()
    {
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
            BiometricLoginEnabled = settings.BiometricLoginEnabled;

            OnPropertyChanged(nameof(PreferredLanguage));
            OnPropertyChanged(nameof(PushNotificationsEnabled));
            OnPropertyChanged(nameof(ChargingNotificationsEnabled));
            OnPropertyChanged(nameof(WalletNotificationsEnabled));
            OnPropertyChanged(nameof(PromotionalNotificationsEnabled));
            OnPropertyChanged(nameof(BiometricLoginEnabled));
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
                PromotionalNotificationsEnabled = PromotionalNotificationsEnabled,
                BiometricLoginEnabled = BiometricLoginEnabled
            };

            var settings = await _settingsService.UpdateAsync(request);

            if (settings == null)
                return;

            PreferredLanguage = settings.PreferredLanguage;
            PushNotificationsEnabled = settings.PushNotificationsEnabled;
            ChargingNotificationsEnabled = settings.ChargingNotificationsEnabled;
            WalletNotificationsEnabled = settings.WalletNotificationsEnabled;
            PromotionalNotificationsEnabled = settings.PromotionalNotificationsEnabled;
            BiometricLoginEnabled = settings.BiometricLoginEnabled;

            OnPropertyChanged(nameof(PreferredLanguage));
            OnPropertyChanged(nameof(PushNotificationsEnabled));
            OnPropertyChanged(nameof(ChargingNotificationsEnabled));
            OnPropertyChanged(nameof(WalletNotificationsEnabled));
            OnPropertyChanged(nameof(PromotionalNotificationsEnabled));
            OnPropertyChanged(nameof(BiometricLoginEnabled));
        });
    }
}
