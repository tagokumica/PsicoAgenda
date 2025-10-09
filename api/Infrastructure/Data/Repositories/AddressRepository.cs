using Domain.Interface.Repositories;
using Domain.Entities;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class AddressRepository : GenericRepository<Address, Guid>, IAddressRepository
{
    public AddressRepository(PsicoContext db) : base(db)
    {
    }

    public async Task<IEnumerable<Address>> GetAddressesByUserAsync(Guid addressId, CancellationToken ct = default)
    {
        return await
            _db
                .Users
                .Include(t => t.Address)
                .Where(s => s.AddressId == addressId)
                .Select(s => s.Address)
                .ToListAsync(ct);
    }

    public async Task<IEnumerable<Address>> GetAddressByLocationAsync(Guid addressId, CancellationToken ct = default)
    {
        return await
            _db
                .Locations
                .Include(t => t.Address)
                .Where(s => s.AddressId == addressId)
                .Select(s => s.Address)
                .ToListAsync(ct);
    }
}