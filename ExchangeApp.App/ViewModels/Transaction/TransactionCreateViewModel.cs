using ExchangeApp.BL.Facades.Interfaces;
using Microsoft.Extensions.Logging;

namespace ExchangeApp.App.ViewModels.Transaction;

public class TransactionCreateViewModel : ViewModelBase
{
    private readonly ILogger<TransactionCreateViewModel> _logger;
    private readonly ITransactionFacade _transactionFacade;
}