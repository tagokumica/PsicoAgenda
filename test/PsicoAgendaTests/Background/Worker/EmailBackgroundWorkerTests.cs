using Background.Interface;
using Background.Worker;
using Domain.Entities;
using Domain.Entities.Enum;
using Domain.Interface.Services;
using Microsoft.Extensions.Logging;
using Moq;
using PsicoAgendaTests.Background.Worker.Builder;

namespace PsicoAgendaTests.Background.Worker;

public class EmailBackgroundWorkerTests
{
    private readonly Mock<ILogger<EmailBackgroundWorker>> _loggerMock;
    private readonly Mock<IEmailNotificationService> _emailNotificationMock;
    private readonly Mock<ISessionScheduleService> _sessionScheduleMock;
    private readonly EmailBackgroundWorker _worker;
    public EmailBackgroundWorkerTests()
    {
        _loggerMock = new Mock<ILogger<EmailBackgroundWorker>>();
        _emailNotificationMock = new Mock<IEmailNotificationService>();
        _sessionScheduleMock = new Mock<ISessionScheduleService>();
        _worker = new EmailBackgroundWorker(
            _loggerMock.Object,
            _emailNotificationMock.Object,
            _sessionScheduleMock.Object);
    }

    [Fact(DisplayName = "ProcessAsync - Nenhuma sessão encontrada não envia e-mails")]
    public async Task ProcessAsync_NoSessions_DoesNotSendEmails()
    {
        // Arrange
        _sessionScheduleMock
            .Setup(s => s.GetSessionScheduleByDurationCountAsync(It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        // Act
        await _worker.ProcessAsync(CancellationToken.None);

        // Assert
        _emailNotificationMock.Verify(
            n => n.SendAppointmentReminderAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()),
            Times.Never
        );
    }

    [Fact(DisplayName = "ProcessAsync - Envia e-mails para todas as sessões encontradas")]
    public async Task ProcessAsync_FoundSessions_SendsEmails()
    {
        var patient = new Patient(
            Guid.NewGuid(),
            "Ana Souza",
            "ana.souza@example.com",
            new DateTime(1992, 8, 14),
            "invalid-cpf",
            "Paciente com histórico de ansiedade leve.",
            Gender.Female,
            "+55 (11) 91234-5678");

        var sessionBuilder = new SessionScheduleBuilder()
            .WithPatient(patient)
            .Build();

        var sessions = new List<SessionSchedule>
        {
            sessionBuilder
        };

        _sessionScheduleMock
            .Setup(s => s.GetSessionScheduleByDurationCountAsync(It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(sessions.Count);

        _sessionScheduleMock
            .Setup(s => s.GetUpcomingSessionsAsync(It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(sessions);

        // Act
        await _worker.ProcessAsync(CancellationToken.None);

        // Assert
        _emailNotificationMock.Verify(
            n => n.SendAppointmentReminderAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()),
            Times.Exactly(sessions.Count)
        );
    }

    [Fact(DisplayName = "ProcessAsync - Captura exceções e registra erro")]
    public async Task ProcessAsync_Exception_LogsError()
    {
        // Arrange
        _sessionScheduleMock
            .Setup(s => s.GetSessionScheduleByDurationCountAsync(It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Falha simulada"));

        // Act
        await _worker.ProcessAsync(CancellationToken.None);

        // Assert
        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<InvalidOperationException>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
            Times.Once
        );

    }

    public SessionSchedule CreateForTest(
        Patient patient,
        DateTime availableAt,
        TimeSpan duration)
    {
        var schedule = new SessionSchedule(
            Guid.NewGuid(),
            patient.Id,
            Guid.NewGuid(),
            Guid.NewGuid(),
            availableAt,
            Status.Sheduled,
            duration);

        // use reflexão internamente ou um construtor protegido
        typeof(SessionSchedule)
            .GetProperty(nameof(Patient))
            ?.SetValue(schedule, patient);

        return schedule;
    }

}


