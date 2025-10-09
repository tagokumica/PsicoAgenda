
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class SpecialityService : ISpecialityService
    {
        private readonly ISpecialityRepository _specialityRepository;
        public SpecialityService(ISpecialityRepository specialityRepository)
        {
            _specialityRepository = specialityRepository;
        }
        public async Task AddAsync(Speciality speciality, CancellationToken ct = default)
        {
            if (speciality is null)
                throw new ArgumentNullException(nameof(speciality));

            await _specialityRepository.AddAsync(speciality, ct);
            await _specialityRepository.SaveChangesAsync(ct);
        }

        public async void Delete(Speciality speciality, CancellationToken ct = default)
        {
            await Task.Run(async () =>
            {
                if (speciality is null)
                    throw new ArgumentNullException(nameof(speciality));
                if (speciality.Id == Guid.Empty)
                    throw new ArgumentException("Especialidade sem Id.");

                var current = await _specialityRepository.GetByIdAsync(speciality.Id, ct);
                if (current is null)
                    throw new KeyNotFoundException($"Especialidade {speciality.Id} não encontrado.");

                _specialityRepository.Remove(speciality);
                await _specialityRepository.SaveChangesAsync(ct);
            }, ct);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            return _specialityRepository.ExistsAsync(id, ct);
        }

        public async Task<Speciality> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            var speciality = await _specialityRepository.GetByIdAsync(id, ct);
            return speciality ?? throw new KeyNotFoundException($"Especialidade {id} não encontrado.");
        }

        public Task<List<Speciality>> ListAsync(Func<IQueryable<Speciality>, IQueryable<Speciality>>? query = null, CancellationToken ct = default)
        {
            return _specialityRepository.ListAsync(query, ct);
        }

        public async void Update(Speciality speciality)
        {
            await Task.Run(async () =>
            {
                if (speciality is null)
                    throw new ArgumentNullException(nameof(speciality));
                if (speciality.Id == Guid.Empty)
                    throw new ArgumentException("Especialidade sem Id.");

                var exists = await _specialityRepository.ExistsAsync(speciality.Id);
                if (!exists)
                    throw new KeyNotFoundException($"Especialidade {speciality.Id} não encontrado.");

                _specialityRepository.Update(speciality);
                await _specialityRepository.SaveChangesAsync();
            });
        }
    }
}
