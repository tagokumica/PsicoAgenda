
using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface IWaitService
    {
        Task<Wait> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<Wait>> ListAsync(Func<IQueryable<Wait>, IQueryable<Wait>>? query = null, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(Wait wait, CancellationToken ct = default);
        void Update(Wait wait);
        void Delete(Wait wait, CancellationToken ct = default);

    }
}
