using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper;

public class WaitMapper : IWaitMapper
{
    private readonly IMapper _mapper;
    private readonly IWaitService _waitService;

    public WaitMapper(IMapper mapper, IWaitService waitService)
    {
        _mapper = mapper;
        _waitService = waitService;
    }

    public async Task<WaitViewModel> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _waitService.GetByIdAsync(id, ct);
        return _mapper.Map<WaitViewModel>(entity);
    }

    public async Task<List<WaitViewModel>> ListAsync(Func<IQueryable<WaitViewModel>, IQueryable<WaitViewModel>>? query = null, CancellationToken ct = default)
    {
        var entities = await _waitService.ListAsync(null, ct);
        var viewModels = _mapper.Map<List<WaitViewModel>>(entities);
        return query is null ? viewModels : query(viewModels.AsQueryable()).ToList();
    }

    public async Task AddAsync(WaitViewModel waitViewModel, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Wait>(waitViewModel);
        await _waitService.AddAsync(entity, ct);
    }

    public void Update(WaitViewModel waitViewModel)
    {
        var entity = _mapper.Map<Wait>(waitViewModel);
        _waitService.Update(entity);
    }

    public void Delete(WaitViewModel waitViewModel, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Wait>(waitViewModel);
        _waitService.Delete(entity, ct);
    }
}