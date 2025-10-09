namespace Domain.Entities;

public class TermsOfUse
{
    public Guid Id { get; private set; }
    public string Content { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string Version { get; private set; }
    public IEnumerable<TermsAcceptance> TermsAcceptance { get; private set; }
    public TermsOfUse(string content, string name, string version, DateTime createdAt)
    {
        Id = Guid.NewGuid();
        Content = content;
        Name = name;
        CreatedAt = createdAt;
        Version = version;
    }

    public TermsOfUse() { }

}