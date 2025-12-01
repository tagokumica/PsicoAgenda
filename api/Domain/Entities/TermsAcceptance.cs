namespace Domain.Entities;

public class TermsAcceptance
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid TermsOfUseId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string Content { get; private set; }
    public bool IsAgreed { get; private set; }

    public TermsOfUse TermsOfUse { get; private set; }
    public User User { get; private set; }

    public TermsAcceptance(Guid id, Guid userId, Guid termsOfUseId, DateTime createdAt, bool isAgreed, string content)
    {
        Id = id;
        UserId = userId;
        TermsOfUseId = termsOfUseId;
        CreatedAt = createdAt;
        IsAgreed = isAgreed;
        Content = content;
    }
    public TermsAcceptance()
    {
    }
}