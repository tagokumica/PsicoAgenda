
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class SessionNoteService : ISessionNoteService
    {
        private readonly ISessionNoteRepository _sessionNoteRepository;
        public SessionNoteService(ISessionNoteRepository sessionNoteRepository)
        {
            _sessionNoteRepository = sessionNoteRepository;
        }
        public async Task AddAsync(SessionNote sessionNote, CancellationToken ct = default)
        {
            if (sessionNote is null)
                throw new ArgumentNullException(nameof(sessionNote));

            await _sessionNoteRepository.AddAsync(sessionNote, ct);
            await _sessionNoteRepository.SaveChangesAsync(ct);
        }

        public async void Delete(SessionNote sessionNote, CancellationToken ct = default)
        {
            await Task.Run(async () =>
            {
                if (sessionNote is null)
                    throw new ArgumentNullException(nameof(sessionNote));
                if (sessionNote.Id == Guid.Empty)
                    throw new ArgumentException("Notas de Sessão sem Id.");

                var current = await _sessionNoteRepository.GetByIdAsync(sessionNote.Id, ct);
                if (current is null)
                    throw new KeyNotFoundException($"Notas de Sessão {sessionNote.Id} não encontrado.");

                _sessionNoteRepository.Remove(sessionNote);
                await _sessionNoteRepository.SaveChangesAsync(ct);
            }, ct);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            return _sessionNoteRepository.ExistsAsync(id, ct);
        }

        public async Task<SessionNote> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            var sessionNote = await _sessionNoteRepository.GetByIdAsync(id, ct);
            return sessionNote ?? throw new KeyNotFoundException($"Notas de Sessão {id} não encontrado.");
        }

        public Task<List<SessionNote>> ListAsync(Func<IQueryable<SessionNote>, IQueryable<SessionNote>>? query = null, CancellationToken ct = default)
        {
            return _sessionNoteRepository.ListAsync(query, ct);
        }

        public async void Update(SessionNote sessionNote)
        {
            await Task.Run(async () =>
            {
                if (sessionNote is null)
                    throw new ArgumentNullException(nameof(sessionNote));
                if (sessionNote.Id == Guid.Empty)
                    throw new ArgumentException("Notas de Sessão sem Id.");

                var exists = await _sessionNoteRepository.ExistsAsync(sessionNote.Id);
                if (!exists)
                    throw new KeyNotFoundException($"Notas de Sessão {sessionNote.Id} não encontrado.");

                _sessionNoteRepository.Update(sessionNote);
                await _sessionNoteRepository.SaveChangesAsync();
            });
        }
    }
}
