using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Shared.Constants;

public static class ApiRoutes
{
    public static class Auth
    {
        public const string SendOtp = "/api/app/mobile-auth/send-otp";

        public const string VerifyOtp = "/connect/token";

        public const string Logout = "/api/app/mobile-auth/logout";
    }

    public static class Customer
    {
        public const string Profile = "/api/app/customer/profile";

        public const string ProfilePhoto = "/api/app/customer/profile-photo";

        public const string Settings = "/api/app/customer/settings";

        public const string Account = "/api/app/customer/account";
    }

    public static class Vehicle
    {
        public const string Base = "/api/app/customer-vehicle";

        public const string MyVehicles = Base + "/my-vehicles";

        public const string ActiveManufacturers = "/api/app/manufacturer/active-list";

        public static string ById(Guid id) => $"{Base}/{id}";

        public static string SetDefault(Guid id) => $"{Base}/{id}/set-default";

        public static string ModelsByManufacturer(Guid manufacturerId) =>
            $"/api/app/vehicle-model/by-manufacturer/{manufacturerId}";

        public static string VariantsByModel(Guid vehicleModelId) =>
            $"/api/app/vehicle-variant/by-model/{vehicleModelId}";
    }

    public static class Station
    {
        public const string Nearby = "/api/app/station/nearby";

        public const string Details = "/api/app/station";
    }

    public static class Charging
    {
        public const string Start = "/api/app/charging/start";

        public const string Stop = "/api/app/charging/stop";

        public const string Session = "/api/app/charging/session";
    }

    public static class Wallet
    {
        public const string Balance = "/api/app/wallet/balance";

        public const string Transactions = "/api/app/wallet/transactions";
    }

    public static class Notification
    {
        public const string List = "/api/app/notification";

        public const string MarkRead = "/api/app/notification/read";
    }
}
