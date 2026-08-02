namespace nApps.Futs.Mobile.Shared.Location;

public sealed class DeviceLocationService : IDeviceLocationService
{
    public async Task<DeviceLocation?> GetCurrentAsync(CancellationToken cancellationToken = default)
    {
        var permission = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (permission != PermissionStatus.Granted)
            permission = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

        if (permission != PermissionStatus.Granted)
            return null;

        var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(15));
        var location = await Geolocation.Default.GetLocationAsync(request, cancellationToken);
        return location is null ? null : new DeviceLocation(location.Latitude, location.Longitude);
    }
}
