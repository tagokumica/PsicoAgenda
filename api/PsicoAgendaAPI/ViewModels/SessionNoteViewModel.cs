
namespace PsicoAgendaAPI.ViewModels
{
    public class SessionNoteViewModel
    {
        public Guid Id { get; private set; }
        public Guid SessionScheduleId { get; private set; }
        public Guid UserId { get; private set; }
        public Guid ProfessionalId { get; private set; }
        public string Content { get; private set; }
        public string[] Tags { get; private set; }
        public string Insight { get; private set; }
        public HealthCareProfissionalViewModel HealthCareProfissionalViewModel { get; private set; }
        public UserViewModel UserViewModel { get; private set; }
        public SessionScheduleViewModel SessionScheduleViewModel { get; private set; }
        public SessionNoteViewModel() { }
        public SessionNoteViewModel(Guid id, Guid sessionScheduleId, Guid userId, Guid professionalId, string content, string[] tags, string insight)
        {
            Id = id;
            SessionScheduleId = sessionScheduleId;
            UserId = userId;
            ProfessionalId = professionalId;
            Content = content;
            Tags = tags;
            Insight = insight;
        }
    }
}