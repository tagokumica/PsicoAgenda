
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper.Interface
{
    public interface IWaitMapper
    {
        Task<WaitViewModel> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<WaitViewModel>> ListAsync(Func<IQueryable<WaitViewModel>, IQueryable<WaitViewModel>>? query = null, CancellationToken ct = default);
        Task AddAsync(WaitViewModel waitViewModel, CancellationToken ct = default);
        void Update(WaitViewModel waitViewModel);
        void Delete(WaitViewModel waitViewModel, CancellationToken ct = default);

    }
}
