using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface ISessionScheduleService
    {
        Task<IEnumerable<SessionSchedule>> GetSessionScheduleBySessionNotesAsync(Guid sessionScheduleId, CancellationToken ct = default);
        Task<IEnumerable<SessionSchedule>> GetSessionScheduleByWaitsAsync(Guid sessionScheduleId, CancellationToken ct = default);
        Task<SessionSchedule> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<SessionSchedule>> ListAsync(Func<IQueryable<SessionSchedule>, IQueryable<SessionSchedule>>? query = null, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(SessionSchedule sessionSchedule, CancellationToken ct = default);
        void Update(SessionSchedule sessionSchedule);
        void Delete(SessionSchedule sessionSchedule, CancellationToken ct = default);
        Task<int> GetSessionScheduleByDurationCountAsync(TimeSpan durationMinute, CancellationToken ct = default);
        Task<IEnumerable<SessionSchedule>> GetUpcomingSessionsAsync(TimeSpan duration, CancellationToken ct = default);
    }
}
