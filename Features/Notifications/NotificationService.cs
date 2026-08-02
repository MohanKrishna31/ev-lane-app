using nApps.Futs.Mobile.Shared.Services.Api;

namespace nApps.Futs.Mobile.Features.Notifications;

public interface INotificationService
{
    Task<NotificationListDto> GetAsync(bool? isRead = null);
    Task<long> GetUnreadCountAsync();
    Task MarkReadAsync(Guid id);
}

public sealed class NotificationService : INotificationService
{
    private readonly IApiService _api;
    public NotificationService(IApiService api) => _api = api;
    public async Task<NotificationListDto> GetAsync(bool? isRead = null)
    {
        var filter = isRead.HasValue ? $"&IsRead={isRead.Value.ToString().ToLowerInvariant()}" : "";
        return await _api.GetAsync<NotificationListDto>($"/api/app/notification?SkipCount=0&MaxResultCount=100{filter}") ?? new();
    }
    public async Task<long> GetUnreadCountAsync() => await _api.GetAsync<long>("/api/app/notification/unread-count");
    public Task MarkReadAsync(Guid id) => _api.PostAsync($"/api/app/notification/mark-as-read/{id}");
}
