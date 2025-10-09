using Domain.Entities.Enum;

namespace Domain.Entities;

public class Availabilitie
{
    public Guid Id { get; set; }
    public Guid PatientId { get; private set; }
    public Guid ProfissionalId { get; private set; }
    public DateTime AvaliableAt { get; private set; }
    public TimeSpan DurationMinutes { get; private set; }
    public Guid BookedByUserId { get; private set; }
    public bool IsBooked { get; private set; }
    public Guid CreatedBy { get; private set; }
    public Source Source { get; private set; }
    public TypeAvailabilitie TypeAvailabilitie { get; private set; }
    public string? Location { get; private set; }
    public string? MeetUrl { get; private set; }
    public User User { get; private set; }
    public HealthCareProfissional HealthCareProfissional { get; private set; }
    public Patient Patient { get; private set; }

    public Availabilitie() { }
    public Availabilitie(Guid id, Guid patientId, Guid profissionalId, DateTime avaliableAt, TimeSpan durationMinutes, Guid createdBy, Source source, TypeAvailabilitie typeAvailabilitie, string? location, string? meetUrl, bool isBooked)
    {
        Id = id;
        PatientId = patientId;
        ProfissionalId = profissionalId;
        AvaliableAt = avaliableAt;
        DurationMinutes = durationMinutes;
        CreatedBy = createdBy;
        Source = source;
        TypeAvailabilitie = typeAvailabilitie;
        Location = location;
        MeetUrl = meetUrl;
        IsBooked = isBooked;
    }
}