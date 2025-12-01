
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services;

namespace Domain.Services
{
    public class TermsAcceptanceService : ITermsAcceptanceService
    {
        private readonly ITermsAcceptanceRepository _termsAcceptanceRepository;
        public TermsAcceptanceService(ITermsAcceptanceRepository termsAcceptanceRepository)
        {
            _termsAcceptanceRepository = termsAcceptanceRepository;
        }
        public async Task AddAsync(TermsAcceptance termsAcceptance, CancellationToken ct = default)
        {
            if (termsAcceptance is null)
                throw new ArgumentNullException(nameof(termsAcceptance));

            await _termsAcceptanceRepository.AddAsync(termsAcceptance, ct);
            await _termsAcceptanceRepository.SaveChangesAsync(ct);
        }

        public async void Delete(TermsAcceptance termsAcceptance, CancellationToken ct = default)
        {
            try
            {
                _termsAcceptanceRepository.Remove(termsAcceptance);
                await _termsAcceptanceRepository.SaveChangesAsync(ct);
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException();
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Aceite dos Termos sem Id.", e);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException($"Aceite dos Termos {termsAcceptance.Id} não encontrado.", e);
            }

            await Task.CompletedTask;

        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            return _termsAcceptanceRepository.ExistsAsync(id, ct);
        }

        public async Task<TermsAcceptance> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(id));

            var termsAcceptance = await _termsAcceptanceRepository.GetByIdAsync(id, ct);
            return termsAcceptance ?? throw new KeyNotFoundException($"Aceite dos Termos {id} não encontrado.");
        }

        public async Task<IEnumerable<TermsAcceptance>> GetIsAgreedAsync(CancellationToken ct = default)
        {
            return await _termsAcceptanceRepository.GetIsAgreedAsync(ct);
        }

        public async Task<IEnumerable<TermsAcceptance>> GetIsNotAgreedAsync(CancellationToken ct = default)
        {
            return await _termsAcceptanceRepository.GetIsNotAgreedAsync(ct);
        }

        public async Task<TermsAcceptance> IsAgreedAsync(Guid userId, Guid termsOfUseId, CancellationToken ct = default)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(userId));

            if (termsOfUseId == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(termsOfUseId));

            var termsAcceptance = await _termsAcceptanceRepository.IsAgreedAsync(userId, termsOfUseId, ct);
            return termsAcceptance;
        }

        public async Task<TermsAcceptance> IsNotAgreedAsync(Guid userId, Guid termsOfUseId, CancellationToken ct = default)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(userId));

            if (termsOfUseId == Guid.Empty)
                throw new ArgumentException("Id inválido.", nameof(termsOfUseId));

            var termsAcceptance = await _termsAcceptanceRepository.IsAgreedAsync(userId, termsOfUseId, ct);
            return termsAcceptance;
        }

        public Task<List<TermsAcceptance>> ListAsync(Func<IQueryable<TermsAcceptance>, IQueryable<TermsAcceptance>>? query = null, CancellationToken ct = default)
        {
            return _termsAcceptanceRepository.ListAsync(query, ct);
        }

        public async void Update(TermsAcceptance termsAcceptance)
        {
            try
            {
                _termsAcceptanceRepository.Update(termsAcceptance);
                await _termsAcceptanceRepository.SaveChangesAsync();
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException();
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Aceite dos Termos sem Id.", e);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException($"Aceite dos Termos {termsAcceptance.Id} não encontrado.", e);
            }

            await Task.CompletedTask;

        }
    }
}
