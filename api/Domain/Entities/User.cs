using Domain.Entities.Enum;

namespace Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public Gender Gender { get; private set; }
    public Guid AddressId { get; private set; }
    public Address Address { get; private set; }
    public string Avatar { get; private set; }
    public IEnumerable<TermsAcceptance> TermsAcceptance { get; private set; } = new List<TermsAcceptance>();
    public IEnumerable<Availabilitie> Availabilities { get; private set; } = new List<Availabilitie>();
    public IEnumerable<SessionSchedule> SessionSchedules { get; private set; } = new List<SessionSchedule>();
    public IEnumerable<SessionNote> SessionNotes { get; private set; } = new List<SessionNote>();
    public IEnumerable<HealthCareProfissional> HealthCareProfessionals { get; private set; } = new List<HealthCareProfissional>();
    public User() { }

    public User(Guid id, string name, string email, Gender gender, Guid addressId, string avatar)
    {
        Id = id;
        Name = name;
        Email = email;
        Gender = gender;
        AddressId = addressId;
        Avatar = avatar;
    }
}