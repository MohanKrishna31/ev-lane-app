namespace nApps.Futs.Mobile.Features.Sessions;

public interface ISessionService
{
    Task<PagedSessionResult> GetMySessionsAsync(int skipCount = 0, int maxResultCount = 50);
    Task<ChargingSessionDto?> GetMySessionAsync(Guid id);
    Task<ChargingSessionDto?> GetActiveAsync();
    Task<ChargingStartRequestDto?> RequestStartAsync(RequestChargingStartDto request);
    Task RequestStopAsync(Guid chargingSessionId);
}
