using Domain.Entities;

namespace Domain.Interface.Repositories
{
    public interface ITermsAcceptanceRepository : IGenericRepository<TermsAcceptance, Guid>
    {
        Task<IEnumerable<TermsAcceptance>> GetIsAgreedAsync(CancellationToken ct = default);
        Task<IEnumerable<TermsAcceptance>> GetIsNotAgreedAsync(CancellationToken ct = default);
        Task<TermsAcceptance> IsAgreedAsync(Guid userId, Guid termsOfUseId, CancellationToken ct = default);
        Task<TermsAcceptance> IsNotAgreedAsync(Guid userId, Guid termsOfUseId, CancellationToken ct = default);

    }
}
