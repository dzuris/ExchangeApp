using ExchangeApp.BL.Models.Transaction;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface ITransactionFacade : IFacade
{
    Task<TransactionDetailModel?> GetById(int id);
    Task<int> InsertAsync(TransactionDetailModel model);
}