

using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class WaitService : IWaitService
    {
        private readonly IWaitRepository _waitRepository;

        public WaitService(IWaitRepository waitRepository)
        {
            _waitRepository = waitRepository;
        }

        public async Task AddAsync(Wait wait, CancellationToken ct = default)
        {
            if (wait is null)
                throw new ArgumentNullException(nameof(wait));

            await _waitRepository.AddAsync(wait, ct);
            await _waitRepository.SaveChangesAsync(ct);
        }

        public async void Delete(Wait wait, CancellationToken ct = default)
        {
            await Task.Run(async () =>
            {
                if (wait is null)
                    throw new ArgumentNullException(nameof(wait));
                if (wait.Id == Guid.Empty)
                    throw new ArgumentException("Espera sem Id.");

                var current = await _waitRepository.GetByIdAsync(wait.Id, ct);
                if (current is null)
                    throw new KeyNotFoundException($"Espera {wait.Id} não encontrado.");

                _waitRepository.Remove(wait);
                await _waitRepository.SaveChangesAsync(ct);
            }, ct);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            return _waitRepository.ExistsAsync(id, ct);
        }

        public async Task<Wait> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            var user = await _waitRepository.GetByIdAsync(id, ct);
            return user ?? throw new KeyNotFoundException($"Espera {id} não encontrado.");
        }

        public Task<List<Wait>> ListAsync(Func<IQueryable<Wait>, IQueryable<Wait>>? query = null, CancellationToken ct = default)
        {
            return _waitRepository.ListAsync(query, ct);
        }

        public async void Update(Wait wait)
        {
            await Task.Run(async () =>
            {
                if (wait is null)
                    throw new ArgumentNullException(nameof(wait));
                if (wait.Id == Guid.Empty)
                    throw new ArgumentException("Espera sem Id.");

                var current = await _waitRepository.GetByIdAsync(wait.Id);
                if (current is null)
                    throw new KeyNotFoundException($"Espera {wait.Id} não encontrado.");

                _waitRepository.Update(wait);
                await _waitRepository.SaveChangesAsync();
            });
        }
    }
}
