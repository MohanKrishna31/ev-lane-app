using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using nApps.Futs.Mobile.Features.Authentication;
using nApps.Futs.Mobile.Features.Authentication.ViewModels;
using nApps.Futs.Mobile.Features.Customer;
using nApps.Futs.Mobile.Features.Settings;
using nApps.Futs.Mobile.Features.Splash.ViewModels;
using nApps.Futs.Mobile.Shared.Configuration;
using nApps.Futs.Mobile.Shared.Helpers;
using nApps.Futs.Mobile.Shared.Http;
using nApps.Futs.Mobile.Shared.Media;
using nApps.Futs.Mobile.Shared.Navigation;
using nApps.Futs.Mobile.Shared.Services.Api;
using nApps.Futs.Mobile.Shared.Services.Storage;
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

        return builder.Build();
    }
}
