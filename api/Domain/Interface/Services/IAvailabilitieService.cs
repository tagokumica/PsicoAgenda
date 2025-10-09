
using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface IAvailabilitieService
    {
        Task<IEnumerable<Availabilitie>> IsBookedAsync(CancellationToken ct = default);
        Task<IEnumerable<Availabilitie>> IsNotBookedAsync(CancellationToken ct = default);
        Task<Availabilitie> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<Availabilitie>> ListAsync(Func<IQueryable<Availabilitie>, IQueryable<Availabilitie>>? query = null, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(Availabilitie availabilitie, CancellationToken ct = default);
        void Update(Availabilitie availabilitie);
        void Delete(Availabilitie availabilitie, CancellationToken ct = default);

    }
}
