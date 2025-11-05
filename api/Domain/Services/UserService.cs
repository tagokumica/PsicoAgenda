

using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Domain.Notifiers;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly INotifier _notifier;

        public UserService(IUserRepository userRepository, IPatientRepository patientRepository, INotifier notifier)
        {
            _userRepository = userRepository;
            _patientRepository = patientRepository;
            _notifier = notifier;
        }

        public async Task AddAsync(User user, CancellationToken ct = default)
        {
            try
            {
                if (await _patientRepository.ExistsByEmailAsync(user.Email, ct) ||
                    await _userRepository.ExistsByEmailAsync(user.Email, ct))
                {
                    _notifier.Handle(new Notification(nameof(user.Email), "E-mail já cadastrado."));
                    return;
                }

                await _userRepository.AddAsync(user, ct);
                await _userRepository.SaveChangesAsync(ct);

            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
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
            try
            {
                if (await _patientRepository.ExistsByEmailAsync(user.Email) ||
                    await _userRepository.ExistsByEmailAsync(user.Email))
                {
                    _notifier.Handle(new Notification(nameof(user.Email), "E-mail já cadastrado."));
                    return;
                }

                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Usuário sem Id.", e);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException($"Usuário {user.Id} não encontrado.", e);
            }

            await Task.CompletedTask;

        }
    }
}
