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

        public const string Logout = "/connect/revocation";
    }

    public static class Customer
    {
        public const string Profile = "/api/app/customer/me";
    }

    public static class Vehicle
    {
        public const string List = "/api/app/vehicle";

        public const string Create = "/api/app/vehicle";

        public const string Update = "/api/app/vehicle";

        public const string Delete = "/api/app/vehicle";
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