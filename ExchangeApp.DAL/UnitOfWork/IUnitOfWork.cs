using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories;
using ExchangeApp.DAL.Repositories.Interfaces;

namespace ExchangeApp.DAL.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    //IRepository<TEntity, TId> GetRepository<TEntity, TId>()
    //    where TEntity : class, IEntity;

    ICurrencyRepository CurrencyRepository { get; }
    IDonationRepository DonationRepository { get; }

    Task CommitAsync();
}