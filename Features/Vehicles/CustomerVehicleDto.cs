namespace nApps.Futs.Mobile.Features.Vehicles;

public sealed class CustomerVehicleDto
{
    public Guid Id { get; set; }
    public Guid ManufacturerId { get; set; }
    public string? ManufacturerName { get; set; }
    public Guid VehicleModelId { get; set; }
    public string? VehicleModelName { get; set; }
    public Guid VehicleVariantId { get; set; }
    public string? VehicleVariantName { get; set; }
    public string? RegistrationNumber { get; set; }
    public string? NickName { get; set; }
    public string? Color { get; set; }
    public string? VinNumber { get; set; }
    public double BatteryCapacityKWh { get; set; }
    public int ConnectorType { get; set; }
    public string? ConnectorTypeName { get; set; }
    public double MaxAcChargingPowerKw { get; set; }
    public double MaxDcChargingPowerKw { get; set; }
    public bool SupportsFastCharging { get; set; }
    public double CurrentOdometerKm { get; set; }
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; }
}
