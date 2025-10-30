
using Domain.Entities.Enum;

namespace PsicoAgendaAPI.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public Gender Gender { get; private set; }
        public Guid AddressId { get; private set; }
        public AddressViewModel AddressViewModel { get; private set; }
        public string Avatar { get; private set; }
        public IEnumerable<TermsAcceptanceViewModel> TermsAcceptanceViewModel { get; private set; } = new List<TermsAcceptanceViewModel>();
        public IEnumerable<AvailabilitieViewModel> AvailabilitiesViewModel { get; private set; } = new List<AvailabilitieViewModel>();
        public IEnumerable<SessionScheduleViewModel> SessionSchedulesViewModel { get; private set; } = new List<SessionScheduleViewModel>();
        public IEnumerable<SessionNoteViewModel> SessionNotesViewModel { get; private set; } = new List<SessionNoteViewModel>();
        public IEnumerable<HealthCareProfissionalViewModel> HealthCareProfessionalsViewModel { get; private set; } = new List<HealthCareProfissionalViewModel>();
        public UserViewModel() { }

        public UserViewModel(Guid id, string name, string email, Gender gender, Guid addressId, string avatar)
        {
            Id = id;
            Name = name;
            Email = email;
            Gender = gender;
            AddressId = addressId;
            Avatar = avatar;
        }
    }
}