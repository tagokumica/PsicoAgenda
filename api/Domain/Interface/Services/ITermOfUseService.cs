using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface ITermOfUseService
    {
        Task<IEnumerable<TermsOfUse>> GetTermsOfUseByTermsAcceptanceAsync(Guid termsOfUseId, CancellationToken ct = default);
        Task<TermsOfUse> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<TermsOfUse>> ListAsync(Func<IQueryable<TermsOfUse>, IQueryable<TermsOfUse>>? query = null, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(TermsOfUse termsOfUse, CancellationToken ct = default);
        void Update(TermsOfUse termsOfUse);
        void Delete(TermsOfUse termsOfUse, CancellationToken ct = default);

    }
}
