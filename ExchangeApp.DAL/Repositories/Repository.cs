using ExchangeApp.DAL.Data;
using ExchangeApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly ExchangeAppDbContext _dbContext;

    public Repository(ExchangeAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>();
    }

    public TEntity? GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public TEntity? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public TEntity Insert(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public TEntity Update(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }

    public bool Exists(Guid id)
    {
        throw new NotImplementedException();
    }

    public bool Exists(int id)
    {
        throw new NotImplementedException();
    }
}
