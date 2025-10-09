using Domain.Entities;
using Domain.Interface.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class SessionScheduleRepository : GenericRepository<SessionSchedule, Guid>, ISessionScheduleRepository
{
    public SessionScheduleRepository(PsicoContext db) : base(db)
    {
    }

    public async Task<IEnumerable<SessionSchedule>> GetSessionScheduleBySessionNotesAsync(Guid sessionScheduleId, CancellationToken ct = default)
    {
        return await
            _db
                .SessionNotes
                .Include(t => t.SessionSchedule)
                .Where(s => s.SessionScheduleId == sessionScheduleId)
                .Select(s => s.SessionSchedule)
                .ToListAsync(ct);

    }

    public async Task<IEnumerable<SessionSchedule>> GetSessionScheduleByWaitsAsync(Guid sessionScheduleId, CancellationToken ct = default)
    {
        return await
            _db
                .Waits
                .Include(t => t.SessionSchedule)
                .Where(s => s.SessionId == sessionScheduleId)
                .Select(s => s.SessionSchedule)
                .ToListAsync(ct);
    }
}