using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Repositories;

public class RepositoryBase<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : class, IEntity
{
    protected readonly DbContext AppDbContext;
    private readonly DbSet<TEntity> _dbSet;

    public RepositoryBase(DbContext appDbContext)
    {
        AppDbContext = appDbContext;
        _dbSet = appDbContext.Set<TEntity>();
    }
    
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var list = await _dbSet.ToListAsync();
        return list;
    }

    public virtual async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual async Task DeleteAsync(TId id)
    {
        var entity = await GetByIdAsync(id);

        if (entity is null)
        {
            return;
        }

        _dbSet.Remove(entity);
    }
}
