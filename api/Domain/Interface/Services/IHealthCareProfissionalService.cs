
using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface IHealthCareProfissionalService
    {
        Task<IEnumerable<HealthCareProfissional>> GetHealthCareProfissionalByAvailabilitiesAsync(Guid profissionalId, CancellationToken ct = default);
        Task<IEnumerable<HealthCareProfissional>> GetHealthCareProfissionalBySessionNotesAsync(Guid profissionalId, CancellationToken ct = default);
        Task<IEnumerable<HealthCareProfissional>> GetHealthCareProfissionalBySessionScheduleAsync(Guid profissionalId, CancellationToken ct = default);
        Task<HealthCareProfissional> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<HealthCareProfissional>> ListAsync(Func<IQueryable<HealthCareProfissional>, IQueryable<HealthCareProfissional>>? query = null, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(HealthCareProfissional healthCareProfissional, CancellationToken ct = default);
        void Update(HealthCareProfissional healthCareProfissional);
        void Delete(HealthCareProfissional healthCareProfissional, CancellationToken ct = default);

    }
}
