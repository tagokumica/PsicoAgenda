
using Domain.Entities;

namespace Domain.Interface.Repositories
{
    public interface IUserRepository : IGenericRepository<User, Guid>
    {
        Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default);
    }
}
