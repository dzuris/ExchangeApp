using ExchangeApp.DAL.Data;
using ExchangeApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Repositories;

public class Repository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : class, IEntity
{
    private readonly DbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }
    
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public async Task DeleteAsync(TId id)
    {
        var entity = await GetByIdAsync(id);

        if (entity is null)
        {
            return;
        }

        _dbSet.Remove(entity);
    }
}
