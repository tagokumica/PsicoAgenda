using Domain.Entities;

namespace Domain.Interface.Repositories;

public interface IHealthCareProfissionalRepository : IGenericRepository<HealthCareProfissional, Guid>
{
    Task<IEnumerable<HealthCareProfissional>> GetHealthCareProfissionalByAvailabilitiesAsync(Guid profissionalId, CancellationToken ct = default);
    Task<IEnumerable<HealthCareProfissional>> GetHealthCareProfissionalBySessionNotesAsync(Guid profissionalId, CancellationToken ct = default);
    Task<IEnumerable<HealthCareProfissional>> GetHealthCareProfissionalBySessionScheduleAsync(Guid profissionalId, CancellationToken ct = default);

}