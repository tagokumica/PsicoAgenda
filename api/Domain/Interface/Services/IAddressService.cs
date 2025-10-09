using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAddressesByUserAsync(Guid addressId, CancellationToken ct = default);
        Task<IEnumerable<Address>> GetAddressByLocationAsync(Guid addressId, CancellationToken ct = default);
        Task<Address> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<Address>> ListAsync(Func<IQueryable<Address>, IQueryable<Address>>? query = null, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(Address address, CancellationToken ct = default);
        void Update(Address address);
        void Delete(Address address, CancellationToken ct = default);
    }
}
