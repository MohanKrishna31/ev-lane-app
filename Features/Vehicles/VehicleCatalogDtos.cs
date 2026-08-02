namespace nApps.Futs.Mobile.Features.Vehicles;

public sealed class ManufacturerDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public sealed class VehicleModelDto
{
    public Guid Id { get; set; }
    public Guid ManufacturerId { get; set; }
    public string? Name { get; set; }
}

public sealed class VehicleVariantDto
{
    public Guid Id { get; set; }
    public Guid VehicleModelId { get; set; }
    public string? Name { get; set; }
    public double BatteryCapacityKWh { get; set; }
    public string? ConnectorTypeName { get; set; }
    public int CertifiedRangeKm { get; set; }
}
