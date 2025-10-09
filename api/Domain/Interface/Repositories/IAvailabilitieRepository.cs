using Domain.Entities;

namespace Domain.Interface.Repositories;

public interface IAvailabilitieRepository : IGenericRepository<Availabilitie, Guid>
{
    Task<IEnumerable<Availabilitie>> IsBookedAsync(CancellationToken ct = default);
    Task<IEnumerable<Availabilitie>> IsNotBookedAsync(CancellationToken ct = default);

}