using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper.Interface
{
    public interface ISessionNoteMapper 
    {
        Task<SessionNoteViewModel> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<SessionNoteViewModel>> ListAsync(Func<IQueryable<SessionNoteViewModel>, IQueryable<SessionNoteViewModel>>? query = null, CancellationToken ct = default);
        Task AddAsync(SessionNoteViewModel sessionNoteViewModel, CancellationToken ct = default);
        void Update(SessionNoteViewModel sessionNoteViewModel);
        void Delete(SessionNoteViewModel sessionNoteViewModel, CancellationToken ct = default);

    }
}
