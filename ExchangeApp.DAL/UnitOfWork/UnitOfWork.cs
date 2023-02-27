using ExchangeApp.DAL.Data;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories;
using ExchangeApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;
    private ICurrencyRepository? _currencyRepository;
    private IDonationRepository? _donationRepository;
    private ITransactionRepository? _transactionRepository;

    public UnitOfWork(ExchangeAppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public ICurrencyRepository CurrencyRepository 
        => _currencyRepository ??= new CurrencyRepository(_dbContext);

    public IDonationRepository DonationRepository
        => _donationRepository ??= new DonationRepository(_dbContext);

    public ITransactionRepository TransactionRepository 
        => _transactionRepository ??= new TransactionRepository(_dbContext);

    public async Task CommitAsync() => await _dbContext.SaveChangesAsync();

    public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
}