using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper.Mapper
{
    public class AddressMapper : IAddressMapper
    {
        private readonly IMapper _mapper;
        private readonly IAddressService _addressService;
        public AddressMapper(IMapper mapper, IAddressService addressService)
        {
            _mapper = mapper;
            _addressService = addressService;
        }

        public async Task AddAsync(AddressViewModel addressViewModel, CancellationToken ct = default)
        {
            var entity = _mapper.Map<Address>(addressViewModel);            
            await _addressService.AddAsync(entity, ct);
        }

        public void Delete(AddressViewModel addressViewModel, CancellationToken ct = default)
        {
            var entity = _mapper.Map<Address>(addressViewModel); 
            _addressService.Delete(entity, ct);
        }

        public async Task<IEnumerable<AddressViewModel>> GetAddressByLocationAsync(Guid addressId, CancellationToken ct = default)
        {
            var entities = await _addressService.GetAddressByLocationAsync(addressId, ct);
            return _mapper.Map<IEnumerable<AddressViewModel>>(entities);
        }

        public async Task<IEnumerable<AddressViewModel>> GetAddressesByUserAsync(Guid addressId, CancellationToken ct = default)
        {
            var entities = await _addressService.GetAddressesByUserAsync(addressId, ct);
            return _mapper.Map<IEnumerable<AddressViewModel>>(entities);
        }

        public async Task<AddressViewModel> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _addressService.GetByIdAsync(id, ct);
            return _mapper.Map<AddressViewModel>(entity);
        }

        public async Task<List<AddressViewModel>> ListAsync(Func<IQueryable<AddressViewModel>, IQueryable<AddressViewModel>>? query = null, CancellationToken ct = default)
        {
            var entities = await _addressService.ListAsync(null, ct);
            var viewModels = _mapper.Map<List<AddressViewModel>>(entities);
            return query is null ? viewModels : query(viewModels.AsQueryable()).ToList();
        }

        public void Update(AddressViewModel addressViewModel)
        {
            var entity = _mapper.Map<Address>(addressViewModel);
            _addressService.Update(entity);
        }
    }
}
