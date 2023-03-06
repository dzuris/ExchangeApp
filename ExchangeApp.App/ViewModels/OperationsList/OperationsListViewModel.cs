using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.ViewModels.OperationsList;

public partial class OperationsListViewModel : ViewModelBase
{
    public IEnumerable<OperationListModelBase> Operations { get; set; } = new List<OperationListModelBase>
    {
        new TransactionListModel
        {
            Id = 1,
            Time = DateTime.Now,
            TransactionType = TransactionType.Buy,
            CurrencyCode = "CZK"
        },
        new DonationListModel
        {
            Id = 1,
            Time = DateTime.Now,
            Type = DonationType.Deposit,
            CurrencyCode = "EUR"
        },
        new TransactionListModel
        {
            Id = 2,
            Time = DateTime.Now,
            TransactionType = TransactionType.Sell,
            CurrencyCode = "USD"
        },
        new DonationListModel
        {
            Id = 5,
            Time = DateTime.Now,
            Type = DonationType.Withdraw,
            CurrencyCode = "USD"
        },
        new DonationListModel
        {
            Id = 9,
            Time = DateTime.Now,
            Type = DonationType.Deposit,
            CurrencyCode = "GBP"
        },
        new TransactionListModel
        {
            Id = 3,
            Time = DateTime.Now,
            TransactionType = TransactionType.Buy,
            CurrencyCode = "HUN"
        }
    };
}