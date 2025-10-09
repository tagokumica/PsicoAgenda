
using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface ISpecialityService
    {
        Task<Speciality> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<Speciality>> ListAsync(Func<IQueryable<Speciality>, IQueryable<Speciality>>? query = null, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(Speciality speciality, CancellationToken ct = default);
        void Update(Speciality speciality);
        void Delete(Speciality speciality, CancellationToken ct = default);

    }
}
