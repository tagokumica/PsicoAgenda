using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper.Interface
{
    public interface IAvailabilitieMapper
    {
        Task<IEnumerable<AvailabilitieViewModel>> IsBookedAsync(CancellationToken ct = default);
        Task<IEnumerable<AvailabilitieViewModel>> IsNotBookedAsync(CancellationToken ct = default);
        Task<AvailabilitieViewModel> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<AvailabilitieViewModel>> ListAsync(Func<IQueryable<AvailabilitieViewModel>, IQueryable<AvailabilitieViewModel>>? query = null, CancellationToken ct = default);
        Task AddAsync(AvailabilitieViewModel availabilitieViewModel, CancellationToken ct = default);
        void Update(AvailabilitieViewModel availabilitieViewModel);
        void Delete(AvailabilitieViewModel availabilitieViewModel, CancellationToken ct = default);
    }
}
