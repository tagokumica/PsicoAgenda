namespace Domain.Entities
{
    public class Address
    {
        public Guid Id { get; private set; }
        public string Street { get; private set; }
        public int Number { get; private set; }
        public string Complement { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public string State { get; private set; }
        public IEnumerable<User> Users { get; private set; } = new List<User>();
        public IEnumerable<Location> Locations { get; private set; } = new List<Location>();

        public Address() { }

        public Address(Guid id, string street, int number, string complement, string city, string zipCode, string state)
        {
            Id = id;
            Street = street;
            Number = number;
            Complement = complement;
            City = city;
            ZipCode = zipCode;
            State = state;
        }

    }
}