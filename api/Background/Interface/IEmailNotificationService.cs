namespace Background.Interface;

public interface IEmailNotificationService
{
    Task SendAppointmentReminderAsync(Guid patientId, string email, DateTime appointmentTime, CancellationToken ct);
}