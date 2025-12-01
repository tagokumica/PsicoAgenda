using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper.Interface
{
    public interface IHealthCareProfissionalMapper 
    {
        Task<IEnumerable<HealthCareProfissionalViewModel>> GetHealthCareProfissionalByAvailabilitiesAsync(Guid profissionalId, CancellationToken ct = default);
        Task<IEnumerable<HealthCareProfissionalViewModel>> GetHealthCareProfissionalBySessionNotesAsync(Guid profissionalId, CancellationToken ct = default);
        Task<IEnumerable<HealthCareProfissionalViewModel>> GetHealthCareProfissionalBySessionScheduleAsync(Guid profissionalId, CancellationToken ct = default);
        Task<HealthCareProfissionalViewModel> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<HealthCareProfissionalViewModel>> ListAsync(Func<IQueryable<HealthCareProfissionalViewModel>, IQueryable<HealthCareProfissionalViewModel>>? query = null, CancellationToken ct = default);
        Task AddAsync(HealthCareProfissionalViewModel healthCareProfessionalViewModel, CancellationToken ct = default);
        void Update(HealthCareProfissionalViewModel healthCareProfessionalViewModel);
        void Delete(HealthCareProfissionalViewModel healthCareProfessionalViewModel, CancellationToken ct = default);


    }
}
