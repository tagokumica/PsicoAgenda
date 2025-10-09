using Domain.Entities;

namespace Domain.Interface.Repositories;

public interface IAddressRepository : IGenericRepository<Address, Guid>
{
    Task<IEnumerable<Address>> GetAddressesByUserAsync(Guid addressId, CancellationToken ct = default);
    Task<IEnumerable<Address>> GetAddressByLocationAsync(Guid addressId, CancellationToken ct = default);

}