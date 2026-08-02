namespace nApps.Futs.Mobile.Shared.Location;

public interface IDeviceLocationService
{
    Task<DeviceLocation?> GetCurrentAsync(CancellationToken cancellationToken = default);
}
