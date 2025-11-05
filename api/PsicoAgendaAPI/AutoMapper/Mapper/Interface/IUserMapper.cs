using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper.Interface
{
    public interface IUserMapper 
    {
        Task<UserViewModel> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<UserViewModel>> ListAsync(Func<IQueryable<UserViewModel>, IQueryable<UserViewModel>>? query = null, CancellationToken ct = default);
        Task AddAsync(UserViewModel userViewModel, CancellationToken ct = default);
        void Update(UserViewModel userViewModel);
        void Delete(UserViewModel userViewModel, CancellationToken ct = default);
    }
}
