using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper
{
    public class SessionNoteMapper : ISessionNoteMapper
    {
        private readonly IMapper _mapper;
        private readonly ISessionNoteService _sessionNoteService;
        public SessionNoteMapper(IMapper mapper, ISessionNoteService sessionNoteService)
        {
            _mapper = mapper;
            _sessionNoteService = sessionNoteService;
        }

        public async Task AddAsync(SessionNoteViewModel sessionNoteViewModel, CancellationToken ct = default)
        {
            var entity = _mapper.Map<SessionNote>(sessionNoteViewModel);
            await _sessionNoteService.AddAsync(entity, ct);
        }

        public void Delete(SessionNoteViewModel sessionNoteViewModel, CancellationToken ct = default)
        {
            var entity = _mapper.Map<SessionNote>(sessionNoteViewModel);
            _sessionNoteService.Delete(entity, ct);
        }

        public async Task<SessionNoteViewModel> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _sessionNoteService.GetByIdAsync(id, ct);
            return _mapper.Map<SessionNoteViewModel>(entity);

        }

        public async Task<List<SessionNoteViewModel>> ListAsync(Func<IQueryable<SessionNoteViewModel>, IQueryable<SessionNoteViewModel>>? query = null, CancellationToken ct = default)
        {
            var entities = await _sessionNoteService.ListAsync(null, ct);
            var viewModels = _mapper.Map<List<SessionNoteViewModel>>(entities);
            return query is null ? viewModels : query(viewModels.AsQueryable()).ToList();
        }

        public void Update(SessionNoteViewModel sessionNoteViewModel)
        {
            var entity = _mapper.Map<SessionNote>(sessionNoteViewModel);
            _sessionNoteService.Update(entity);
        }
    }
}
