using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class AvailabilitieService : IAvailabilitieService
    {
        private readonly IAvailabilitieRepository _availabilitieRepository;

        public AvailabilitieService(IAvailabilitieRepository availabilitieRepository)
        {
            _availabilitieRepository = availabilitieRepository;
        }

        public async Task AddAsync(Availabilitie availabilitie, CancellationToken ct = default)
        {
            if (availabilitie is null)
                throw new ArgumentNullException(nameof(availabilitie));

            await _availabilitieRepository.AddAsync(availabilitie, ct);
            await _availabilitieRepository.SaveChangesAsync(ct);
        }

        public async void Delete(Availabilitie availabilitie, CancellationToken ct)
        {
            await Task.Run(async () =>
            {
                if (availabilitie is null)
                    throw new ArgumentNullException(nameof(availabilitie));
                if (availabilitie.Id == Guid.Empty)
                    throw new ArgumentException("Disponibilidade sem Id.");

                var current = await _availabilitieRepository.GetByIdAsync(availabilitie.Id, ct);
                if (current is null)
                    throw new KeyNotFoundException($"Disponibilidade {availabilitie.Id} não encontrado.");

                _availabilitieRepository.Remove(availabilitie);
                await _availabilitieRepository.SaveChangesAsync(ct);
            }, ct);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            return _availabilitieRepository.ExistsAsync(id, ct);
        }

        public async Task<IEnumerable<Availabilitie>> IsBookedAsync(CancellationToken ct = default)
        {
            return await _availabilitieRepository.IsBookedAsync(ct);
        }

        public async Task<IEnumerable<Availabilitie>> IsNotBookedAsync(CancellationToken ct = default)
        {
            return await _availabilitieRepository.IsNotBookedAsync(ct);
        }

        public async Task<Availabilitie> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            var availabilitie = await _availabilitieRepository.GetByIdAsync(id, ct);
            return availabilitie ?? throw new KeyNotFoundException($"Disponibilidade {id} não encontrado.");
        }

        public Task<List<Availabilitie>> ListAsync(Func<IQueryable<Availabilitie>, IQueryable<Availabilitie>>? query = null, CancellationToken ct = default)
        {
            return _availabilitieRepository.ListAsync(query, ct);
        }

        public async void Update(Availabilitie availabilitie)
        {
            await Task.Run(async () =>
            {
                if (availabilitie is null)
                    throw new ArgumentNullException(nameof(availabilitie));
                if (availabilitie.Id == Guid.Empty)
                    throw new ArgumentException("Disponibilidade sem Id.");

                var exists = await _availabilitieRepository.ExistsAsync(availabilitie.Id);
                if (!exists)
                    throw new KeyNotFoundException($"Disponibilidade {availabilitie.Id} não encontrado.");

                _availabilitieRepository.Update(availabilitie);
                await _availabilitieRepository.SaveChangesAsync();
            });
        }
    }
}
