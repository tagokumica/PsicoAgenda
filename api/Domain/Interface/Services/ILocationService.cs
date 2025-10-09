
using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface ILocationService
    {
        Task<Location> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<Location>> ListAsync(Func<IQueryable<Location>, IQueryable<Location>>? query = null, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(Location location, CancellationToken ct = default);
        void Update(Location location);
        void Delete(Location location, CancellationToken ct = default);
    }
}
