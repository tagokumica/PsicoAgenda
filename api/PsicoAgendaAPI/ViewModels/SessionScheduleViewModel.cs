
using Domain.Entities.Enum;

namespace PsicoAgendaAPI.ViewModels
{
    public class SessionScheduleViewModel
    {
        public Guid Id { get; private set; }
        public Guid ProfessionalId { get; private set; }
        public Guid UserId { get; private set; }
        public Guid PatientId { get; private set; }
        public DateTime AvaliableAt { get; private set; }
        public Status Status { get; private set; }
        public TimeSpan DurationMinute { get; private set; }
        public PatientViewModel Patient { get; private set; }
        public HealthCareProfissionalViewModel HealthCareProfissional { get; private set; }
        public UserViewModel UserViewModel { get; private set; }
        public IEnumerable<SessionNoteViewModel> SessionNotesViewModel { get; private set; } = new List<SessionNoteViewModel>();
        public IEnumerable<WaitViewModel> WaitsViewModel { get; private set; } = new List<WaitViewModel>();
        public SessionScheduleViewModel() { }
        public SessionScheduleViewModel(Guid id, Guid professionalId, Guid userId, Guid patientId, DateTime avaliableAt, Status status, TimeSpan durationMinute)
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