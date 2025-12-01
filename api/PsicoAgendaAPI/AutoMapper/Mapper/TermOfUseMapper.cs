using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper
{
    public class TermOfUseMapper : ITermOfUseMapper
    {
        private readonly IMapper _mapper;
        private readonly ITermOfUseService _termsOfUseService;
        public TermOfUseMapper(IMapper mapper, ITermOfUseService termsOfUseService)
        {
            _mapper = mapper;
            _termsOfUseService = termsOfUseService;
        }
        public async Task AddAsync(TermsOfUseViewModel termsOfUseViewModel, CancellationToken ct = default)
        {
            var entity = _mapper.Map<TermsOfUse>(termsOfUseViewModel);
            await _termsOfUseService.AddAsync(entity, ct);
        }

        public void Delete(TermsOfUseViewModel termsOfUseViewModel, CancellationToken ct = default)
        {
            var entity = _mapper.Map<TermsOfUse>(termsOfUseViewModel);
            _termsOfUseService.Delete(entity, ct);
        }

        public async Task<TermsOfUseViewModel> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _termsOfUseService.GetByIdAsync(id, ct);
            return _mapper.Map<TermsOfUseViewModel>(entity);
        }

        public async Task<IEnumerable<TermsOfUseViewModel>> GetTermsOfUseByTermsAcceptanceAsync(Guid termsOfUseId, CancellationToken ct = default)
        {
            var entities = await _termsOfUseService.GetTermsOfUseByTermsAcceptanceAsync(termsOfUseId, ct);
            return _mapper.Map<IEnumerable<TermsOfUseViewModel>>(entities);
        }

        public async Task<List<TermsOfUseViewModel>> ListAsync(Func<IQueryable<TermsOfUseViewModel>, IQueryable<TermsOfUseViewModel>>? query = null, CancellationToken ct = default)
        {
            var entities = await _termsOfUseService.ListAsync(null, ct);
            var viewModels = _mapper.Map<List<TermsOfUseViewModel>>(entities);
            return query is null ? viewModels : query(viewModels.AsQueryable()).ToList();
        }

        public void Update(TermsOfUseViewModel termsOfUseViewModel)
        {
            var entity = _mapper.Map<TermsOfUse>(termsOfUseViewModel);
            _termsOfUseService.Update(entity);
        }
    }
}
