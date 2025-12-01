using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper.Interface
{
    public interface IConsentMapper 
    {
        Task<ConsentViewModel> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<ConsentViewModel>> ListAsync(Func<IQueryable<ConsentViewModel>, IQueryable<ConsentViewModel>>? query = null, CancellationToken ct = default);
        Task AddAsync(ConsentViewModel consentViewModel, CancellationToken ct = default);
        void Update(ConsentViewModel consentViewModel);
        void Delete(ConsentViewModel consentViewModel, CancellationToken ct = default);

    }
}
