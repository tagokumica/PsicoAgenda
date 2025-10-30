

namespace PsicoAgendaAPI.ViewModels
{
    public class LocationViewModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid AddressId { get; private set; }
        public AddressViewModel AddressViewModel { get; private set; }

        public LocationViewModel(){ }

        public LocationViewModel(Guid id, string name, Guid addressId)
        {
            Id = id;
            Name = name;
            AddressId = addressId;
        }
    }
}