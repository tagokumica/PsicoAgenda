using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper;

public class TermsAcceptanceMapper : ITermsAcceptanceMapper
{
    private readonly IMapper _mapper;
    private readonly ITermsAcceptanceService _termsAcceptanceService;
    public TermsAcceptanceMapper(IMapper mapper, ITermsAcceptanceService termsAcceptanceService)
    {
        _mapper = mapper;
        _termsAcceptanceService = termsAcceptanceService;
    }

    public async Task<TermsAcceptanceViewModel> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _termsAcceptanceService.GetByIdAsync(id, ct);
        return _mapper.Map<TermsAcceptanceViewModel>(entity);
    }

    public async Task<List<TermsAcceptanceViewModel>> ListAsync(Func<IQueryable<TermsAcceptanceViewModel>, IQueryable<TermsAcceptanceViewModel>>? query = null, CancellationToken ct = default)
    {
        var entities = await _termsAcceptanceService.ListAsync(null, ct);
        var viewModels = _mapper.Map<List<TermsAcceptanceViewModel>>(entities);
        return query is null ? viewModels : query(viewModels.AsQueryable()).ToList();
    }

    public async Task AddAsync(TermsAcceptanceViewModel termsAcceptanceViewModel, CancellationToken ct = default)
    {
        var entity = _mapper.Map<TermsAcceptance>(termsAcceptanceViewModel);
        await _termsAcceptanceService.AddAsync(entity, ct);
    }

    public void Update(TermsAcceptanceViewModel termsAcceptanceViewModel)
    {
        var entity = _mapper.Map<TermsAcceptance>(termsAcceptanceViewModel);
        _termsAcceptanceService.Update(entity);
    }

    public void Delete(TermsAcceptanceViewModel termsAcceptanceViewModel, CancellationToken ct = default)
    {
        var entity = _mapper.Map<TermsAcceptance>(termsAcceptanceViewModel);
        _termsAcceptanceService.Delete(entity, ct);
    }

    public async Task<IEnumerable<TermsAcceptanceViewModel>> GetIsAgreedAsync(CancellationToken ct = default)
    {
        var entities = await _termsAcceptanceService.GetIsAgreedAsync(ct);
        return _mapper.Map<IEnumerable<TermsAcceptanceViewModel>>(entities);
    }

    public async Task<IEnumerable<TermsAcceptanceViewModel>> GetIsNotAgreedAsync(CancellationToken ct = default)
    {
        var entities = await _termsAcceptanceService.GetIsNotAgreedAsync(ct);
        return _mapper.Map<IEnumerable<TermsAcceptanceViewModel>>(entities);
    }

    public async Task<TermsAcceptanceViewModel> IsAgreedAsync(Guid userId, Guid termsOfUseId, CancellationToken ct = default)
    {
        var entity = await _termsAcceptanceService.IsAgreedAsync(userId, termsOfUseId, ct);
        return _mapper.Map<TermsAcceptanceViewModel>(entity);
    }

    public async Task<TermsAcceptanceViewModel> IsNotAgreedAsync(Guid userId, Guid termsOfUseId, CancellationToken ct = default)
    {
        var entity = await _termsAcceptanceService.IsNotAgreedAsync(userId, termsOfUseId, ct);
        return _mapper.Map<TermsAcceptanceViewModel>(entity);
    }
}