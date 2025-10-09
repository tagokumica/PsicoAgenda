
using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetPatientByConsentAsync(Guid patientId, CancellationToken ct = default);
        Task<IEnumerable<Patient>> GetPatientByAvailabilitiesAsync(Guid patientId, CancellationToken ct = default);
        Task<IEnumerable<Patient>> GetPatientByWaitsAsync(Guid patientId, CancellationToken ct = default);
        Task<Patient> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<Patient>> ListAsync(Func<IQueryable<Patient>, IQueryable<Patient>>? query = null, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(Patient patient, CancellationToken ct = default);
        void Update(Patient patient);
        void Delete(Patient patient, CancellationToken ct = default);

    }
}
