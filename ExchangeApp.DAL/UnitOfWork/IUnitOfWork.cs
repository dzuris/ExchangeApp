using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories;

namespace ExchangeApp.DAL.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<TEntity, TId> GetRepository<TEntity, TId>()
        where TEntity : class, IEntity;

    Task CommitAsync();
}