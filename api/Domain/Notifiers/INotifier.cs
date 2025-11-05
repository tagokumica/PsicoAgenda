namespace Domain.Notifiers;

public interface INotifier
{
    bool HasNotifications();
    IReadOnlyCollection<Notification> GetNotifications();
    void Handle(Notification notification);
}