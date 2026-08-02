using nApps.Futs.Mobile.Shared.Services.Api;

namespace nApps.Futs.Mobile.Features.Sessions;

public sealed class SessionService : ISessionService
{
    private const string BasePath = "/api/app/charging-session";
    private readonly IApiService _apiService;

    public SessionService(IApiService apiService) => _apiService = apiService;

    public async Task<PagedSessionResult> GetMySessionsAsync(int skipCount = 0, int maxResultCount = 50) =>
        await _apiService.GetAsync<PagedSessionResult>(
            $"{BasePath}/my-sessions?SkipCount={skipCount}&MaxResultCount={maxResultCount}")
        ?? new PagedSessionResult();

    public Task<ChargingSessionDto?> GetMySessionAsync(Guid id) =>
        _apiService.GetAsync<ChargingSessionDto>($"{BasePath}/{id}/my");

    public Task<ChargingSessionDto?> GetActiveAsync() =>
        _apiService.GetAsync<ChargingSessionDto>($"{BasePath}/my-active-session");

    public Task<ChargingStartRequestDto?> RequestStartAsync(RequestChargingStartDto request) =>
        _apiService.PostAsync<RequestChargingStartDto, ChargingStartRequestDto>(
            $"{BasePath}/request-start",
            request);

    public Task RequestStopAsync(Guid chargingSessionId) =>
        _apiService.PostAsync($"{BasePath}/{chargingSessionId}/request-stop");
}
