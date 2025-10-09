using Domain.Entities.Enum;

namespace Domain.Entities
{
    public class SessionSchedule
    {
        public Guid Id { get; private set; }
        public Guid ProfessionalId { get; private set; }
        public Guid UserId { get; private set; }
        public Guid PatientId { get; private set; }
        public DateTime AvaliableAt { get; private set; }
        public Status Status { get; private set; }
        public TimeSpan DurationMinute { get; private set; }
        public Patient Patient { get; private set; }
        public HealthCareProfissional HealthCareProfissional { get; private set; }
        public User User { get; private set; }
        public IEnumerable<SessionNote> SessionNotes { get; private set; } = new List<SessionNote>();
        public IEnumerable<Wait> Waits { get; private set; } = new List<Wait>();
        public SessionSchedule() { }
        public SessionSchedule(Guid id, Guid professionalId, Guid userId, Guid patientId, DateTime avaliableAt, Status status, TimeSpan durationMinute)
        {
            Id = id;
            ProfessionalId = professionalId;
            UserId = userId;
            PatientId = patientId;
            AvaliableAt = avaliableAt;
            Status = status;
            DurationMinute = durationMinute;
        }
    }
}