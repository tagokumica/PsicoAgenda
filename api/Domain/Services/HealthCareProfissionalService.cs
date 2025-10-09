
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class HealthCareProfissionalService : IHealthCareProfissionalService
    {
        private readonly IHealthCareProfissionalRepository _healthCareProfissionalRepository;
        public HealthCareProfissionalService(IHealthCareProfissionalRepository healthCareProfissionalRepository)
        {
           _healthCareProfissionalRepository = healthCareProfissionalRepository; 
        }
        public async Task AddAsync(HealthCareProfissional healthCareProfissional, CancellationToken ct = default)
        {
            if (healthCareProfissional is null)
                throw new ArgumentNullException(nameof(healthCareProfissional));

            await _healthCareProfissionalRepository.AddAsync(healthCareProfissional, ct);
            await _healthCareProfissionalRepository.SaveChangesAsync(ct);
        }

        public async void Delete(HealthCareProfissional healthCareProfissional, CancellationToken ct = default)
        {
            await Task.Run(async () =>
            {
                if (healthCareProfissional is null)
                    throw new ArgumentNullException(nameof(healthCareProfissional));
                if (healthCareProfissional.Id == Guid.Empty)
                    throw new ArgumentException("Profissional sem Id.");

                var current = await _healthCareProfissionalRepository.GetByIdAsync(healthCareProfissional.Id, ct);
                if (current is null)
                    throw new KeyNotFoundException($"Profissional {healthCareProfissional.Id} não encontrado.");

                _healthCareProfissionalRepository.Remove(healthCareProfissional);
                await _healthCareProfissionalRepository.SaveChangesAsync(ct);
            }, ct);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            return _healthCareProfissionalRepository.ExistsAsync(id, ct);
        }

        public async Task<HealthCareProfissional> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            var healthCareProfissional = await _healthCareProfissionalRepository.GetByIdAsync(id, ct);
            return healthCareProfissional ?? throw new KeyNotFoundException($"Profissional {id} não encontrado.");
        }

        public async Task<IEnumerable<HealthCareProfissional>> GetHealthCareProfissionalByAvailabilitiesAsync(Guid profissionalId, CancellationToken ct = default)
        {
            return await _healthCareProfissionalRepository.GetHealthCareProfissionalByAvailabilitiesAsync(profissionalId, ct);
        }

        public async Task<IEnumerable<HealthCareProfissional>> GetHealthCareProfissionalBySessionNotesAsync(Guid profissionalId, CancellationToken ct = default)
        {
            return await _healthCareProfissionalRepository.GetHealthCareProfissionalBySessionNotesAsync(profissionalId, ct);
        }

        public async Task<IEnumerable<HealthCareProfissional>> GetHealthCareProfissionalBySessionScheduleAsync(Guid profissionalId, CancellationToken ct = default)
        {
            return await _healthCareProfissionalRepository.GetHealthCareProfissionalBySessionScheduleAsync(profissionalId, ct);
        }

        public async Task<List<HealthCareProfissional>> ListAsync(Func<IQueryable<HealthCareProfissional>, IQueryable<HealthCareProfissional>>? query = null, CancellationToken ct = default)
        {
            return await _healthCareProfissionalRepository.ListAsync(query, ct);
        }

        public async void Update(HealthCareProfissional healthCareProfissional)
        {
            await Task.Run(async () =>
            {
                if (healthCareProfissional is null)
                    throw new ArgumentNullException(nameof(healthCareProfissional));
                if (healthCareProfissional.Id == Guid.Empty)
                    throw new ArgumentException("Profissional sem Id.");

                var exists = await _healthCareProfissionalRepository.ExistsAsync(healthCareProfissional.Id);
                if (!exists)
                    throw new KeyNotFoundException($"Profissional {healthCareProfissional.Id} não encontrado.");

                _healthCareProfissionalRepository.Update(healthCareProfissional);
                await _healthCareProfissionalRepository.SaveChangesAsync();
            });
        }
    }
}
