using System.Globalization;
using nApps.Futs.Mobile.Shared.Services.Api;

namespace nApps.Futs.Mobile.Features.Stations;

public sealed class StationService : IStationService
{
    private const string BasePath = "/api/app/charging-station";
    private readonly IApiService _apiService;

    public StationService(IApiService apiService) => _apiService = apiService;

    public async Task<PagedStationResult> SearchAsync(StationSearchRequest request)
    {
        var query = new List<string>
        {
            $"AvailableOnly={request.AvailableOnly.ToString().ToLowerInvariant()}",
            $"SkipCount={request.SkipCount}",
            $"MaxResultCount={request.MaxResultCount}"
        };
        Add(query, "Search", request.Search);
        Add(query, "Latitude", request.Latitude);
        Add(query, "Longitude", request.Longitude);
        if (request.Latitude.HasValue && request.Longitude.HasValue)
            Add(query, "RadiusKm", request.RadiusKm);
        Add(query, "CustomerVehicleId", request.CustomerVehicleId);
        Add(query, "ConnectorType", request.ConnectorType);
        return await _apiService.GetAsync<PagedStationResult>($"{BasePath}?{string.Join('&', query)}")
            ?? new PagedStationResult();
    }

    public Task<ChargingStationDetailsDto?> GetAsync(Guid id) =>
        _apiService.GetAsync<ChargingStationDetailsDto>($"{BasePath}/{id}");

    private static void Add(List<string> query, string key, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value)) query.Add($"{key}={Uri.EscapeDataString(value)}");
    }
    private static void Add(List<string> query, string key, double? value)
    {
        if (value.HasValue) query.Add($"{key}={value.Value.ToString(CultureInfo.InvariantCulture)}");
    }
    private static void Add(List<string> query, string key, Guid? value)
    {
        if (value.HasValue) query.Add($"{key}={value.Value}");
    }
    private static void Add(List<string> query, string key, int? value)
    {
        if (value.HasValue) query.Add($"{key}={value.Value}");
    }
}
