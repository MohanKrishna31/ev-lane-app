using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using nApps.Futs.Mobile.Features.Notifications;
using nApps.Futs.Mobile.Shared.Configuration;
using nApps.Futs.Mobile.Shared.Constants;
using nApps.Futs.Mobile.Shared.Services.Storage;

namespace nApps.Futs.Mobile.Shared.Services.SignalR;

public sealed class NotificationRealtimeService : IAsyncDisposable
{
    private readonly HubConnection _connection;
    public event Action<NotificationDto>? Received;

    public NotificationRealtimeService(IOptions<SignalRSettings> settings, IStorageService storage)
    {
        _connection = new HubConnectionBuilder()
            .WithUrl(settings.Value.HubUrl, options =>
            {
                options.AccessTokenProvider = () => storage.GetSecureAsync(StorageKeys.AccessToken);
            })
            .WithAutomaticReconnect()
            .Build();
        _connection.On<NotificationDto>("ReceiveNotification", item => Received?.Invoke(item));
        _connection.On<NotificationDto>("NotificationReceived", item => Received?.Invoke(item));
    }

    public async Task StartAsync()
    {
        if (_connection.State == HubConnectionState.Disconnected)
            await _connection.StartAsync();
    }

    public async ValueTask DisposeAsync() => await _connection.DisposeAsync();
}
