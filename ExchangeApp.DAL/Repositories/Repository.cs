using ExchangeApp.DAL.Data;
using ExchangeApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Repositories;

public class Repository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : class, IEntity
{
    private readonly ExchangeAppDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(ExchangeAppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _dbSet.ToList();
    }

    public TEntity? GetById(TId id)
    {
        return _dbSet.Find(id);
    }

    public TEntity Insert(TEntity entity)
    {
        return _dbSet.Add(entity).Entity;
    }

    public void Update(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
}
