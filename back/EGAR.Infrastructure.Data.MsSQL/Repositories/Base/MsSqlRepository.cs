using EGAR.Infrastructure.Data.MsSQL.Context;
using Microsoft.EntityFrameworkCore;

namespace EGAR.Infrastructure.Data.MsSQL.Repositories.Base;

public class MsSqlRepository<TEntity> where TEntity : class
{
    readonly AppDbContext _dbContext;
    readonly DbSet<TEntity> _dbSet;

    protected AppDbContext DbContext => _dbContext;
    protected DbSet<TEntity> DbSet => _dbSet;

    public MsSqlRepository(AppDbContext context)
    {
        _dbContext = context;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct = default)
        => await _dbSet.ToListAsync(ct);

    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default) 
        => await _dbSet.FindAsync(id, ct);

    public async Task AddAsync(TEntity entity, CancellationToken ct = default)
        => await _dbSet.AddAsync(entity, ct);

    public Task Update(TEntity entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken ct = default) 
        => _dbContext.SaveChangesAsync(ct);
}
