using ExchangeApp.BL.Models.Transaction;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface ITransactionFacade : IFacade
{
    Task<int> InsertAsync(TransactionDetailModel model);
}