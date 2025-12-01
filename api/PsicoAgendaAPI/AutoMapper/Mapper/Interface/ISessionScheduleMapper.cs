using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper.Interface
{
    public interface ISessionScheduleMapper
    {
        Task<IEnumerable<SessionScheduleViewModel>> GetSessionScheduleBySessionNotesAsync(Guid sessionScheduleId, CancellationToken ct = default);
        Task<IEnumerable<SessionScheduleViewModel>> GetSessionScheduleByWaitsAsync(Guid sessionScheduleId, CancellationToken ct = default);
        Task<SessionScheduleViewModel> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<SessionScheduleViewModel>> ListAsync(Func<IQueryable<SessionScheduleViewModel>, IQueryable<SessionScheduleViewModel>>? query = null, CancellationToken ct = default);
        Task AddAsync(SessionScheduleViewModel sessionScheduleViewModel, CancellationToken ct = default);
        void Update(SessionScheduleViewModel sessionScheduleViewModel);
        void Delete(SessionScheduleViewModel sessionScheduleViewModel, CancellationToken ct = default);

    }
}
