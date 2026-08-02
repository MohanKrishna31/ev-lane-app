using nApps.Futs.Mobile.Shared.Services.SignalR;
using nApps.Futs.Mobile.Shared.ViewModels;

namespace nApps.Futs.Mobile.Features.Notifications;

public sealed class NotificationsViewModel : BaseViewModel
{
    private readonly INotificationService _service;
    private readonly NotificationRealtimeService _realtime;
    public NotificationsViewModel(INotificationService service, NotificationRealtimeService realtime) { _service = service; _realtime = realtime; _realtime.Received += OnReceived; }
    public IReadOnlyList<NotificationDto> Notifications { get; private set; } = [];
    public long UnreadCount { get; private set; }
    public async Task LoadAsync() => await ExecuteAsync(async () => { Notifications = (await _service.GetAsync()).Items; UnreadCount = await _service.GetUnreadCountAsync(); OnPropertyChanged(nameof(Notifications)); OnPropertyChanged(nameof(UnreadCount)); try { await _realtime.StartAsync(); } catch { } });
    public async Task MarkReadAsync(NotificationDto item) { if (item.IsRead) return; await _service.MarkReadAsync(item.Id); await LoadAsync(); }
    private void OnReceived(NotificationDto item) { Notifications = new[] { item }.Concat(Notifications).ToList(); UnreadCount++; OnPropertyChanged(nameof(Notifications)); OnPropertyChanged(nameof(UnreadCount)); }
}
