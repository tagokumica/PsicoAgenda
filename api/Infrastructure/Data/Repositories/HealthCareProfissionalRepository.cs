using Domain.Entities;
using Domain.Interface.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class HealthCareProfissionalRepository : GenericRepository<HealthCareProfissional, Guid>, IHealthCareProfissionalRepository
{
    public HealthCareProfissionalRepository(PsicoContext db) : base(db)
    {
    }


    public async Task<IEnumerable<HealthCareProfissional>> GetHealthCareProfissionalByAvailabilitiesAsync(Guid profissionalId, CancellationToken ct = default)
    {
        return await
            _db
                .Availabilities
                .Include(t => t.HealthCareProfissional)
                .Where(s => s.ProfissionalId == profissionalId)
                .Select(s => s.HealthCareProfissional)
                .ToListAsync(ct);
    }

    public async Task<IEnumerable<HealthCareProfissional>> GetHealthCareProfissionalBySessionNotesAsync(Guid profissionalId, CancellationToken ct = default)
    {
        return await
            _db
                .SessionNotes
                .Include(t => t.HealthCareProfissional)
                .Where(s => s.ProfessionalId == profissionalId)
                .Select(s => s.HealthCareProfissional)
                .ToListAsync(ct);
    }

    public async Task<IEnumerable<HealthCareProfissional>> GetHealthCareProfissionalBySessionScheduleAsync(Guid profissionalId, CancellationToken ct = default)
    {
        return await
            _db
                .SessionSchedules
                .Include(t => t.HealthCareProfissional)
                .Where(s => s.ProfessionalId == profissionalId)
                .Select(s => s.HealthCareProfissional)
                .ToListAsync(ct);
    }
}