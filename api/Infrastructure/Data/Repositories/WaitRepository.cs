using Domain.Entities;
using Domain.Interface.Repositories;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories
{
    public class WaitRepository : GenericRepository<Wait, Guid>, IWaitRepository
    {
        public WaitRepository(PsicoContext db) : base(db)
        {
        }
    }
}
