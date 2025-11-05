

using Domain.Entities;
using Domain.Interface.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : GenericRepository<User, Guid>, IUserRepository
    {
        public UserRepository(PsicoContext db) : base(db)
        {
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default)
        {
            return await _set
                .AsNoTracking()
                .AnyAsync(p => p.Email == email, ct);
        }
    }
}
