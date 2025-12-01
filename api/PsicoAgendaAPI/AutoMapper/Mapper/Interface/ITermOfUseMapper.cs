using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper.Interface
{
    public interface ITermOfUseMapper 
    {
        Task<IEnumerable<TermsOfUseViewModel>> GetTermsOfUseByTermsAcceptanceAsync(Guid termsOfUseId, CancellationToken ct = default);
        Task<TermsOfUseViewModel> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<TermsOfUseViewModel>> ListAsync(Func<IQueryable<TermsOfUseViewModel>, IQueryable<TermsOfUseViewModel>>? query = null, CancellationToken ct = default);
        Task AddAsync(TermsOfUseViewModel termsOfUseViewModel, CancellationToken ct = default);
        void Update(TermsOfUseViewModel termsOfUseViewModel);
        void Delete(TermsOfUseViewModel termsOfUseViewModel, CancellationToken ct = default);

    }
}
