
using Domain.Entities;

namespace Domain.Interface.Services
{
    public interface ISessionNoteService
    {
        Task<SessionNote> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<SessionNote>> ListAsync(Func<IQueryable<SessionNote>, IQueryable<SessionNote>>? query = null, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(SessionNote sessionNote, CancellationToken ct = default);
        void Update(SessionNote sessionNote);
        void Delete(SessionNote sessionNote, CancellationToken ct = default);

    }
}
