namespace nApps.Futs.Mobile.Features.Vehicles;

public sealed class CreateUpdateCustomerVehicleRequest
{
    public Guid VehicleVariantId { get; set; }
    public string RegistrationNumber { get; set; } = string.Empty;
    public string? NickName { get; set; }
    public string? Color { get; set; }
    public string? VinNumber { get; set; }
    public double CurrentOdometerKm { get; set; }
    public bool IsDefault { get; set; }
}
