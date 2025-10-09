using Domain.Entities;
using Domain.Interface.Repositories;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories
{
    public class TermAcceptanceRepository : GenericRepository<TermsAcceptance, Guid>, ITermsAcceptanceRepository
    {
        public TermAcceptanceRepository(PsicoContext db) : base(db)
        {
        }
    }
}
