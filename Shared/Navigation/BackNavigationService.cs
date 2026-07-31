using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace nApps.Futs.Mobile.Shared.Navigation;

public class BackNavigationService : IBackNavigationService
{
    DateTime _lastBackPressed = DateTime.MinValue;
    readonly SemaphoreSlim _lock = new(1, 1);
    readonly System.Collections.Generic.Stack<string> _history = new();

    /// <summary>
    /// Raised when the platform requests a back navigation. The argument is the target URI to navigate to.
    /// Subscribers (Blazor side) should perform the actual NavigationManager.NavigateTo call.
    /// </summary>
    public event Func<string?, Task>? PlatformGoBackRequested;

    public void NotifyNavigation(string? uri)
    {
        try
        {
            if (string.IsNullOrEmpty(uri))
                return;

            // normalize: use absolute uri as key
            if (_history.Count == 0 || _history.Peek() != uri)
            {
                _history.Push(uri);
            }
        }
        catch
        {
            // swallow errors
        }
    }

    public async Task<bool> TryGoBackAsync()
    {
        // prefer Blazor navigation history first
        if (_history.Count > 1)
        {
            // pop current
            _history.Pop();
            var target = _history.Count > 0 ? _history.Peek() : null;
            if (PlatformGoBackRequested != null)
            {
                await PlatformGoBackRequested.Invoke(target);
                return true;
            }
        }

        // fallback to Shell navigation if app uses Shell
        var nav = Shell.Current?.Navigation;
        if (nav != null && nav.NavigationStack.Count > 1)
        {
            await Shell.Current.Navigation.PopAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> OnHardwareBackPressedAsync()
    {
        await _lock.WaitAsync();
        try
        {
            if (await TryGoBackAsync())
                return true;

            var now = DateTime.UtcNow;
            if ((now - _lastBackPressed).TotalSeconds <= 2)
            {
                // exit the app
#if ANDROID
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#else
                Environment.Exit(0);
#endif
                return true;
            }

            _lastBackPressed = now;

#if ANDROID
            try
            {
                var context = Android.App.Application.Context;
                Android.Widget.Toast.MakeText(context, "Press back again to exit", Android.Widget.ToastLength.Short).Show();
            }
            catch
            {
                // ignore toast failures
            }
#else
            if (Application.Current?.MainPage != null)
            {
                // lightweight fallback notification
                await Application.Current.MainPage.DisplayAlert("Exit", "Press back again to exit", "OK");
            }
#endif

            return true;
        }
        finally
        {
            _lock.Release();
        }
    }
}
