namespace PsicoAgendaAPI.ViewModels
{
    public class TermsOfUseViewModel
    {
        public Guid Id { get; private set; }
        public string Content { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Version { get; private set; }
        public IEnumerable<TermsAcceptanceViewModel> TermsAcceptanceViewModel { get; private set; }
        public TermsOfUseViewModel(string content, string name, string version, DateTime createdAt)
        {
            Id = Guid.NewGuid();
            Content = content;
            Name = name;
            CreatedAt = createdAt;
            Version = version;
        }

        public TermsOfUseViewModel() { }
    }
}