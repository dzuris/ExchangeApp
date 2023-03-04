using CommunityToolkit.Mvvm.ComponentModel;
using ExchangeApp.App.Services.Interfaces;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.ViewModels.Transaction;

[QueryProperty(nameof(Transaction), "Transaction")]
public partial class TransactionDetailViewModel : ViewModelBase
{
    private readonly IPrinterService _printerService;

    public TransactionDetailViewModel(IPrinterService printerService)
    {
        _printerService = printerService;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TransactionNumber))]
    [NotifyPropertyChangedFor(nameof(HasTransactionCustomer))]
    [NotifyPropertyChangedFor(nameof(IsTransactionBuy))]
    [NotifyPropertyChangedFor(nameof(IsTransactionSell))]
    private TransactionDetailModel? _transaction;

    public string TransactionNumber
    {
        get
        {
            if (Transaction is null)
            {
                return string.Empty;
            }

            return Transaction.Time.ToString("yyyyMMdd") + " / " + Transaction.Id;
        }
    }

    public bool IsTransactionBuy => Transaction is { TransactionType: TransactionType.Buy };
    public bool IsTransactionSell => !IsTransactionBuy;

    public bool HasTransactionCustomer => Transaction?.CustomerId != null;

    public string DomesticCurrencyCode => "EUR";
}