using nApps.Futs.Mobile.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Settings;

public class SettingsViewModel : BaseViewModel
{
    public bool PushNotificationsEnabled { get; set; } = true;

    public bool ChargingNotificationsEnabled { get; set; } = true;

    public bool WalletNotificationsEnabled { get; set; } = true;

    public bool PromotionalNotificationsEnabled { get; set; } = false;

    public bool BiometricLoginEnabled { get; set; }

    public string PreferredLanguage { get; set; } = "English";
}
