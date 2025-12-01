
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper.Interface
{
    public interface ISpecialityMapper 
    {

        Task<SpecialityViewModel> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<SpecialityViewModel>> ListAsync(Func<IQueryable<SpecialityViewModel>, IQueryable<SpecialityViewModel>>? query = null, CancellationToken ct = default);
        Task AddAsync(SpecialityViewModel specialityViewModel, CancellationToken ct = default);
        void Update(SpecialityViewModel specialityViewModel);
        void Delete(SpecialityViewModel specialityViewModel, CancellationToken ct = default);

    }
}
