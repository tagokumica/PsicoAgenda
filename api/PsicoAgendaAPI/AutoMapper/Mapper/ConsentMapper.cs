using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper
{
    public class ConsentMapper : IConsentMapper
    {
        private readonly IMapper _mapper;
        private readonly IConsentService _consentService;
        public ConsentMapper(IMapper mapper, IConsentService consentService)
        {
            _mapper = mapper;
            _consentService = consentService;
        }

        public async Task AddAsync(ConsentViewModel consentViewModel, CancellationToken ct = default)
        {
            var entity = _mapper.Map<Consent>(consentViewModel);
            await _consentService.AddAsync(entity, ct);
        }

        public void Delete(ConsentViewModel consentViewModel, CancellationToken ct = default)
        {
            var entity = _mapper.Map<Consent>(consentViewModel);
            _consentService.Delete(entity, ct);
        }

        public async Task<ConsentViewModel> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _consentService.GetByIdAsync(id, ct);
            return _mapper.Map<ConsentViewModel>(entity);
        }

        public async Task<List<ConsentViewModel>> ListAsync(Func<IQueryable<ConsentViewModel>, IQueryable<ConsentViewModel>>? query = null, CancellationToken ct = default)
        {
            var entities = await _consentService.ListAsync(null, ct);
            var viewModels = _mapper.Map<List<ConsentViewModel>>(entities);
            return query is null ? viewModels : query(viewModels.AsQueryable()).ToList();
        }

        public void Update(ConsentViewModel consentViewModel)
        {
            var entity = _mapper.Map<Consent>(consentViewModel);
            _consentService.Update(entity);
        }
    }
}
