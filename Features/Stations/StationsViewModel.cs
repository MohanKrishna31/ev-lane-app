using nApps.Futs.Mobile.Shared.Location;
using nApps.Futs.Mobile.Shared.ViewModels;

namespace nApps.Futs.Mobile.Features.Stations;

public sealed class StationsViewModel : BaseViewModel
{
    private readonly IStationService _stationService;
    private readonly IDeviceLocationService _locationService;

    public StationsViewModel(IStationService stationService, IDeviceLocationService locationService)
    {
        _stationService = stationService;
        _locationService = locationService;
    }

    public IReadOnlyList<ChargingStationListItemDto> Stations { get; private set; } = [];
    public DeviceLocation? CurrentLocation { get; private set; }
    public string SearchText { get; set; } = string.Empty;
    public bool AvailableOnly { get; set; }
    public double RadiusKm { get; set; } = 25;

    public async Task LoadAsync(bool requestLocation = true, bool includeLocation = true)
    {
        await ExecuteAsync(async () =>
        {
            if (requestLocation)
            {
                try
                {
                    CurrentLocation = await _locationService.GetCurrentAsync();
                }
                catch
                {
                    CurrentLocation = null;
                }
            }

            var request = new StationSearchRequest
            {
                Search = SearchText,
                Latitude = includeLocation ? CurrentLocation?.Latitude : null,
                Longitude = includeLocation ? CurrentLocation?.Longitude : null,
                RadiusKm = RadiusKm,
                AvailableOnly = AvailableOnly
            };
            var result = await _stationService.SearchAsync(request);

            // Seeded or searched stations may be outside the nearby radius.
            // Fall back to an unbounded station query rather than showing an
            // unexplained empty result on first load.
            if (result.Items.Count == 0 && includeLocation)
            {
                request.Latitude = null;
                request.Longitude = null;
                result = await _stationService.SearchAsync(request);
            }

            Stations = result.Items;
            OnPropertyChanged(nameof(CurrentLocation));
            OnPropertyChanged(nameof(Stations));
        });
    }
}
