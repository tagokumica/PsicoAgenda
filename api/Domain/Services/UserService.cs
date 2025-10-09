

using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddAsync(User user, CancellationToken ct = default)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            await _userRepository.AddAsync(user, ct);
            await _userRepository.SaveChangesAsync(ct);
        }

        public async void Delete(User user, CancellationToken ct = default)
        {
            await Task.Run(async () =>
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));
                if (user.Id == Guid.Empty)
                    throw new ArgumentException("Usuário sem Id.");

                var current = await _userRepository.GetByIdAsync(user.Id, ct);
                if (current is null)
                    throw new KeyNotFoundException($"Usuário {user.Id} não encontrado.");

                _userRepository.Remove(user);
                await _userRepository.SaveChangesAsync(ct);
            }, ct);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            return _userRepository.ExistsAsync(id, ct);
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            var user = await _userRepository.GetByIdAsync(id, ct);
            return user ?? throw new KeyNotFoundException($"Usuário {id} não encontrado.");
        }

        public Task<List<User>> ListAsync(Func<IQueryable<User>, IQueryable<User>>? query = null, CancellationToken ct = default)
        {
            return _userRepository.ListAsync(query, ct);
        }

        public async void Update(User user)
        {
            await Task.Run(async () =>
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));
                if (user.Id == Guid.Empty)
                    throw new ArgumentException("Usuário sem Id.");

                var current = await _userRepository.GetByIdAsync(user.Id);
                if (current is null)
                    throw new KeyNotFoundException($"Usuário {user.Id} não encontrado.");

                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();
            });
        }
    }
}
