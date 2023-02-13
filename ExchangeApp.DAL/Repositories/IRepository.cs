using ExchangeApp.DAL.Entities;

namespace ExchangeApp.DAL.Repositories;

public interface IRepository<TEntity, TId>
    where TEntity : class, IEntity
{
    IEnumerable<TEntity> GetAll();
    TEntity? GetById(TId id);
    TEntity Insert (TEntity entity);
    void Update (TEntity entity);
    void Remove(TEntity entity);
}
