
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task AddAsync(Address address, CancellationToken ct = default)
        {
            if (address is null) 
                throw new ArgumentNullException(nameof(address));

            await _addressRepository.AddAsync(address, ct);
            await _addressRepository.SaveChangesAsync(ct);
        }

        public async void Delete(Address address, CancellationToken ct)
        {
            await Task.Run(async () =>
            {
                if (address is null) 
                    throw new ArgumentNullException(nameof(address));
                if (address.Id == Guid.Empty) 
                    throw new ArgumentException("Endereço sem Id.");

                var current = await _addressRepository.GetByIdAsync(address.Id, ct);
                if (current is null)
                    throw new KeyNotFoundException($"Endereço {address.Id} não encontrado.");

                _addressRepository.Remove(address);
                await _addressRepository.SaveChangesAsync(ct);
            }, ct);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty) 
                throw new ArgumentException("Id inválido.", nameof(id));

            return _addressRepository.ExistsAsync(id, ct);
        }

        public async Task<IEnumerable<Address>> GetAddressByLocationAsync(Guid addressId, CancellationToken ct = default)
        {
            if (addressId == Guid.Empty) 
                throw new ArgumentException("Endereço não Encontrado pela Localização.", nameof(addressId));

            return await _addressRepository.GetAddressByLocationAsync(addressId, ct);
        }

        public async Task<IEnumerable<Address>> GetAddressesByUserAsync(Guid addressId, CancellationToken ct = default)
        {
            if (addressId == Guid.Empty)
                throw new ArgumentException("Endereço não Encontrado por Usuário.", nameof(addressId));

            return await _addressRepository.GetAddressesByUserAsync(addressId, ct);
        }

        public async Task<Address> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty) 
                throw new ArgumentException("Id inválido.", nameof(id));

            var address = await _addressRepository.GetByIdAsync(id, ct);
            return address ?? throw new KeyNotFoundException($"Endereço {id} não encontrado.");
        }

        public Task<List<Address>> ListAsync(Func<IQueryable<Address>, IQueryable<Address>>? query = null, CancellationToken ct = default)
        {
            return _addressRepository.ListAsync(query, ct);
        }

        public async void Update(Address address)
        {
            await Task.Run(async () =>
            {
                if (address is null) 
                    throw new ArgumentNullException(nameof(address));
                if (address.Id == Guid.Empty) 
                    throw new ArgumentException("Endereço sem Id.");

                var exists = await _addressRepository.ExistsAsync(address.Id);
                if (!exists) 
                    throw new KeyNotFoundException($"Endereço {address.Id} não encontrado.");

                _addressRepository.Update(address);
                await _addressRepository.SaveChangesAsync();
            });
        }
    }
}
