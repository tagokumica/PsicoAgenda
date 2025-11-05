using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper
{
    public class PatientMapper : IPatientMapper
    {
        private readonly IMapper _mapper;
        private readonly IPatientService _patientService;
        public PatientMapper(IMapper mapper, IPatientService patientService)
        {
            _mapper = mapper;
            _patientService = patientService;
        }

        public async Task AddAsync(PatientViewModel patientViewModel, CancellationToken ct = default)
        {
            var entity = _mapper.Map<Patient>(patientViewModel);
            await _patientService.AddAsync(entity, ct);
        }

        public void Delete(PatientViewModel patientViewModel, CancellationToken ct = default)
        {
            var entity = _mapper.Map<Patient>(patientViewModel);
            _patientService.Delete(entity, ct);
        }

        public async Task<PatientViewModel> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _patientService.GetByIdAsync(id, ct);
            return _mapper.Map<PatientViewModel>(entity);
        }

        public async Task<IEnumerable<PatientViewModel>> GetPatientByAvailabilitiesAsync(Guid patientId, CancellationToken ct = default)
        {
            var entities = await _patientService.GetPatientByAvailabilitiesAsync(patientId, ct);
            return _mapper.Map<IEnumerable<PatientViewModel>>(entities);
        }

        public async Task<IEnumerable<PatientViewModel>> GetPatientByConsentAsync(Guid patientId, CancellationToken ct = default)
        {
            var entities = await _patientService.GetPatientByConsentAsync(patientId, ct);
            return _mapper.Map<IEnumerable<PatientViewModel>>(entities);
        }

        public async Task<IEnumerable<PatientViewModel>> GetPatientByWaitsAsync(Guid patientId, CancellationToken ct = default)
        {
            var entities = await _patientService.GetPatientByWaitsAsync(patientId, ct);
            return _mapper.Map<IEnumerable<PatientViewModel>>(entities);
        }

        public async Task<List<PatientViewModel>> ListAsync(Func<IQueryable<PatientViewModel>, IQueryable<PatientViewModel>>? query = null, CancellationToken ct = default)
        {
            var entities = await _patientService.ListAsync(null, ct);
            var viewModels = _mapper.Map<List<PatientViewModel>>(entities);
            return query is null ? viewModels : query(viewModels.AsQueryable()).ToList();
        }

        public void Update(PatientViewModel patientViewModel)
        {
            var entity = _mapper.Map<Patient>(patientViewModel);
            _patientService.Update(entity);
        }
    }
}
