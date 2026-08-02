using nApps.Futs.Mobile.Shared.ViewModels;

namespace nApps.Futs.Mobile.Features.Sessions;

public sealed class SessionDetailsViewModel : BaseViewModel
{
    private readonly ISessionService _sessionService;
    public SessionDetailsViewModel(ISessionService sessionService) => _sessionService = sessionService;
    public ChargingSessionDto? Session { get; private set; }

    public async Task LoadAsync(Guid id)
    {
        await ExecuteAsync(async () =>
        {
            Session = await _sessionService.GetMySessionAsync(id);
            OnPropertyChanged(nameof(Session));
        });
    }

    public async Task RequestStopAsync()
    {
        if (Session is null) return;
        await ExecuteAsync(async () =>
        {
            await _sessionService.RequestStopAsync(Session.Id);
            Session = await _sessionService.GetMySessionAsync(Session.Id);
            OnPropertyChanged(nameof(Session));
        });
    }
}
