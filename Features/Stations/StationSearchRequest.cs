namespace nApps.Futs.Mobile.Features.Stations;

public sealed class StationSearchRequest
{
    public string? Search { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double RadiusKm { get; set; } = 25;
    public Guid? CustomerVehicleId { get; set; }
    public int? ConnectorType { get; set; }
    public bool AvailableOnly { get; set; }
    public int SkipCount { get; set; }
    public int MaxResultCount { get; set; } = 50;
}
