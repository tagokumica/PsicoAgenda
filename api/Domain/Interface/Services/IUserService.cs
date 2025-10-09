
using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<User>> ListAsync(Func<IQueryable<User>, IQueryable<User>>? query = null, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(User user, CancellationToken ct = default);
        void Update(User user);
        void Delete(User user, CancellationToken ct = default);

    }
}
