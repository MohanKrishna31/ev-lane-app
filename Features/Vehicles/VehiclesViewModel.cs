using nApps.Futs.Mobile.Shared.ViewModels;

namespace nApps.Futs.Mobile.Features.Vehicles;

public sealed class VehiclesViewModel : BaseViewModel
{
    private readonly IVehicleService _vehicleService;

    public VehiclesViewModel(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public IReadOnlyList<CustomerVehicleDto> Vehicles { get; private set; } = [];

    public async Task LoadAsync()
    {
        await ExecuteAsync(async () =>
        {
            Vehicles = await _vehicleService.GetMyVehiclesAsync();
            OnPropertyChanged(nameof(Vehicles));
        });
    }

    public async Task DeleteAsync(Guid id)
    {
        await ExecuteAsync(async () =>
        {
            await _vehicleService.DeleteAsync(id);
            Vehicles = await _vehicleService.GetMyVehiclesAsync();
            OnPropertyChanged(nameof(Vehicles));
        });
    }

    public async Task SetDefaultAsync(Guid id)
    {
        await ExecuteAsync(async () =>
        {
            await _vehicleService.SetDefaultAsync(id);
            Vehicles = await _vehicleService.GetMyVehiclesAsync();
            OnPropertyChanged(nameof(Vehicles));
        });
    }
}
