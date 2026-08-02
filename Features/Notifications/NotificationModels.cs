namespace nApps.Futs.Mobile.Features.Notifications;

public sealed class NotificationDto
{
    public Guid Id { get; set; }
    public int Type { get; set; }
    public int Priority { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
    public string? Data { get; set; }
    public DateTime Time { get; set; }
    public bool IsRead { get; set; }
}

public sealed class NotificationListDto
{
    public long TotalCount { get; set; }
    public List<NotificationDto> Items { get; set; } = [];
}
