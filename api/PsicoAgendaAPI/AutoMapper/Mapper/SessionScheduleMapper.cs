using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper
{
    public class SessionScheduleMapper : ISessionScheduleMapper
    {
        private readonly IMapper _mapper;
        private readonly ISessionScheduleService _sessionScheduleService;
        public SessionScheduleMapper(IMapper mapper, ISessionScheduleService sessionScheduleService)
        {
            _mapper = mapper;
            _sessionScheduleService = sessionScheduleService;
        }

        public async Task AddAsync(SessionScheduleViewModel sessionScheduleViewModel, CancellationToken ct = default)
        {
            var entity = _mapper.Map<SessionSchedule>(sessionScheduleViewModel);
            await _sessionScheduleService.AddAsync(entity, ct);
        }

        public void Delete(SessionScheduleViewModel sessionScheduleViewModel, CancellationToken ct = default)
        {
            var entity = _mapper.Map<SessionSchedule>(sessionScheduleViewModel);
            _sessionScheduleService.Delete(entity, ct);
        }

        public async Task<SessionScheduleViewModel> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _sessionScheduleService.GetByIdAsync(id, ct);
            return _mapper.Map<SessionScheduleViewModel>(entity);
        }

        public async Task<IEnumerable<SessionScheduleViewModel>> GetSessionScheduleBySessionNotesAsync(Guid sessionScheduleId, CancellationToken ct = default)
        {
            var entities = await _sessionScheduleService.GetSessionScheduleBySessionNotesAsync(sessionScheduleId, ct);
            return _mapper.Map<IEnumerable<SessionScheduleViewModel>>(entities);
        }

        public async Task<IEnumerable<SessionScheduleViewModel>> GetSessionScheduleByWaitsAsync(Guid sessionScheduleId, CancellationToken ct = default)
        {
            var entities = await _sessionScheduleService.GetSessionScheduleByWaitsAsync(sessionScheduleId, ct);
            return _mapper.Map<IEnumerable<SessionScheduleViewModel>>(entities);
        }

        public async Task<List<SessionScheduleViewModel>> ListAsync(Func<IQueryable<SessionScheduleViewModel>, IQueryable<SessionScheduleViewModel>>? query = null, CancellationToken ct = default)
        {
            var entities = await _sessionScheduleService.ListAsync(null, ct);
            var viewModels = _mapper.Map<List<SessionScheduleViewModel>>(entities);
            return query is null ? viewModels : query(viewModels.AsQueryable()).ToList();
        }

        public void Update(SessionScheduleViewModel sessionScheduleViewModel)
        {
            var entity = _mapper.Map<SessionSchedule>(sessionScheduleViewModel);
            _sessionScheduleService.Update(entity);
        }
    }
}
