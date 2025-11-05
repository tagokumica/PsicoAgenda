using Domain.Entities;

namespace Domain.Interface.Repositories;

public interface IPatientRepository : IGenericRepository<Patient, Guid>
{
    Task<IEnumerable<Patient>> GetPatientByConsentAsync(Guid patientId, CancellationToken ct = default);
    Task<IEnumerable<Patient>> GetPatientByAvailabilitiesAsync(Guid patientId, CancellationToken ct = default);
    Task<IEnumerable<Patient>> GetPatientByWaitsAsync(Guid patientId, CancellationToken ct = default);
    Task<bool> ExistsByCpfAsync(string cpf, CancellationToken ct = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default);


}