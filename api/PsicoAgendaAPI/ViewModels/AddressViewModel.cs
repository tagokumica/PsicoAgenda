
namespace PsicoAgendaAPI.ViewModels
{
    public class AddressViewModel
    {
        public Guid Id { get; private set; }
        public string Street { get; private set; }
        public int Number { get; private set; }
        public string Complement { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public string State { get; private set; }
        public IEnumerable<UserViewModel> UsersViewModel { get; private set; } = new List<UserViewModel>();
        public IEnumerable<LocationViewModel> LocationsViewModel { get; private set; } = new List<LocationViewModel>();

        public AddressViewModel() { }

        public AddressViewModel(Guid id, string street, int number, string complement, string city, string zipCode, string state)
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