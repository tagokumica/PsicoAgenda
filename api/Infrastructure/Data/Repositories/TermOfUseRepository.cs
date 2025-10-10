using Domain.Entities;
using Domain.Interface.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class TermOfUseRepository : GenericRepository<TermsOfUse, Guid>, ITermOfUseRepository
    {
        public TermOfUseRepository(PsicoContext db) : base(db)
        {
        }

        public async Task<IEnumerable<TermsOfUse>> GetTermsOfUseByTermsAcceptanceAsync(Guid termsOfUseId, CancellationToken ct = default)
        {
            return await
                _db
                    .TermsAcceptances
                    .AsNoTracking()
                    .Include(t => t.TermsOfUse)
                    .Where(s => s.TermsOfUseId == termsOfUseId)
                    .Select(s => s.TermsOfUse)
                    .ToListAsync(ct);
        }
    }
}
