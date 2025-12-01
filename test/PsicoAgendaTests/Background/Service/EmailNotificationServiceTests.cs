using Background.Interface.Service;
using Infrastructure.Mail;
using Moq;

namespace PsicoAgendaTests.Background.Service;

public class EmailNotificationServiceTests
{
    private readonly Mock<IEmailSender> _emailSenderMock;
    private readonly EmailNotificationService _service;
    public EmailNotificationServiceTests()
    {
        _emailSenderMock = new Mock<IEmailSender>();
        _service = new EmailNotificationService(_emailSenderMock.Object);
    }

    [Fact(DisplayName = "SendAppointmentReminderAsync envia email com assunto e corpo esperados")]
    public async Task SendAppointmentReminderAsync_Success()
    {
        
        var patientId = Guid.NewGuid();
        var email = "paciente@teste.com";
        var appointmentTime = new DateTime(2025, 10, 29, 15, 30, 0);
        var ct = CancellationToken.None;

        // Act
        await _service.SendAppointmentReminderAsync(patientId, email, appointmentTime, ct);

        // Assert
        _emailSenderMock.Verify(sender => sender.SendAsync(
                email,
                It.Is<string>(s => s.Contains("Lembrete de Sessão Psicológica")),
                It.Is<string>(body => body.Contains(appointmentTime.ToString("dd/MM/yyyy HH:mm")) &&
                                      body.Contains("PsicoAgenda")),
                ct),
            Times.Once
        );
    }

}