
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task AddAsync(Patient patient, CancellationToken ct = default)
        {
            if (patient is null)
                throw new ArgumentNullException(nameof(patient));

            await _patientRepository.AddAsync(patient, ct);
            await _patientRepository.SaveChangesAsync(ct);
        }

        public async void Delete(Patient patient, CancellationToken ct = default)
        {
            await Task.Run(async () =>
            {
                if (patient is null)
                    throw new ArgumentNullException(nameof(patient));
                if (patient.Id == Guid.Empty)
                    throw new ArgumentException("Paciente sem Id.");

                var current = await _patientRepository.GetByIdAsync(patient.Id, ct);
                if (current is null)
                    throw new KeyNotFoundException($"Paciente {patient.Id} não encontrado.");

                _patientRepository.Remove(patient);
                await _patientRepository.SaveChangesAsync(ct);
            }, ct);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            return _patientRepository.ExistsAsync(id, ct);
        }

        public async Task<Patient> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            var patient = await _patientRepository.GetByIdAsync(id, ct);
            return patient ?? throw new KeyNotFoundException($"Paciente {id} não encontrado.");
        }

        public async Task<IEnumerable<Patient>> GetPatientByAvailabilitiesAsync(Guid patientId, CancellationToken ct = default)
        {
            return await _patientRepository.GetPatientByAvailabilitiesAsync(patientId, ct);
        }

        public async Task<IEnumerable<Patient>> GetPatientByConsentAsync(Guid patientId, CancellationToken ct = default)
        {
            return await _patientRepository.GetPatientByConsentAsync(patientId, ct);
        }

        public async Task<IEnumerable<Patient>> GetPatientByWaitsAsync(Guid patientId, CancellationToken ct = default)
        {
            return await _patientRepository.GetPatientByWaitsAsync(patientId, ct);
        }

        public Task<List<Patient>> ListAsync(Func<IQueryable<Patient>, IQueryable<Patient>>? query = null, CancellationToken ct = default)
        {
            return _patientRepository.ListAsync(query, ct);
        }

        public async void Update(Patient patient)
        {
            await Task.Run(async () =>
            {
                if (patient is null)
                    throw new ArgumentNullException(nameof(patient));
                if (patient.Id == Guid.Empty)
                    throw new ArgumentException("Disponibilidade sem Id.");

                var exists = await _patientRepository.ExistsAsync(patient.Id);
                if (!exists)
                    throw new KeyNotFoundException($"Disponibilidade {patient.Id} não encontrado.");

                _patientRepository.Update(patient);
                await _patientRepository.SaveChangesAsync();
            });
        }
    }
}
