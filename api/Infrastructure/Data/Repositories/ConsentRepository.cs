using Domain.Entities;
using Domain.Interface.Repositories;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;

public class ConsentRepository : GenericRepository<Consent, Guid>, IConsentRepository
{

    public ConsentRepository(PsicoContext db) : base(db)
    {
    }

}