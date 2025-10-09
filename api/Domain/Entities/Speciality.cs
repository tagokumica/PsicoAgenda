namespace Domain.Entities
{
    public class Speciality
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<HealthCareProfissional> HealthCareProfissionals { get; private set; } = new List<HealthCareProfissional>();
        public Speciality() { }
        public Speciality(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}