namespace nApps.Futs.Mobile.Features.Stations;

public sealed class PagedStationResult
{
    public List<ChargingStationListItemDto> Items { get; set; } = [];
    public long TotalCount { get; set; }
}

public class ChargingStationListItemDto
{
    public Guid Id { get; set; }
    public string? StationCode { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? DistanceKm { get; set; }
    public int Status { get; set; }
    public int AvailableConnectorCount { get; set; }
    public int TotalConnectorCount { get; set; }
    public int? MaximumPowerKw { get; set; }
    public List<int> ConnectorTypes { get; set; } = [];
}

public sealed class ChargingStationDetailsDto : ChargingStationListItemDto
{
    public string? ContactInfo { get; set; }
    public string? BusinessHours { get; set; }
    public string? Facilities { get; set; }
    public List<ChargingChargerDto> Chargers { get; set; } = [];
}

public sealed class ChargingChargerDto
{
    public Guid Id { get; set; }
    public string? ChargerCode { get; set; }
    public int Status { get; set; }
    public bool IsOnline { get; set; }
    public List<ChargingConnectorDto> Connectors { get; set; } = [];
}

public sealed class ChargingConnectorDto
{
    public Guid ChargerId { get; set; }
    public int ConnectorId { get; set; }
    public int ConnectorType { get; set; }
    public int PowerType { get; set; }
    public int? MaxPowerKw { get; set; }
    public string? Tariff { get; set; }
    public int Status { get; set; }
    public bool? IsCompatible { get; set; }
    public bool CanStart { get; set; }
}
