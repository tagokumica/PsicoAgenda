
using Domain.Entities;
using Domain.Entities.ValueObject;
using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Domain.Notifiers;

namespace Domain.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly INotifier _notifier;
        private readonly IUserRepository _userRepository;

        public PatientService(IPatientRepository patientRepository, INotifier notifier, IUserRepository userRepository)
        {
            _patientRepository = patientRepository;
            _notifier = notifier;
            _userRepository = userRepository;
        }

        public async Task AddAsync(Patient patient, CancellationToken ct = default)
        {
            try
            {
                if (!Document.IsValid(patient.Cpf))
                {
                    _notifier.Handle(new Notification(nameof(patient.Cpf), "CPF inválido."));
                    return;
                }

                if (await _patientRepository.ExistsByCpfAsync(patient.Cpf, ct))
                {
                    _notifier.Handle(new Notification(nameof(patient.Cpf), "CPF já cadastrado."));
                    return;
                }

                if (await _patientRepository.ExistsByEmailAsync(patient.Email, ct) ||
                    await _userRepository.ExistsByEmailAsync(patient.Email, ct))
                {
                    _notifier.Handle(new Notification(nameof(patient.Email), "E-mail já cadastrado."));
                    return;
                }

                await _patientRepository.AddAsync(patient, ct);
                await _patientRepository.SaveChangesAsync(ct);

            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
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

            try
            {
                if (await _patientRepository.ExistsByEmailAsync(patient.Email) ||
                    await _userRepository.ExistsByEmailAsync(patient.Email))
                {
                    _notifier.Handle(new Notification(nameof(patient.Email), "E-mail já cadastrado."));
                    return;
                }

                _patientRepository.Update(patient);
                await _patientRepository.SaveChangesAsync();

            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Paciente sem Id.", e);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException($"Paciente {patient.Id} não encontrado.", e);
            }

            await Task.CompletedTask;
        }
    }
}
