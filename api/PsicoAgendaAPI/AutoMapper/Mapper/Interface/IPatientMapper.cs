
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper.Interface
{
    public interface IPatientMapper 
    {

        Task<IEnumerable<PatientViewModel>> GetPatientByConsentAsync(Guid patientId, CancellationToken ct = default);
        Task<IEnumerable<PatientViewModel>> GetPatientByAvailabilitiesAsync(Guid patientId, CancellationToken ct = default);
        Task<IEnumerable<PatientViewModel>> GetPatientByWaitsAsync(Guid patientId, CancellationToken ct = default);
        Task<PatientViewModel> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<PatientViewModel>> ListAsync(Func<IQueryable<PatientViewModel>, IQueryable<PatientViewModel>>? query = null, CancellationToken ct = default);
        Task AddAsync(PatientViewModel patientViewModel, CancellationToken ct = default);
        void Update(PatientViewModel patientViewModel);
        void Delete(PatientViewModel patientViewModel, CancellationToken ct = default);

    }
}
