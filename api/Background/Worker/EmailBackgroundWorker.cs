using Background.Interface;
using Domain.Interface.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Background.Worker;

public class EmailBackgroundWorker : BackgroundService
{
    private readonly ILogger<EmailBackgroundWorker> _logger;
    private readonly IEmailNotificationService _notificationService;
    private readonly ISessionScheduleService _sessionScheduleService;

    public EmailBackgroundWorker(ILogger<EmailBackgroundWorker> logger, IEmailNotificationService notificationService, ISessionScheduleService sessionScheduleService)
    {
        _logger = logger;
        _notificationService = notificationService;
        _sessionScheduleService = sessionScheduleService;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }

    public async Task ProcessAsync(CancellationToken ct)
    {
        try
        {
            var count = await _sessionScheduleService.GetSessionScheduleByDurationCountAsync(TimeSpan.FromHours(24), ct);
            if (count <= 0)
            {
                _logger.LogInformation("Nenhuma sessão encontrada nas próximas 24 horas.");
                return;
            }

            _logger.LogInformation("Foram encontradas {Count} sessões nas próximas 24 horas.", count);

            var sessions = await _sessionScheduleService.GetUpcomingSessionsAsync(TimeSpan.FromHours(24), ct);
            foreach (var session in sessions)
            {
                await _notificationService.SendAppointmentReminderAsync(session.PatientId, session.Patient.Email, session.AvaliableAt, ct);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro no envio de e-mails.");
        }
    }
}