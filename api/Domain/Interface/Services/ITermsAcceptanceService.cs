

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
        Task<IEnumerable<TermsAcceptance>> GetIsAgreedAsync(CancellationToken ct = default);
        Task<IEnumerable<TermsAcceptance>> GetIsNotAgreedAsync(CancellationToken ct = default);
        Task<TermsAcceptance> IsAgreedAsync(Guid userId, Guid termsOfUseId, CancellationToken ct = default);
        Task<TermsAcceptance> IsNotAgreedAsync(Guid userId, Guid termsOfUseId, CancellationToken ct = default);
    }
}
