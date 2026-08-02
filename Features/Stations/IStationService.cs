namespace nApps.Futs.Mobile.Features.Stations;

public interface IStationService
{
    Task<PagedStationResult> SearchAsync(StationSearchRequest request);
    Task<ChargingStationDetailsDto?> GetAsync(Guid id);
}
