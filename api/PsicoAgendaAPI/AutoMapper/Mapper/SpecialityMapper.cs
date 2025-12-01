using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper;

public class SpecialityMapper: ISpecialityMapper
{
    private readonly IMapper _mapper;
    private readonly ISpecialityService _specialityService;
    public SpecialityMapper(ISpecialityService specialityService, IMapper mapper)
    {
        _specialityService = specialityService;
        _mapper = mapper;
    }
    public async Task<SpecialityViewModel> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _specialityService.GetByIdAsync(id, ct);
        return _mapper.Map<SpecialityViewModel>(entity);
    }

    public async Task<List<SpecialityViewModel>> ListAsync(Func<IQueryable<SpecialityViewModel>, IQueryable<SpecialityViewModel>>? query = null, CancellationToken ct = default)
    {
        var entities = await _specialityService.ListAsync(null, ct);
        var viewModels = _mapper.Map<List<SpecialityViewModel>>(entities);
        return query is null ? viewModels : query(viewModels.AsQueryable()).ToList();
    }

    public async Task AddAsync(SpecialityViewModel specialityViewModel, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Speciality>(specialityViewModel);
        await _specialityService.AddAsync(entity, ct);
    }

    public void Update(SpecialityViewModel specialityViewModel)
    {
        var entity = _mapper.Map<Speciality>(specialityViewModel);
        _specialityService.Update(entity);
    }

    public void Delete(SpecialityViewModel specialityViewModel, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Speciality>(specialityViewModel);
        _specialityService.Delete(entity, ct);
    }
}