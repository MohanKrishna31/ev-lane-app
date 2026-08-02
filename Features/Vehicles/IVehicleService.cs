namespace nApps.Futs.Mobile.Features.Vehicles;

public interface IVehicleService
{
    Task<IReadOnlyList<CustomerVehicleDto>> GetMyVehiclesAsync();
    Task<CustomerVehicleDto?> GetAsync(Guid id);
    Task<CustomerVehicleDto?> CreateAsync(CreateUpdateCustomerVehicleRequest request);
    Task<CustomerVehicleDto?> UpdateAsync(Guid id, CreateUpdateCustomerVehicleRequest request);
    Task DeleteAsync(Guid id);
    Task SetDefaultAsync(Guid id);
    Task<IReadOnlyList<ManufacturerDto>> GetManufacturersAsync();
    Task<IReadOnlyList<VehicleModelDto>> GetModelsAsync(Guid manufacturerId);
    Task<IReadOnlyList<VehicleVariantDto>> GetVariantsAsync(Guid vehicleModelId);
}
