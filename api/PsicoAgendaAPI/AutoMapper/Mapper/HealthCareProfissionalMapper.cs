using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper
{
    public class HealthCareProfessionalMapper : IHealthCareProfissionalMapper
    {
        private readonly IMapper _mapper;
        private readonly IHealthCareProfissionalService _healthCareProfissionalService;
        public HealthCareProfessionalMapper(IMapper mapper, IHealthCareProfissionalService healthCareProfissionalService)
        {
            _mapper = mapper;
            _healthCareProfissionalService = healthCareProfissionalService;
        }

        public async Task AddAsync(HealthCareProfissionalViewModel healthCareProfissionalViewModel, CancellationToken ct = default)
        {
            var entity = _mapper.Map<HealthCareProfissional>(healthCareProfissionalViewModel);
            await _healthCareProfissionalService.AddAsync(entity, ct);
        }

        public void Delete(HealthCareProfissionalViewModel healthCareProfissionalViewModel, CancellationToken ct = default)
        {
            var entity = _mapper.Map<HealthCareProfissional>(healthCareProfissionalViewModel);
            _healthCareProfissionalService.Delete(entity, ct);
        }

        public async Task<HealthCareProfissionalViewModel> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _healthCareProfissionalService.GetByIdAsync(id, ct);
            return _mapper.Map<HealthCareProfissionalViewModel>(entity);
        }

        public async Task<IEnumerable<HealthCareProfissionalViewModel>> GetHealthCareProfissionalByAvailabilitiesAsync(Guid professionalId, CancellationToken ct = default)
        {
            var entities = await _healthCareProfissionalService.GetHealthCareProfissionalByAvailabilitiesAsync(professionalId, ct);
            return _mapper.Map<IEnumerable<HealthCareProfissionalViewModel>>(entities);
        }

        public async Task<IEnumerable<HealthCareProfissionalViewModel>> GetHealthCareProfissionalBySessionNotesAsync(Guid professionalId, CancellationToken ct = default)
        {
            var entities = await _healthCareProfissionalService.GetHealthCareProfissionalBySessionNotesAsync(professionalId, ct);
            return _mapper.Map<IEnumerable<HealthCareProfissionalViewModel>>(entities);
        }

        public async Task<IEnumerable<HealthCareProfissionalViewModel>> GetHealthCareProfissionalBySessionScheduleAsync(Guid professionalId, CancellationToken ct = default)
        {
            var entities = await _healthCareProfissionalService.GetHealthCareProfissionalBySessionScheduleAsync(professionalId, ct);
            return _mapper.Map<IEnumerable<HealthCareProfissionalViewModel>>(entities);
        }

        public async Task<List<HealthCareProfissionalViewModel>> ListAsync(Func<IQueryable<HealthCareProfissionalViewModel>, IQueryable<HealthCareProfissionalViewModel>>? query = null, CancellationToken ct = default)
        {
            var entities = await _healthCareProfissionalService.ListAsync(null, ct);
            var viewModels = _mapper.Map<List<HealthCareProfissionalViewModel>>(entities);
            return query is null ? viewModels : query(viewModels.AsQueryable()).ToList();
        }

        public void Update(HealthCareProfissionalViewModel healthCareProfessionalViewModel)
        {
            var entity = _mapper.Map<HealthCareProfissional>(healthCareProfessionalViewModel);
            _healthCareProfissionalService.Update(entity);
        }
    }
}
