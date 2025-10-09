using Domain.Entities;
using Domain.Interface.Repositories;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;

public class LocationRepository : GenericRepository<Location, Guid>, ILocationRepository
{
    public LocationRepository(PsicoContext db) : base(db)
    {
    }
}