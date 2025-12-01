using Infrastructure.Mail;

namespace Background.Interface.Service;

public class EmailNotificationService : IEmailNotificationService
{
    private readonly IEmailSender _emailSender;
    public EmailNotificationService(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }
    public async Task SendAppointmentReminderAsync(Guid patientId, string email, DateTime appointmentTime, CancellationToken ct)
    {
        string subject = "🧠 Lembrete de Sessão Psicológica";
        string body = $@"
            <h3>Olá!</h3>
            <p>Este é um lembrete da sua sessão agendada para <strong>{appointmentTime:dd/MM/yyyy HH:mm}</strong>.</p>
            <p>Por favor, esteja preparado(a) com alguns minutos de antecedência.</p>
            <p>Equipe <b>PsicoAgenda</b>.</p>";

        await _emailSender.SendAsync(email, subject, body, ct);
    }
}