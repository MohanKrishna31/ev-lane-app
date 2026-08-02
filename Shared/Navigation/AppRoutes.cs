using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Shared.Navigation;

public static class AppRoutes
{
    public const string Splash = "/";

    public const string Login = "/login";

    public const string Dashboard = "/dashboard";

    public const string Vehicles = "/vehicles";

    public const string VehicleCreate = "/vehicles/new";

    public static string VehicleEdit(Guid id) => $"/vehicles/{id}/edit";

    public const string Stations = "/stations";

    public static string StationDetails(Guid id) => $"/stations/{id}";

    public const string Charging = "/charging";

    public const string Wallet = "/wallet";

    public const string Notifications = "/notifications";

    public const string Profile = "/profile";

    public const string Sessions = "/sessions";

    public static string SessionDetails(Guid id) => $"/sessions/{id}";

    public const string Settings = "/settings";

    public const string SettingsLanguage = "/settings/language";

    public const string PrivacyPolicy = "/settings/privacy-policy";

    public const string TermsAndConditions = "/settings/terms-and-conditions";

    public const string ContactUs = "/settings/contact-us";

    public const string About = "/settings/about";
}
