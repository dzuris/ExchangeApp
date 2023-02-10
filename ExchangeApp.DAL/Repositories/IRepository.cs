using ExchangeApp.DAL.Entities;

namespace ExchangeApp.DAL.Repositories;

public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    IQueryable<TEntity> GetAll();
    TEntity? GetById(Guid id);
    TEntity? GetById(int id);
    TEntity Insert (TEntity entity);
    TEntity Update (TEntity entity);
    void Remove(Guid id);
    void Remove(int id);
    bool Exists(Guid id);
    bool Exists(int id);
}
