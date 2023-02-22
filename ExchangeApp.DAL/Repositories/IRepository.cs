using ExchangeApp.DAL.Entities;

namespace ExchangeApp.DAL.Repositories;

public interface IRepository<TEntity, in TId>
    where TEntity : class, IEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TId id);
    Task InsertAsync (TEntity entity);
    Task UpdateAsync (TEntity entity);
    Task DeleteAsync(TId id);
}
