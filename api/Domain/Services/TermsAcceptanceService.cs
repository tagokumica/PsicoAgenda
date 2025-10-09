
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class TermsAcceptanceService : ITermsAcceptanceService
    {
        private readonly ITermsAcceptanceRepository _termsAcceptanceRepository;
        public TermsAcceptanceService(ITermsAcceptanceRepository termsAcceptanceRepository)
        {
            _termsAcceptanceRepository = termsAcceptanceRepository;
        }
        public async Task AddAsync(TermsAcceptance termsAcceptance, CancellationToken ct = default)
        {
            if (termsAcceptance is null)
                throw new ArgumentNullException(nameof(termsAcceptance));

            await _termsAcceptanceRepository.AddAsync(termsAcceptance, ct);
            await _termsAcceptanceRepository.SaveChangesAsync(ct);
        }

        public async void Delete(TermsAcceptance termsAcceptance, CancellationToken ct = default)
        {
            await Task.Run(async () =>
            {
                if (termsAcceptance is null)
                    throw new ArgumentNullException(nameof(termsAcceptance));
                if (termsAcceptance.Id == Guid.Empty)
                    throw new ArgumentException("Aceite dos Termos sem Id.");

                var current = await _termsAcceptanceRepository.GetByIdAsync(termsAcceptance.Id, ct);
                if (current is null)
                    throw new KeyNotFoundException($"Aceite dos Termos {termsAcceptance.Id} não encontrado.");

                _termsAcceptanceRepository.Remove(termsAcceptance);
                await _termsAcceptanceRepository.SaveChangesAsync(ct);
            }, ct);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            return _termsAcceptanceRepository.ExistsAsync(id, ct);
        }

        public async Task<TermsAcceptance> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            var termsAcceptance = await _termsAcceptanceRepository.GetByIdAsync(id, ct);
            return termsAcceptance ?? throw new KeyNotFoundException($"Aceite dos Termos {id} não encontrado.");
        }

        public Task<List<TermsAcceptance>> ListAsync(Func<IQueryable<TermsAcceptance>, IQueryable<TermsAcceptance>>? query = null, CancellationToken ct = default)
        {
            return _termsAcceptanceRepository.ListAsync(query, ct);
        }

        public async void Update(TermsAcceptance termsAcceptance)
        {
            await Task.Run(async () =>
            {
                if (termsAcceptance is null)
                    throw new ArgumentNullException(nameof(termsAcceptance));
                if (termsAcceptance.Id == Guid.Empty)
                    throw new ArgumentException("Aceite dos Termos sem Id.");

                var current = await _termsAcceptanceRepository.GetByIdAsync(termsAcceptance.Id);
                if (current is null)
                    throw new KeyNotFoundException($"Aceite dos Termos {termsAcceptance.Id} não encontrado.");

                _termsAcceptanceRepository.Update(termsAcceptance);
                await _termsAcceptanceRepository.SaveChangesAsync();
            });
        }
    }
}
