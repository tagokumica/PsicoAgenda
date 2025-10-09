namespace Domain.Interface.Repositories;

public interface IGenericRepository<T, TId> where T : class
{
    Task<T?> GetByIdAsync(TId id, CancellationToken ct = default);
    Task<List<T>> ListAsync(
        Func<IQueryable<T>, IQueryable<T>>? query = null, CancellationToken ct = default);
    Task<bool> ExistsAsync(TId id, CancellationToken ct = default);
    Task<int> CountAsync(Func<IQueryable<T>, IQueryable<T>>? query = null, CancellationToken ct = default);
    Task AddAsync(T entity, CancellationToken ct = default);
    void Update(T entity);
    void Remove(T entity);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}