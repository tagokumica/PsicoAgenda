using Domain.Entities;
using Domain.Interface.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class AvailabilitieRepository : GenericRepository<Availabilitie, Guid>, IAvailabilitieRepository
{
    public AvailabilitieRepository(PsicoContext db) : base(db)
    {
    }

    public async Task<IEnumerable<Availabilitie>> IsBookedAsync(CancellationToken ct = default)
    {
        return await 
            _set
                .AsNoTracking()
                .Where(s => s.IsBooked)
                .ToListAsync(ct);
    }

    public async Task<IEnumerable<Availabilitie>> IsNotBookedAsync(CancellationToken ct = default)
    {
        return await
            _set
                .AsNoTracking()
                .Where(s => !s.IsBooked)
                .ToListAsync(ct);
    }
}