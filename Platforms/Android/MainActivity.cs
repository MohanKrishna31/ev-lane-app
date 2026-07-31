using Android.App;
using Android.Content.PM;
using Android.OS;
using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

// for service resolution
using Microsoft.Extensions.DependencyInjection;

namespace nApps.Futs.Mobile;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    public override void OnBackPressed()
    {
        try
        {
            var services = Microsoft.Maui.Controls.Application.Current?.Handler?.MauiContext?.Services;
            if (services != null)
            {
                var backService = services.GetService(typeof(nApps.Futs.Mobile.Shared.Navigation.IBackNavigationService)) as nApps.Futs.Mobile.Shared.Navigation.IBackNavigationService;
                if (backService != null)
                {
                    // synchronously wait for handling
                    var handled = Task.Run(() => backService.OnHardwareBackPressedAsync()).GetAwaiter().GetResult();
                    if (handled)
                        return;
                }
            }
        }
        catch
        {
            // ignore and fall back to default
        }

        base.OnBackPressed();
    }
}
