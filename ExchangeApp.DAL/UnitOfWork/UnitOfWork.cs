using AutoMapper;
using ExchangeApp.DAL.Data;
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
    private ICustomerRepository? _customerRepository;
    private IOperationRepository? _operationRepository;
    private readonly IMapper _mapper;

    public UnitOfWork(ExchangeAppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper;
    }

    public ICurrencyRepository CurrencyRepository 
        => _currencyRepository ??= new CurrencyRepository(_dbContext, _mapper);

    public IDonationRepository DonationRepository
        => _donationRepository ??= new DonationRepository(_dbContext, _mapper);

    public ITransactionRepository TransactionRepository 
        => _transactionRepository ??= new TransactionRepository(_dbContext, _mapper);

    public ICustomerRepository CustomerRepository 
        => _customerRepository ??= new CustomerRepository(_dbContext);

    public IOperationRepository OperationRepository
        => _operationRepository ??= new OperationRepository(_dbContext, _mapper);

    public async Task CommitAsync() => await _dbContext.SaveChangesAsync();

    public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
}