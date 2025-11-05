namespace Domain.Notifiers;

public class Notifier : INotifier
{
    private readonly List<Notification> _notifications = new();

    public bool HasNotifications() => _notifications.Any();

    public IReadOnlyCollection<Notification> GetNotifications() =>
        _notifications.AsReadOnly();

    public void Handle(Notification notification)
    {
        _notifications.Add(notification);
    }
}