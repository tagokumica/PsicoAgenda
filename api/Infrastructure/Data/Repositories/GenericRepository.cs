using Domain.Interface.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class GenericRepository<T, TId> : IGenericRepository<T, TId> where T : class
{
    protected readonly PsicoContext _db;
    protected readonly DbSet<T> _set;

    public GenericRepository(PsicoContext db)
    {
        _db = db;
        _set = _db.Set<T>();
    }

    public async Task<T?> GetByIdAsync(TId id, CancellationToken ct = default)
    {
        return await _set.FindAsync([id], ct);
    }

    public async Task<List<T>> ListAsync(Func<IQueryable<T>, IQueryable<T>>? query = null, CancellationToken ct = default)
    {
        IQueryable<T> q = _set.AsNoTracking();
        if (query is not null) q = query(q);
        return await q.ToListAsync(ct);
    }

    public async Task<bool> ExistsAsync(TId id, CancellationToken ct = default)
    {
        var entity = await _set.FindAsync([id], ct);
        if (entity is not null)
        {
            _db.Entry(entity).State = EntityState.Detached;
            return true;
        }
        return false;
    }

    public async Task<int> CountAsync(Func<IQueryable<T>, IQueryable<T>>? query = null, CancellationToken ct = default)
    {
        IQueryable<T> q = _set.AsNoTracking();
        if (query is not null) q = query(q);
        return await q.CountAsync(ct);
    }

    public Task AddAsync(T entity, CancellationToken ct = default)
        => _set.AddAsync(entity, ct).AsTask();

    public void Update(T entity) => _set.Update(entity);
    public void Remove(T entity) => _set.Remove(entity);

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _db.SaveChangesAsync(ct);
}