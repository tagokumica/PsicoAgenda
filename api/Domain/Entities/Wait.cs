namespace Domain.Entities;

public class Wait
{
    public Guid Id { get; private set; }
    public Guid PatientId { get; private set; }
    public Guid SessionId { get; private set; }
    public DateTime PreferrdeTime { get; private set; }
    public Guid Status { get; private set; }
    public DateTime CreatedaAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Patient Patient { get; private set; }
    public SessionSchedule SessionSchedule { get; private set; }
    public Wait() { }
    public Wait(Guid id, Guid patientId, Guid sessionId, DateTime preferrdeTime, Guid status, DateTime createdaAt, DateTime updatedAt)
    {
        Id = id;
        PatientId = patientId;
        SessionId = sessionId;
        PreferrdeTime = preferrdeTime;
        Status = status;
        CreatedaAt = createdaAt;
        UpdatedAt = updatedAt;
    }
}