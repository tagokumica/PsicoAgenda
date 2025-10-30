
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper.Interface
{
    public interface IAddressMapper 
    {
        Task<IEnumerable<AddressViewModel>> GetAddressesByUserAsync(Guid addressId, CancellationToken ct = default);
        Task<IEnumerable<AddressViewModel>> GetAddressByLocationAsync(Guid addressId, CancellationToken ct = default);
        Task<AddressViewModel> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<AddressViewModel>> ListAsync(Func<IQueryable<AddressViewModel>, IQueryable<AddressViewModel>>? query = null, CancellationToken ct = default);
        Task AddAsync(AddressViewModel addressViewModel, CancellationToken ct = default);
        void Update(AddressViewModel addressViewModel);
        void Delete(AddressViewModel addressViewModel, CancellationToken ct = default);

    }
}
