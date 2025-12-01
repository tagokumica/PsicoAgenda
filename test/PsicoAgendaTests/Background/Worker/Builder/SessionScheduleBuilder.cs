using Domain.Entities;
using Domain.Entities.Enum;

namespace PsicoAgendaTests.Background.Worker.Builder;

public class SessionScheduleBuilder
{
    private Patient _patient = new Patient(
        Guid.NewGuid(),
        "Ana Souza",
        "ana.souza@example.com",
        new DateTime(1992, 8, 14),
        "invalid-cpf",
        "Paciente com histórico de ansiedade leve.",
        Gender.Female,
        "+55 (11) 91234-5678");
    private DateTime _avaliableAt = DateTime.UtcNow.AddHours(2);
    private TimeSpan _duration = TimeSpan.FromMinutes(50);

    public SessionScheduleBuilder WithPatient(Patient patient)
    {
        _patient = patient;
        return this;
    }

    public SessionSchedule Build()
    {
        var schedule = new SessionSchedule(
            Guid.NewGuid(),
            _patient.Id,
            Guid.NewGuid(),
            Guid.NewGuid(),
            _avaliableAt,
            Status.Sheduled,
            _duration);

        typeof(SessionSchedule)
            .GetProperty("Patient")
            ?.SetValue(schedule, _patient); 

        return schedule;
    }
}