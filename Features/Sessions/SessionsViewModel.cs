using nApps.Futs.Mobile.Shared.ViewModels;

namespace nApps.Futs.Mobile.Features.Sessions;

public sealed class SessionsViewModel : BaseViewModel
{
    private readonly ISessionService _sessionService;
    public SessionsViewModel(ISessionService sessionService) => _sessionService = sessionService;
    public IReadOnlyList<ChargingSessionDto> Sessions { get; private set; } = [];

    public async Task LoadAsync()
    {
        await ExecuteAsync(async () =>
        {
            Sessions = (await _sessionService.GetMySessionsAsync()).Items;
            OnPropertyChanged(nameof(Sessions));
        });
    }
}
