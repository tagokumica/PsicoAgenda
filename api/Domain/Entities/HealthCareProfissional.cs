using Domain.Entities.Enum;

namespace Domain.Entities
{
    public class HealthCareProfissional
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
        public User User { get; private set; }
        public Speciality Speciality { get; private set; }
        public IEnumerable<SessionNote> SessionNotes { get; private set; } = new List<SessionNote>();
        public IEnumerable<SessionSchedule> SessionSchedules { get; private set; } = new List<SessionSchedule>();
        public IEnumerable<Availabilitie> Availabilities { get; private set; } = new List<Availabilitie>();

        public HealthCareProfissional() { }

        public HealthCareProfissional(Guid id, Guid? userId, Guid specialityId, string name, string curriculumURL, string undergraduateURL, string crpOrCrmURL, ApprovalStatus approvalStatus, AvailabilityStatus availabilityStatus)
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