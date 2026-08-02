using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Settings;

public class UpdateSettingsRequest
{
    public string PreferredLanguage { get; set; } = "English";

    public bool PushNotificationsEnabled { get; set; }

    public bool ChargingNotificationsEnabled { get; set; }

    public bool WalletNotificationsEnabled { get; set; }

    public bool PromotionalNotificationsEnabled { get; set; }
}
