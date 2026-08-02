using nApps.Futs.Mobile.Shared.Constants;
using nApps.Futs.Mobile.Shared.Services.Api;

namespace nApps.Futs.Mobile.Features.Vehicles;

public sealed class VehicleService : IVehicleService
{
    private readonly IApiService _apiService;

    public VehicleService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IReadOnlyList<CustomerVehicleDto>> GetMyVehiclesAsync() =>
        await _apiService.GetAsync<List<CustomerVehicleDto>>(ApiRoutes.Vehicle.MyVehicles) ?? [];

    public Task<CustomerVehicleDto?> GetAsync(Guid id) =>
        _apiService.GetAsync<CustomerVehicleDto>(ApiRoutes.Vehicle.ById(id));

    public Task<CustomerVehicleDto?> CreateAsync(CreateUpdateCustomerVehicleRequest request) =>
        _apiService.PostAsync<CreateUpdateCustomerVehicleRequest, CustomerVehicleDto>(
            ApiRoutes.Vehicle.Base,
            request);

    public Task<CustomerVehicleDto?> UpdateAsync(Guid id, CreateUpdateCustomerVehicleRequest request) =>
        _apiService.PutAsync<CreateUpdateCustomerVehicleRequest, CustomerVehicleDto>(
            ApiRoutes.Vehicle.ById(id),
            request);

    public Task DeleteAsync(Guid id) =>
        _apiService.DeleteAsync(ApiRoutes.Vehicle.ById(id));

    public Task SetDefaultAsync(Guid id) =>
        _apiService.PostAsync(ApiRoutes.Vehicle.SetDefault(id));

    public async Task<IReadOnlyList<ManufacturerDto>> GetManufacturersAsync() =>
        await _apiService.GetAsync<List<ManufacturerDto>>(ApiRoutes.Vehicle.ActiveManufacturers) ?? [];

    public async Task<IReadOnlyList<VehicleModelDto>> GetModelsAsync(Guid manufacturerId) =>
        await _apiService.GetAsync<List<VehicleModelDto>>(
            ApiRoutes.Vehicle.ModelsByManufacturer(manufacturerId)) ?? [];

    public async Task<IReadOnlyList<VehicleVariantDto>> GetVariantsAsync(Guid vehicleModelId) =>
        await _apiService.GetAsync<List<VehicleVariantDto>>(
            ApiRoutes.Vehicle.VariantsByModel(vehicleModelId)) ?? [];
}
