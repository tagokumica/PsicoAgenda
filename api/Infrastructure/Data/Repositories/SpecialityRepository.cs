using Domain.Entities;
using Domain.Interface.Repositories;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories
{
    public class SpecialityRepository : GenericRepository<Speciality, Guid>, ISpecialityRepository
    {
        public SpecialityRepository(PsicoContext db) : base(db)
        {
            
        }
    }
}
