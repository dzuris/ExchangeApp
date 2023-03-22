using ExchangeApp.DAL.Repositories.Interfaces;

namespace ExchangeApp.DAL.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    ICurrencyRepository CurrencyRepository { get; }
    IDonationRepository DonationRepository { get; }
    ITransactionRepository TransactionRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IOperationRepository OperationRepository { get; }
    ITotalBalanceRepository TotalBalanceRepository { get; }

    Task CommitAsync();
}