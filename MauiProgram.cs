using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using nApps.Futs.Mobile.Features.Authentication;
using nApps.Futs.Mobile.Features.Authentication.ViewModels;
using nApps.Futs.Mobile.Features.Customer;
using nApps.Futs.Mobile.Features.Settings;
using nApps.Futs.Mobile.Features.Stations;
using nApps.Futs.Mobile.Features.Sessions;
using nApps.Futs.Mobile.Features.Wallet;
using nApps.Futs.Mobile.Features.Notifications;
using nApps.Futs.Mobile.Features.Dashboard;
using nApps.Futs.Mobile.Features.Splash.ViewModels;
using nApps.Futs.Mobile.Features.Vehicles;
using nApps.Futs.Mobile.Shared.Configuration;
using nApps.Futs.Mobile.Shared.Helpers;
using nApps.Futs.Mobile.Shared.Http;
using nApps.Futs.Mobile.Shared.Media;
using nApps.Futs.Mobile.Shared.Location;
using nApps.Futs.Mobile.Shared.Navigation;
using nApps.Futs.Mobile.Shared.Services.Api;
using nApps.Futs.Mobile.Shared.Services.Storage;
using nApps.Futs.Mobile.Shared.Services.SignalR;
using nApps.Futs.Mobile.Shared.State;
using System.Net.Http.Headers;

namespace nApps.Futs.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        var configuration = ConfigurationHelper.LoadConfiguration();

        builder.Services.AddSingleton<IConfiguration>(configuration);

        builder.Services.Configure<AppSettings>(configuration.GetSection(AppSettings.SectionName));

        builder.Services.Configure<ApiSettings>(configuration.GetSection(ApiSettings.SectionName));

        builder.Services.Configure<AuthenticationSettings>(configuration.GetSection(AuthenticationSettings.SectionName));

        builder.Services.Configure<SignalRSettings>(configuration.GetSection(SignalRSettings.SectionName));

        builder.Services.Configure<GoogleMapsSettings>(configuration.GetSection(GoogleMapsSettings.SectionName));

        builder.Services.AddSingleton<IStorageService, StorageService>();

        builder.Services.AddTransient<AuthorizationHandler>();

        builder.Services.AddHttpClient<IApiService, ApiService>((serviceProvider, client) =>
        {
            var apiSettings = serviceProvider.GetRequiredService<IOptions<ApiSettings>>().Value;
            client.BaseAddress = new Uri(apiSettings.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(60);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }).AddHttpMessageHandler<AuthorizationHandler>();

        builder.Services.AddHttpClient<IFileUploadService, FileUploadService>((serviceProvider, client) =>
        {
            var apiSettings = serviceProvider
                .GetRequiredService<IOptions<ApiSettings>>()
                .Value;

            client.BaseAddress = new Uri(apiSettings.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(60);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }).AddHttpMessageHandler<AuthorizationHandler>();

        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

        builder.Services.AddSingleton<AuthenticationState>();

        builder.Services.AddScoped<LoginViewModel>();

        builder.Services.AddScoped<SplashViewModel>();

        builder.Services.AddScoped<CustomerViewModel>();

        builder.Services.AddScoped<EditProfileViewModel>();

        builder.Services.AddScoped<SettingsViewModel>();

        builder.Services.AddScoped<ICustomerService, CustomerService>();

        builder.Services.AddScoped<ISettingsService, SettingsService>();

        builder.Services.AddScoped<IVehicleService, VehicleService>();

        builder.Services.AddScoped<VehiclesViewModel>();

        builder.Services.AddScoped<VehicleEditViewModel>();

        builder.Services.AddSingleton<IDeviceLocationService, DeviceLocationService>();

        builder.Services.AddScoped<IStationService, StationService>();

        builder.Services.AddScoped<StationsViewModel>();

        builder.Services.AddScoped<StationDetailsViewModel>();

        builder.Services.AddScoped<ISessionService, SessionService>();

        builder.Services.AddScoped<SessionsViewModel>();

        builder.Services.AddScoped<SessionDetailsViewModel>();

        builder.Services.AddScoped<IWalletService, WalletService>();

        builder.Services.AddScoped<WalletViewModel>();

        builder.Services.AddScoped<INotificationService, NotificationService>();

        builder.Services.AddScoped<NotificationRealtimeService>();

        builder.Services.AddScoped<NotificationsViewModel>();

        builder.Services.AddScoped<DashboardViewModel>();

        builder.Services.AddSingleton<IMediaPickerService, MediaPickerService>();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddMauiBlazorWebView();

        
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        // Register back navigation service for centralized back handling
        builder.Services.AddSingleton<nApps.Futs.Mobile.Shared.Navigation.IBackNavigationService, nApps.Futs.Mobile.Shared.Navigation.BackNavigationService>();

        return builder.Build();
    }
}
