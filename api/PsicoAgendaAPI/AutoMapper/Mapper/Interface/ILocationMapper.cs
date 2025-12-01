
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper.Interface
{
    public interface ILocationMapper 
    {
        Task<LocationViewModel> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<LocationViewModel>> ListAsync(Func<IQueryable<LocationViewModel>, IQueryable<LocationViewModel>>? query = null, CancellationToken ct = default);
        Task AddAsync(LocationViewModel locationViewModel, CancellationToken ct = default);
        void Update(LocationViewModel locationViewModel);
        void Delete(LocationViewModel locationViewModel, CancellationToken ct = default);

    }
}
