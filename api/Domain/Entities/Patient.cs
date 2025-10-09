using Domain.Entities.Enum;

namespace Domain.Entities
{
    public class Patient
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public string Cpf { get; private set; }
        public string Notes { get; private set; }
        public Gender Gender { get; private set; }
        public string EmergencyContract { get; private set; }
        public IEnumerable<Consent> Consents { get; private set; } = new List<Consent>();
        public IEnumerable<Wait> Waits { get; private set; } = new List<Wait>();
        public IEnumerable<Availabilitie> Availabilities { get; private set; } = new List<Availabilitie>();
        public IEnumerable<SessionSchedule> SessionSchedules { get; private set; } = new List<SessionSchedule>();
        public Patient() { }

        public Patient(Guid id, string name, string email, DateTime? birthDate, string cpf, string notes, Gender gender, string emergencyContract)
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