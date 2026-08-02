using nApps.Futs.Mobile.Shared.Models;
using nApps.Futs.Mobile.Shared.ViewModels;

namespace nApps.Futs.Mobile.Features.Vehicles;

public sealed class VehicleEditViewModel : BaseViewModel
{
    private readonly IVehicleService _vehicleService;

    public VehicleEditViewModel(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public Guid? VehicleId { get; private set; }
    public bool IsEdit => VehicleId.HasValue;
    public Guid SelectedManufacturerId { get; set; }
    public Guid SelectedModelId { get; set; }
    public CreateUpdateCustomerVehicleRequest Model { get; private set; } = new();
    public IReadOnlyList<SelectOption<Guid>> Manufacturers { get; private set; } = [];
    public IReadOnlyList<SelectOption<Guid>> Models { get; private set; } = [];
    public IReadOnlyList<SelectOption<Guid>> Variants { get; private set; } = [];

    public async Task LoadAsync(Guid? vehicleId)
    {
        await ExecuteAsync(async () =>
        {
            VehicleId = vehicleId;
            var manufacturers = await _vehicleService.GetManufacturersAsync();
            Manufacturers = ToOptions(manufacturers.Select(x => (x.Id, x.Name)));

            if (vehicleId.HasValue)
            {
                var vehicle = await _vehicleService.GetAsync(vehicleId.Value);
                if (vehicle is null)
                    throw new InvalidOperationException("Vehicle was not found.");

                SelectedManufacturerId = vehicle.ManufacturerId;
                SelectedModelId = vehicle.VehicleModelId;
                Models = ToOptions((await _vehicleService.GetModelsAsync(SelectedManufacturerId))
                    .Select(x => (x.Id, x.Name)));
                Variants = ToOptions((await _vehicleService.GetVariantsAsync(SelectedModelId))
                    .Select(x => (x.Id, x.Name)));
                Model = new CreateUpdateCustomerVehicleRequest
                {
                    VehicleVariantId = vehicle.VehicleVariantId,
                    RegistrationNumber = vehicle.RegistrationNumber ?? string.Empty,
                    NickName = vehicle.NickName,
                    Color = vehicle.Color,
                    VinNumber = vehicle.VinNumber,
                    CurrentOdometerKm = vehicle.CurrentOdometerKm,
                    IsDefault = vehicle.IsDefault
                };
            }

            NotifyFormChanged();
        });
    }

    public async Task ManufacturerChangedAsync(Guid manufacturerId)
    {
        SelectedManufacturerId = manufacturerId;
        SelectedModelId = Guid.Empty;
        Model.VehicleVariantId = Guid.Empty;
        Models = SelectedManufacturerId == Guid.Empty
            ? []
            : ToOptions((await _vehicleService.GetModelsAsync(SelectedManufacturerId))
                .Select(x => (x.Id, x.Name)));
        Variants = [];
        NotifyFormChanged();
    }

    public async Task ModelChangedAsync(Guid modelId)
    {
        SelectedModelId = modelId;
        Model.VehicleVariantId = Guid.Empty;
        Variants = SelectedModelId == Guid.Empty
            ? []
            : ToOptions((await _vehicleService.GetVariantsAsync(SelectedModelId))
                .Select(x => (x.Id, x.Name)));
        NotifyFormChanged();
    }

    public void VariantChanged(Guid variantId)
    {
        Model.VehicleVariantId = variantId;
        OnPropertyChanged(nameof(Model));
    }

    public async Task<bool> SaveAsync()
    {
        if (Model.VehicleVariantId == Guid.Empty)
        {
            ErrorMessage = "Please select a vehicle variant.";
            return false;
        }

        Model.RegistrationNumber = Model.RegistrationNumber.Trim().ToUpperInvariant();
        if (string.IsNullOrWhiteSpace(Model.RegistrationNumber))
        {
            ErrorMessage = "Registration number is required.";
            return false;
        }

        var result = await ExecuteAsync(() => VehicleId.HasValue
            ? _vehicleService.UpdateAsync(VehicleId.Value, Model)
            : _vehicleService.CreateAsync(Model));

        return result is not null;
    }

    private void NotifyFormChanged()
    {
        OnPropertyChanged(nameof(VehicleId));
        OnPropertyChanged(nameof(IsEdit));
        OnPropertyChanged(nameof(SelectedManufacturerId));
        OnPropertyChanged(nameof(SelectedModelId));
        OnPropertyChanged(nameof(Model));
        OnPropertyChanged(nameof(Manufacturers));
        OnPropertyChanged(nameof(Models));
        OnPropertyChanged(nameof(Variants));
    }

    private static IReadOnlyList<SelectOption<Guid>> ToOptions(
        IEnumerable<(Guid Id, string? Name)> values)
    {
        var options = new List<SelectOption<Guid>>
        {
            new() { Value = Guid.Empty, Text = "Select" }
        };
        options.AddRange(values.Select(x => new SelectOption<Guid>
        {
            Value = x.Id,
            Text = x.Name ?? "Unnamed"
        }));
        return options;
    }
}
