
using Domain.Entities;

namespace Domain.Interface.Repositories
{
    public interface IUserRepository : IGenericRepository<User, Guid>
    {
    }
}
