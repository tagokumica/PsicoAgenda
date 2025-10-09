

using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task AddAsync(Location location, CancellationToken ct = default)
        {
            if (location is null)
                throw new ArgumentNullException(nameof(location));

            await _locationRepository.AddAsync(location, ct);
            await _locationRepository.SaveChangesAsync(ct);
        }

        public async void Delete(Location location, CancellationToken ct = default)
        {
            await Task.Run(async () =>
            {
                if (location is null)
                    throw new ArgumentNullException(nameof(location));
                if (location.Id == Guid.Empty)
                    throw new ArgumentException("Localização sem Id.");

                var current = await _locationRepository.GetByIdAsync(location.Id, ct);
                if (current is null)
                    throw new KeyNotFoundException($"Localização {location.Id} não encontrado.");

                _locationRepository.Remove(location);
                await _locationRepository.SaveChangesAsync(ct);
            }, ct);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            return _locationRepository.ExistsAsync(id, ct);
        }

        public async Task<Location> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            var location = await _locationRepository.GetByIdAsync(id, ct);
            return location ?? throw new KeyNotFoundException($"Localização {id} não encontrado.");
        }

        public Task<List<Location>> ListAsync(Func<IQueryable<Location>, IQueryable<Location>>? query = null, CancellationToken ct = default)
        {
            return _locationRepository.ListAsync(query, ct);
        }

        public async void Update(Location location)
        {
            await Task.Run(async () =>
            {
                if (location is null)
                    throw new ArgumentNullException(nameof(location));
                if (location.Id == Guid.Empty)
                    throw new ArgumentException("Localização sem Id.");

                var exists = await _locationRepository.ExistsAsync(location.Id);
                if (!exists)
                    throw new KeyNotFoundException($"Localização {location.Id} não encontrado.");

                _locationRepository.Update(location);
                await _locationRepository.SaveChangesAsync();
            });
        }
    }
}
