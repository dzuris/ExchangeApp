using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Transaction;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface ITransactionFacade : IFacade
{
    Task InsertAsync(TransactionDetailModel model);
}