
namespace PsicoAgendaAPI.ViewModels
{
    public class TermsAcceptanceViewModel
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid TermsOfUseId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Content { get; private set; }
        public bool IsAgreed { get; set; }

        public TermsOfUseViewModel TermsOfUseViewModel { get; private set; }
        public UserViewModel UserViewModel { get; private set; }

        public TermsAcceptanceViewModel(Guid id, Guid userId, Guid termsOfUseId, DateTime createdAt, bool isAgreed)
        {
            Id = id;
            UserId = userId;
            TermsOfUseId = termsOfUseId;
            CreatedAt = createdAt;
            IsAgreed = isAgreed;
        }
        public TermsAcceptanceViewModel()
        {
            
        }
    }
}