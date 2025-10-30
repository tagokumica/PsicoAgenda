
using Domain.Entities.Enum;

namespace PsicoAgendaAPI.ViewModels
{
    public class HealthCareProfissionalViewModel
    {
        public Guid Id { get; private set; }
        public Guid? UserId { get; private set; }
        public Guid SpecialityId { get; private set; }
        public string Name { get; private set; }
        public string CurriculumURL { get; private set; }
        public string UndergraduateURL { get; private set; }
        public string CrpOrCrmURL { get; private set; }
        public ApprovalStatus ApprovalStatus { get; private set; }
        public AvailabilityStatus AvailabilityStatus { get; private set; }
        public UserViewModel UserViewModel { get; private set; }
        public SpecialityViewModel SpecialityViewModel { get; private set; }
        public IEnumerable<SessionNoteViewModel> SessionNotesViewModel { get; private set; } = new List<SessionNoteViewModel>();
        public IEnumerable<SessionScheduleViewModel> SessionSchedulesViewModel { get; private set; } = new List<SessionScheduleViewModel>();
        public IEnumerable<AvailabilitieViewModel> AvailabilitiesViewModel { get; private set; } = new List<AvailabilitieViewModel>();

        public HealthCareProfissionalViewModel() { }

        public HealthCareProfissionalViewModel(Guid id, Guid? userId, Guid specialityId, string name, string curriculumURL, string undergraduateURL, string crpOrCrmURL, ApprovalStatus approvalStatus, AvailabilityStatus availabilityStatus)
        {
            Id = id;
            UserId = userId;
            SpecialityId = specialityId;
            Name = name;
            CurriculumURL = curriculumURL;
            UndergraduateURL = undergraduateURL;
            CrpOrCrmURL = crpOrCrmURL;
            ApprovalStatus = approvalStatus;
            AvailabilityStatus = availabilityStatus;
        }

    }
}