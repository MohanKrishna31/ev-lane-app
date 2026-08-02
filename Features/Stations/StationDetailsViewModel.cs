using nApps.Futs.Mobile.Shared.ViewModels;

namespace nApps.Futs.Mobile.Features.Stations;

public sealed class StationDetailsViewModel : BaseViewModel
{
    private readonly IStationService _stationService;

    public StationDetailsViewModel(IStationService stationService) => _stationService = stationService;

    public ChargingStationDetailsDto? Station { get; private set; }

    public async Task LoadAsync(Guid id)
    {
        await ExecuteAsync(async () =>
        {
            Station = await _stationService.GetAsync(id);
            OnPropertyChanged(nameof(Station));
        });
    }
}
