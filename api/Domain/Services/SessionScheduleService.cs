
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
            try
            {
                _sessionScheduleRepository.Remove(sessionSchedule);
                await _sessionScheduleRepository.SaveChangesAsync(ct);
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException();
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Agenda de Atendimento sem Id.", e);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException($"Agenda de Atendimento {sessionSchedule.Id} não encontrado.", e);
            }

            await Task.CompletedTask;

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

        public Task<int> GetSessionScheduleByDurationCountAsync(TimeSpan durationMinute, CancellationToken ct = default)
        {
            return _sessionScheduleRepository.GetSessionScheduleByDurationCountAsync(durationMinute, ct);
        }

        public Task<IEnumerable<SessionSchedule>> GetSessionScheduleBySessionNotesAsync(Guid sessionScheduleId, CancellationToken ct = default)
        {
            return _sessionScheduleRepository.GetSessionScheduleBySessionNotesAsync(sessionScheduleId, ct);
        }

        public Task<IEnumerable<SessionSchedule>> GetSessionScheduleByWaitsAsync(Guid sessionScheduleId, CancellationToken ct = default)
        {
            return _sessionScheduleRepository.GetSessionScheduleByWaitsAsync(sessionScheduleId, ct);
        }

        public Task<IEnumerable<SessionSchedule>> GetUpcomingSessionsAsync(TimeSpan duration, CancellationToken ct = default)
        {
            return _sessionScheduleRepository.GetUpcomingSessionsAsync(duration, ct);
        }

        public Task<List<SessionSchedule>> ListAsync(Func<IQueryable<SessionSchedule>, IQueryable<SessionSchedule>>? query = null, CancellationToken ct = default)
        {
            return _sessionScheduleRepository.ListAsync(query, ct);
        }

        public async void Update(SessionSchedule sessionSchedule)
        {
            try
            {
                _sessionScheduleRepository.Update(sessionSchedule);
                await _sessionScheduleRepository.SaveChangesAsync();
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException();
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Agenda de Atendimento sem Id.", e);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException($"Agenda de Atendimento {sessionSchedule.Id} não encontrado.", e);
            }

            await Task.CompletedTask;
        }
    }
}
