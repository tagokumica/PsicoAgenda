using Domain.Entities;
using Domain.Interface.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class PatientRepository : GenericRepository<Patient, Guid>, IPatientRepository
{
    public PatientRepository(PsicoContext db) : base(db)
    {
    }

    public async Task<IEnumerable<Patient>> GetPatientByConsentAsync(Guid patientId, CancellationToken ct = default)
    {
        return await
            _db
                .Consents
                .AsNoTracking()
                .Include(t => t.Patient)
                .Where(s => s.PatientId == patientId)
                .Select(s => s.Patient)
                .ToListAsync(ct);
    }

    public async Task<IEnumerable<Patient>> GetPatientByAvailabilitiesAsync(Guid patientId, CancellationToken ct = default)
    {
        return await
            _db
                .Availabilities
                .AsNoTracking()
                .Include(t => t.Patient)
                .Where(s => s.PatientId == patientId)
                .Select(s => s.Patient)
                .ToListAsync(ct);
    }

    public async Task<IEnumerable<Patient>> GetPatientByWaitsAsync(Guid patientId, CancellationToken ct = default)
    {
        return await
            _db
                .Waits
                .AsNoTracking()
                .Include(t => t.Patient)
                .Where(s => s.PatientId == patientId)
                .Select(s => s.Patient)
                .ToListAsync(ct);
    }

    public async Task<bool> ExistsByCpfAsync(string cpf, CancellationToken ct = default)
    {
        return await _set
            .AsNoTracking()
            .AnyAsync(p => p.Cpf == cpf, ct);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default)
    {
        return await _set
            .AsNoTracking()
            .AnyAsync(p => p.Email == email, ct);
    }
}