using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper
{
    public class LocationMapper : ILocationMapper
    {
        private readonly IMapper _mapper;
        private readonly ILocationService _locationService;
        public LocationMapper(IMapper mapper, ILocationService locationService)
        {
            _mapper = mapper;
            _locationService = locationService;
        }

        public async Task AddAsync(LocationViewModel locationViewModel, CancellationToken ct = default)
        {
            var entity = _mapper.Map<Location>(locationViewModel);
            await _locationService.AddAsync(entity, ct);
        }

        public void Delete(LocationViewModel locationViewModel, CancellationToken ct = default)
        {
            var entity = _mapper.Map<Location>(locationViewModel);
            _locationService.Delete(entity, ct);
        }

        public async Task<LocationViewModel> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _locationService.GetByIdAsync(id, ct);
            return _mapper.Map<LocationViewModel>(entity);
        }

        public async Task<List<LocationViewModel>> ListAsync(Func<IQueryable<LocationViewModel>, IQueryable<LocationViewModel>>? query = null, CancellationToken ct = default)
        {
            var entities = await _locationService.ListAsync(null, ct);
            var viewModels = _mapper.Map<List<LocationViewModel>>(entities);
            return query is null ? viewModels : query(viewModels.AsQueryable()).ToList();
        }

        public void Update(LocationViewModel locationViewModel)
        {
            var entity = _mapper.Map<Location>(locationViewModel);
            _locationService.Update(entity);
        }
    }
}
