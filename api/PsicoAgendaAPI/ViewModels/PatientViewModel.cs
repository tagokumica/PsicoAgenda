
using Domain.Entities.Enum;

namespace PsicoAgendaAPI.ViewModels
{
    public class PatientViewModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public string Cpf { get; private set; }
        public string Notes { get; private set; }
        public Gender Gender { get; private set; }
        public string EmergencyContract { get; private set; }
        public IEnumerable<ConsentViewModel> ConsentsViewModel { get; private set; } = new List<ConsentViewModel>();
        public IEnumerable<WaitViewModel> WaitsViewModel { get; private set; } = new List<WaitViewModel>();
        public IEnumerable<AvailabilitieViewModel> AvailabilitiesViewModel { get; private set; } = new List<AvailabilitieViewModel>();
        public IEnumerable<SessionScheduleViewModel> SessionSchedulesViewModel { get; private set; } = new List<SessionScheduleViewModel>();
        public PatientViewModel() { }

        public PatientViewModel(Guid id, string name, string email, DateTime? birthDate, string cpf, string notes, Gender gender, string emergencyContract)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Cpf = cpf;
            Notes = notes;
            Gender = gender;
            EmergencyContract = emergencyContract;
        }
    }
}