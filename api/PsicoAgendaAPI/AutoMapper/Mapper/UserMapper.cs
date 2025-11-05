using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper;

public class UserMapper : IUserMapper
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UserMapper(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }
    public async Task<UserViewModel> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _userService.GetByIdAsync(id, ct);
        return _mapper.Map<UserViewModel>(entity);
    }

    public async Task<List<UserViewModel>> ListAsync(Func<IQueryable<UserViewModel>, IQueryable<UserViewModel>>? query = null, CancellationToken ct = default)
    {
        var entities = await _userService.ListAsync(null, ct);
        var viewModels = _mapper.Map<List<UserViewModel>>(entities);
        return query is null ? viewModels : query(viewModels.AsQueryable()).ToList();
    }

    public async Task AddAsync(UserViewModel userViewModel, CancellationToken ct = default)
    {
        var entity = _mapper.Map<User>(userViewModel);
        await _userService.AddAsync(entity, ct);
    }

    public void Update(UserViewModel userViewModel)
    {
        var entity = _mapper.Map<User>(userViewModel);
        _userService.Update(entity);
    }

    public void Delete(UserViewModel userViewModel, CancellationToken ct = default)
    {
        var entity = _mapper.Map<User>(userViewModel);
        _userService.Delete(entity, ct);
    }
}