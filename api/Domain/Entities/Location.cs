namespace Domain.Entities
{
    public class Location
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid AddressId { get; private set; }
        public Address Address { get; private set; }

        public Location(){ }

        public Location(Guid id, string name, Guid addressId)
        {
            Id = id;
            Name = name;
            AddressId = addressId;
        }
    }
}