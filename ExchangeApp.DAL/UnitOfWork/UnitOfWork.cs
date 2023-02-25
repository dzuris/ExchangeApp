using ExchangeApp.DAL.Data;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories;
using ExchangeApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;
    private ICurrencyRepository? _currencyEntityRepository;
    private IDonationRepository? _donationEntityRepository;

    public UnitOfWork(ExchangeAppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public ICurrencyRepository CurrencyRepository 
        => _currencyEntityRepository ??= new CurrencyRepository(_dbContext);

    public IDonationRepository DonationRepository
        => _donationEntityRepository ??= new DonationRepository(_dbContext);

    //public IRepository<TEntity, TId> GetRepository<TEntity, TId>()
    //    where TEntity : class, IEntity
    //    => new RepositoryBase<TEntity, TId>(_dbContext);

    public async Task CommitAsync() => await _dbContext.SaveChangesAsync();

    public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
}