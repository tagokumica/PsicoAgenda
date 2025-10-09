
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class SessionScheduleService : ISessionScheduleService
    {
        private readonly ISessionScheduleRepository _sessionScheduleRepository;
        public SessionScheduleService(ISessionScheduleRepository sessionScheduleRepository)
        {
            _sessionScheduleRepository = sessionScheduleRepository;
        }
        public async Task AddAsync(SessionSchedule sessionSchedule, CancellationToken ct = default)
        {
            if (sessionSchedule is null)
                throw new ArgumentNullException(nameof(sessionSchedule));

            await _sessionScheduleRepository.AddAsync(sessionSchedule, ct);
            await _sessionScheduleRepository.SaveChangesAsync(ct);
        }

        public async void Delete(SessionSchedule sessionSchedule, CancellationToken ct = default)
        {
            await Task.Run(async () =>
            {
                if (sessionSchedule is null)
                    throw new ArgumentNullException(nameof(sessionSchedule));
                if (sessionSchedule.Id == Guid.Empty)
                    throw new ArgumentException("Agenda de Atendimento sem Id.");

                var current = await _sessionScheduleRepository.GetByIdAsync(sessionSchedule.Id, ct);
                if (current is null)
                    throw new KeyNotFoundException($"Agenda de Atendimento {sessionSchedule.Id} não encontrado.");

                _sessionScheduleRepository.Remove(sessionSchedule);
                await _sessionScheduleRepository.SaveChangesAsync(ct);
            }, ct);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            return _sessionScheduleRepository.ExistsAsync(id, ct);
        }

        public async Task<SessionSchedule> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            var sessionSchedule = await _sessionScheduleRepository.GetByIdAsync(id, ct);
            return sessionSchedule ?? throw new KeyNotFoundException($"Agenda de Atendimento {id} não encontrado.");
        }

        public Task<IEnumerable<SessionSchedule>> GetSessionScheduleBySessionNotesAsync(Guid sessionScheduleId, CancellationToken ct = default)
        {
            return _sessionScheduleRepository.GetSessionScheduleBySessionNotesAsync(sessionScheduleId, ct);
        }

        public Task<IEnumerable<SessionSchedule>> GetSessionScheduleByWaitsAsync(Guid sessionScheduleId, CancellationToken ct = default)
        {
            return _sessionScheduleRepository.GetSessionScheduleByWaitsAsync(sessionScheduleId, ct);
        }

        public Task<List<SessionSchedule>> ListAsync(Func<IQueryable<SessionSchedule>, IQueryable<SessionSchedule>>? query = null, CancellationToken ct = default)
        {
            return _sessionScheduleRepository.ListAsync(query, ct);
        }

        public async void Update(SessionSchedule sessionSchedule)
        {
            await Task.Run(async () =>
            {
                if (sessionSchedule is null)
                    throw new ArgumentNullException(nameof(sessionSchedule));
                if (sessionSchedule.Id == Guid.Empty)
                    throw new ArgumentException("Agenda de Atendimento sem Id.");

                var exists = await _sessionScheduleRepository.ExistsAsync(sessionSchedule.Id);
                if (!exists)
                    throw new KeyNotFoundException($"Agenda de Atendimento {sessionSchedule.Id} não encontrado.");

                _sessionScheduleRepository.Update(sessionSchedule);
                await _sessionScheduleRepository.SaveChangesAsync();
            });
        }
    }
}
