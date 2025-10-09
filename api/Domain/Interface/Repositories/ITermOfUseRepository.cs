
using Domain.Entities;

namespace Domain.Interface.Repositories
{
    public interface ITermOfUseRepository : IGenericRepository<TermsOfUse, Guid>
    {
        Task<IEnumerable<TermsOfUse>> GetTermsOfUseByTermsAcceptanceAsync(Guid termsOfUseId, CancellationToken ct = default);

    }
}
