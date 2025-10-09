
using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface IConsentService
    {
        Task<Consent> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<Consent>> ListAsync(Func<IQueryable<Consent>, IQueryable<Consent>>? query = null, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(Consent consent, CancellationToken ct = default);
        void Update(Consent consent);
        void Delete(Consent consent, CancellationToken ct = default);

    }
}
