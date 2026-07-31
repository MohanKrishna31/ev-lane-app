using System.Threading.Tasks;

namespace nApps.Futs.Mobile.Shared.Navigation;

public interface IBackNavigationService
{
    /// <summary>
    /// Attempts to go back in the navigation stack. Returns true if navigation occurred.
    /// </summary>
    Task<bool> TryGoBackAsync();

    /// <summary>
    /// Called for hardware back presses (Android). Returns true if the event was handled.
    /// </summary>
    Task<bool> OnHardwareBackPressedAsync();

    /// <summary>
    /// Notify the service that navigation occurred (Blazor side should call this on location changes).
    /// </summary>
    void NotifyNavigation(string? uri);

    /// <summary>
    /// Event fired when the platform requests a back navigation and a Blazor subscriber should perform the NavigateTo.
    /// </summary>
    event Func<string?, Task>? PlatformGoBackRequested;
}
