namespace Domain.Entities;

public class SessionNote
{
    public Guid Id { get; private set; }
    public Guid SessionScheduleId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid ProfessionalId { get; private set; }
    public string Content { get; private set; }
    public string[] Tags { get; private set; }
    public string Insight { get; private set; }
    public HealthCareProfissional HealthCareProfissional { get; private set; }
    public User User { get; private set; }
    public SessionSchedule SessionSchedule { get; private set; }
    public SessionNote() { }
    public SessionNote(Guid id, Guid sessionScheduleId, Guid userId, Guid professionalId, string content, string[] tags, string insight)
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