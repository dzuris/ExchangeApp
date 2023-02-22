using ExchangeApp.DAL.Data;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;

    public UnitOfWork(ExchangeAppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public IRepository<TEntity, TId> GetRepository<TEntity, TId>()
        where TEntity : class, IEntity
        => new Repository<TEntity, TId>(_dbContext);

    public async Task CommitAsync() => await _dbContext.SaveChangesAsync();

    public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
}