using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper.Interface
{
    public interface ITermsAcceptanceMapper 
    {
        Task<TermsAcceptanceViewModel> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<TermsAcceptanceViewModel>> ListAsync(Func<IQueryable<TermsAcceptanceViewModel>, IQueryable<TermsAcceptanceViewModel>>? query = null, CancellationToken ct = default);
        Task AddAsync(TermsAcceptanceViewModel termsAcceptanceViewModel, CancellationToken ct = default);
        void Update(TermsAcceptanceViewModel termsAcceptanceViewModel);
        void Delete(TermsAcceptanceViewModel termsAcceptanceViewModel, CancellationToken ct = default);
        Task<IEnumerable<TermsAcceptanceViewModel>> GetIsAgreedAsync(CancellationToken ct = default);
        Task<IEnumerable<TermsAcceptanceViewModel>> GetIsNotAgreedAsync(CancellationToken ct = default);
        Task<TermsAcceptanceViewModel> IsAgreedAsync(Guid userId, Guid termsOfUseId, CancellationToken ct = default);
        Task<TermsAcceptanceViewModel> IsNotAgreedAsync(Guid userId, Guid termsOfUseId, CancellationToken ct = default);


    }
}
