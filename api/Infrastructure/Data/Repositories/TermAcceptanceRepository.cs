using Domain.Entities;
using Domain.Interface.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class TermsAcceptanceRepository : GenericRepository<TermsAcceptance, Guid>, ITermsAcceptanceRepository
    {
        public TermsAcceptanceRepository(PsicoContext db) : base(db)
        {
        }

        public async Task<IEnumerable<TermsAcceptance>> GetIsAgreedAsync(CancellationToken ct = default)
        {
            return await
                _set
                    .AsNoTracking()
                    .Where(s => s.IsAgreed)
                    .ToListAsync(ct);
        }

        public async Task<IEnumerable<TermsAcceptance>> GetIsNotAgreedAsync(CancellationToken ct = default)
        {
            return await
                _set
                    .AsNoTracking()
                    .Where(s => !s.IsAgreed)
                    .ToListAsync(ct);
        }

        public async Task<TermsAcceptance> IsAgreedAsync(Guid userId, Guid termsOfUseId, CancellationToken ct = default)
        {
            await _set
                .Where(t => t.UserId == userId && t.TermsOfUseId == termsOfUseId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(t => t.IsAgreed, true), ct);

            return await _set
                .AsNoTracking()
                .FirstAsync(t => t.UserId == userId && t.TermsOfUseId == termsOfUseId, ct);
        }

        public async Task<TermsAcceptance> IsNotAgreedAsync(Guid userId, Guid termsOfUseId, CancellationToken ct = default)
        {
            await _set
                .Where(t => t.UserId == userId && t.TermsOfUseId == termsOfUseId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(t => t.IsAgreed, false), ct);

            return await _set
                .AsNoTracking()
                .FirstAsync(t => t.UserId == userId && t.TermsOfUseId == termsOfUseId, ct);
        }
    }
}
