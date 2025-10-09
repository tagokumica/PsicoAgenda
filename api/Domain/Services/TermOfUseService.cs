

using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class TermOfUseService : ITermOfUseService
    {
        private readonly ITermOfUseRepository _termOfUseRepository;
        public TermOfUseService(ITermOfUseRepository termOfUseRepository)
        {
            _termOfUseRepository = termOfUseRepository;
        }
        public async Task AddAsync(TermsOfUse termsOfUse, CancellationToken ct = default)
        {
            if (termsOfUse is null)
                throw new ArgumentNullException(nameof(termsOfUse));

            await _termOfUseRepository.AddAsync(termsOfUse, ct);
            await _termOfUseRepository.SaveChangesAsync(ct);
        }

        public async void Delete(TermsOfUse termsOfUse, CancellationToken ct = default)
        {
            await Task.Run(async () =>
            {
                if (termsOfUse is null)
                    throw new ArgumentNullException(nameof(termsOfUse));
                if (termsOfUse.Id == Guid.Empty)
                    throw new ArgumentException("Termo de Uso sem Id.");

                var current = await _termOfUseRepository.GetByIdAsync(termsOfUse.Id, ct);
                if (current is null)
                    throw new KeyNotFoundException($"Termo de Uso {termsOfUse.Id} não encontrado.");

                _termOfUseRepository.Remove(termsOfUse);
                await _termOfUseRepository.SaveChangesAsync(ct);
            }, ct);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            return _termOfUseRepository.ExistsAsync(id, ct);
        }

        public async Task<TermsOfUse> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            var termsOfUse = await _termOfUseRepository.GetByIdAsync(id, ct);
            return termsOfUse ?? throw new KeyNotFoundException($"Termo de Uso {id} não encontrado.");
        }

        public Task<IEnumerable<TermsOfUse>> GetTermsOfUseByTermsAcceptanceAsync(Guid termsOfUseId, CancellationToken ct = default)
        {
            return _termOfUseRepository.GetTermsOfUseByTermsAcceptanceAsync(termsOfUseId, ct);
        }

        public Task<List<TermsOfUse>> ListAsync(Func<IQueryable<TermsOfUse>, IQueryable<TermsOfUse>>? query = null, CancellationToken ct = default)
        {
            return _termOfUseRepository.ListAsync(query, ct);
        }

        public async void Update(TermsOfUse termsOfUse)
        {
            await Task.Run(async () =>
            {
                if (termsOfUse is null)
                    throw new ArgumentNullException(nameof(termsOfUse));
                if (termsOfUse.Id == Guid.Empty)
                    throw new ArgumentException("Termo de Uso sem Id.");

                var current = await _termOfUseRepository.GetByIdAsync(termsOfUse.Id);
                if (current is null)
                    throw new KeyNotFoundException($"Termo de Uso {termsOfUse.Id} não encontrado.");

                _termOfUseRepository.Update(termsOfUse);
                await _termOfUseRepository.SaveChangesAsync();
            });
        }
    }
}