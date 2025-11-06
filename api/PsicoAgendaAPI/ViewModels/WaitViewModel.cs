namespace PsicoAgendaAPI.ViewModels
{
    public class WaitViewModel
    {
        public Guid Id { get; private set; }
        public Guid PatientId { get; private set; }
        public Guid SessionId { get; private set; }
        public DateTime PreferrdeTime { get; private set; }
        public DateTime CreatedaAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public PatientViewModel PatientViewModel { get; private set; }
        public SessionScheduleViewModel SessionScheduleViewModel { get; private set; }
        public WaitViewModel() { }
        public WaitViewModel(Guid id, Guid patientId, Guid sessionId, DateTime preferrdeTime, DateTime createdaAt, DateTime updatedAt)
        {
            Id = id;
            PatientId = patientId;
            SessionId = sessionId;
            PreferrdeTime = preferrdeTime;
            CreatedaAt = createdaAt;
            UpdatedAt = updatedAt;
        }
    }
}