using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper
{
    public class AvailabilitieMapper : IAvailabilitieMapper
    {
        private readonly IMapper _mapper;
        private readonly IAvailabilitieService _availabilitieService;
        public AvailabilitieMapper(IMapper mapper, IAvailabilitieService availabilitieService)
        {
            _mapper = mapper;
            _availabilitieService = availabilitieService;
        }

        public async Task AddAsync(AvailabilitieViewModel availabilitieViewModel, CancellationToken ct = default)
        {
            var entity = _mapper.Map<Availabilitie>(availabilitieViewModel);
            await _availabilitieService.AddAsync(entity, ct);
        }

        public void Delete(AvailabilitieViewModel availabilitieViewModel, CancellationToken ct = default)
        {
            var entity = _mapper.Map<Availabilitie>(availabilitieViewModel); 
            _availabilitieService.Delete(entity, ct);
        }

        public async Task<AvailabilitieViewModel> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _availabilitieService.GetByIdAsync(id, ct);
            return _mapper.Map<AvailabilitieViewModel>(entity);

        }

        public async Task<IEnumerable<AvailabilitieViewModel>> IsBookedAsync(CancellationToken ct = default)
        {
            var entities = await _availabilitieService.IsBookedAsync(ct);
            return _mapper.Map<IEnumerable<AvailabilitieViewModel>>(entities);
        }

        public async Task<IEnumerable<AvailabilitieViewModel>> IsNotBookedAsync(CancellationToken ct = default)
        {
            var entities = await _availabilitieService.IsNotBookedAsync(ct);
            return _mapper.Map<IEnumerable<AvailabilitieViewModel>>(entities);
        }

        public async Task<List<AvailabilitieViewModel>> ListAsync(Func<IQueryable<AvailabilitieViewModel>, IQueryable<AvailabilitieViewModel>>? query = null, CancellationToken ct = default)
        {
            var entities = await _availabilitieService.ListAsync(null, ct);
            var viewModels = _mapper.Map<List<AvailabilitieViewModel>>(entities);
            return query is null ? viewModels : query(viewModels.AsQueryable()).ToList();
        }

        public void Update(AvailabilitieViewModel availabilitieViewModel)
        {
            var entity = _mapper.Map<Availabilitie>(availabilitieViewModel);
            _availabilitieService.Update(entity);
        }
    }
}
