

using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface ITermsAcceptanceService
    {
        Task<TermsAcceptance> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<TermsAcceptance>> ListAsync(Func<IQueryable<TermsAcceptance>, IQueryable<TermsAcceptance>>? query = null, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(TermsAcceptance termsAcceptance, CancellationToken ct = default);
        void Update(TermsAcceptance termsAcceptance);
        void Delete(TermsAcceptance termsAcceptance, CancellationToken ct = default);

    }
}
