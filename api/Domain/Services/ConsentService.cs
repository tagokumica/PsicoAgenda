
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class ConsentService : IConsentService
    {
        private readonly IConsentRepository _consentRepository;

        public ConsentService(IConsentRepository consentRepository)
        {
            _consentRepository = consentRepository;
        }

        public async Task AddAsync(Consent consent, CancellationToken ct = default)
        {
            if (consent is null)
                throw new ArgumentNullException(nameof(consent));

            await _consentRepository.AddAsync(consent, ct);
            await _consentRepository.SaveChangesAsync(ct);
        }

        public async void Delete(Consent consent, CancellationToken ct = default)
        {
            await Task.Run(async () =>
            {
                if (consent is null)
                    throw new ArgumentNullException(nameof(consent));
                if (consent.Id == Guid.Empty)
                    throw new ArgumentException("Consentimento sem Id.");

                var current = await _consentRepository.GetByIdAsync(consent.Id, ct);
                if (current is null)
                    throw new KeyNotFoundException($"Consentimento {consent.Id} não encontrado.");

                _consentRepository.Remove(consent);
                await _consentRepository.SaveChangesAsync(ct);
            }, ct);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            return _consentRepository.ExistsAsync(id, ct);
        }

        public async Task<Consent> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            var consent = await _consentRepository.GetByIdAsync(id, ct);
            return consent ?? throw new KeyNotFoundException($"Consentimento {id} não encontrado.");
        }

        public Task<List<Consent>> ListAsync(Func<IQueryable<Consent>, IQueryable<Consent>>? query = null, CancellationToken ct = default)
        {
            return _consentRepository.ListAsync(query, ct);
        }

        public async void Update(Consent consent)
        {
            await Task.Run(async () =>
            {
                if (consent is null)
                    throw new ArgumentNullException(nameof(consent));
                if (consent.Id == Guid.Empty)
                    throw new ArgumentException("Consentimento sem Id.");

                var exists = await _consentRepository.ExistsAsync(consent.Id);
                if (!exists)
                    throw new KeyNotFoundException($"Consentimento {consent.Id} não encontrado.");

                _consentRepository.Update(consent);
                await _consentRepository.SaveChangesAsync();
            });
        }
    }
}
