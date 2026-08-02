using nApps.Futs.Mobile.Features.Notifications;
using nApps.Futs.Mobile.Features.Sessions;
using nApps.Futs.Mobile.Features.Stations;
using nApps.Futs.Mobile.Features.Wallet;
using nApps.Futs.Mobile.Shared.ViewModels;

namespace nApps.Futs.Mobile.Features.Dashboard;

public sealed class DashboardViewModel : BaseViewModel
{
    private readonly ISessionService _sessions;
    private readonly IWalletService _wallet;
    private readonly IStationService _stations;
    private readonly INotificationService _notifications;
    public DashboardViewModel(ISessionService sessions, IWalletService wallet, IStationService stations, INotificationService notifications) { _sessions=sessions; _wallet=wallet; _stations=stations; _notifications=notifications; }
    public ChargingSessionDto? ActiveSession { get; private set; }
    public WalletBalanceDto? Wallet { get; private set; }
    public IReadOnlyList<ChargingStationListItemDto> Stations { get; private set; }=[];
    public long UnreadNotifications { get; private set; }
    public async Task LoadAsync() => await ExecuteAsync(async () =>
    {
        try { ActiveSession = await _sessions.GetActiveAsync(); } catch { ActiveSession = null; }
        try { Wallet = await _wallet.GetBalanceAsync(); } catch { Wallet = null; }
        try { Stations = (await _stations.SearchAsync(new(){MaxResultCount=5})).Items; } catch { Stations=[]; }
        try { UnreadNotifications = await _notifications.GetUnreadCountAsync(); } catch { UnreadNotifications=0; }
        OnPropertyChanged(string.Empty);
    });
}
