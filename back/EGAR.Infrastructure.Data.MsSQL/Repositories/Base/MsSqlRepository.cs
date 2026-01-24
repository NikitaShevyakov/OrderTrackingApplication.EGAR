using EGAR.Infrastructure.Data.MsSQL.Context;
using EGAR.Infrastructure.Data.MsSQL.Entity;
using EGAR.Infrastructure.Data.MsSQL.Extensions;
using EGAR.SharedKernel.Enums;
using EGAR.SharedKernel.Models;
using Microsoft.EntityFrameworkCore;

namespace EGAR.Infrastructure.Data.MsSQL.Repositories.Base;

public class MsSqlRepository<TEntity> where TEntity : BaseEntity 
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

    public IQueryable<TEntity> Query() => _dbSet.AsQueryable();

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct = default)
        => await _dbSet.ToListAsync(ct);

    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default) 
        => await _dbSet.Where(x => x.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync(ct);

    public async Task<Result<TEntity>> AddAsync(TEntity entity, CancellationToken ct = default)
    {
        try
        {
            await _dbSet.AddAsync(entity, ct);
            await _dbContext.SaveChangesAsync(ct);
            return Result<TEntity>.Success(entity);
        }
        catch (Exception ex)
        {
            var errorType = DatabaseErrorResolver.ResolveFromException(ex);
            var message = errorType.GetUserFriendlyMessage();
            //logs if need to add
            return Result<TEntity>.Failure(errorType, message);
        }
    }

    public async Task<Result<TEntity>> UpdateAsync(TEntity entity, CancellationToken ct = default)
    {
        try
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync(ct);
            return Result<TEntity>.Success(entity);
        }
        catch (Exception ex)
        {
            var errorType = DatabaseErrorResolver.ResolveFromException(ex);
            var message = errorType.GetUserFriendlyMessage();
            //logs if need to add
            return Result<TEntity>.Failure(errorType, message);
        } 
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        try
        {
            var entity = await GetByIdAsync(id, ct);
            if (entity == null)
                return Result.Failure(ErrorType.NotFound, "Entity not found");

            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync(ct);

            return Result.Success();
        }
        catch (Exception ex)
        {
            var errorType = DatabaseErrorResolver.ResolveFromException(ex);
            var message = errorType.GetUserFriendlyMessage();
            //logs if need to add
            return Result.Failure(errorType, message);
        }
    }
}
