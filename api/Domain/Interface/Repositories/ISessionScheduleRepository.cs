using Domain.Entities;

namespace Domain.Interface.Repositories;

public interface ISessionScheduleRepository : IGenericRepository<SessionSchedule, Guid>
{
    Task<IEnumerable<SessionSchedule>> GetSessionScheduleBySessionNotesAsync(Guid sessionScheduleId, CancellationToken ct = default);
    Task<IEnumerable<SessionSchedule>> GetSessionScheduleByWaitsAsync(Guid sessionScheduleId, CancellationToken ct = default);
}