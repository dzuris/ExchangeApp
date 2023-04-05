using System.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.App.Services.Interfaces;
using ExchangeApp.App.Views.Customers;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;
using ExchangeApp.Common.Exceptions;

namespace ExchangeApp.App.ViewModels.Transaction;

[QueryProperty(nameof(Transaction), "Transaction")]
[QueryProperty(nameof(Id), "id")]
public partial class TransactionDetailViewModel : ViewModelBase
{
    private readonly ITransactionFacade _transactionFacade;
    private readonly ISettingsFacade _settingsFacade;
    private readonly IPrinterService _printerService;

    public TransactionDetailViewModel(IPrinterService printerService, ITransactionFacade transactionFacade, ISettingsFacade settingsFacade)
    {
        _printerService = printerService;
        _transactionFacade = transactionFacade;
        _settingsFacade = settingsFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Transaction ??= await _transactionFacade.GetById(Id);
    }

    [ObservableProperty]
    private int _id;

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

            return Transaction.Created.ToString("yyyyMMdd") + " / " + Transaction.Id;
        }
    }

    public bool IsTransactionBuy => Transaction is { TransactionType: TransactionType.Buy };
    public bool IsTransactionSell => !IsTransactionBuy;

    public bool HasTransactionCustomer => Transaction?.CustomerId != null;

    public string DomesticCurrencyCode => "EUR";

    [RelayCommand]
    private async Task GoToCustomerDetailAsync()
    {
        if (Transaction?.CustomerId is null)
        {
            return;
        }

        var customerId = Transaction.CustomerId ?? Guid.Empty;
        await Shell.Current.GoToAsync($"{nameof(CustomerDetailPage)}?id={customerId}");
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        if (Transaction is null) return;

        var rm = new ResourceManager(typeof(TransactionDetailPageResources));

        var result = await Application.Current?.MainPage?.DisplayAlert(
            rm.GetString("StornoAlertConfirmationTitle"),
            rm.GetString("StornoAlertConfirmationMessage"),
            rm.GetString("StornoAlertConfirmationYesButton"),
            rm.GetString("StornoAlertConfirmationNoButton"))!;

        if (!result) return;

        try
        {
            await _transactionFacade.CancelTransaction(Transaction);
        }
        catch (OperationCanNotBeCanceledException)
        {
            await Application.Current.MainPage?.DisplayAlert(
                rm.GetString("StornoAlertErrorTitle"),
                rm.GetString("StornoAlertErrorMessageClosedTransaction"),
                rm.GetString("AlertCancelButton"))!;
            return;
        }
        catch (InsufficientMoneyException)
        {
            await Application.Current.MainPage?.DisplayAlert(
                rm.GetString("StornoAlertErrorTitle"),
                rm.GetString("StornoAlertErrorMessageInsufficientMoney"),
                rm.GetString("AlertCancelButton"))!;
            return;
        }
        catch (ArgumentNullException)
        {
            return;
        }

        Transaction.IsCanceled = true;
        OnPropertyChanged(nameof(Transaction));

        if (await _settingsFacade.ShouldSaveTransactionsAutomaticallyAsync())
        {
            try
            {
                await _printerService.SavePdf(Transaction);
            }
            catch (ArgumentNullException)
            {
            }
        }

        await Application.Current.MainPage?.DisplayAlert(
            rm.GetString("StornoAlertTitle"),
            rm.GetString("StornoAlertMessage"),
            rm.GetString("AlertCancelButton"))!;
    }

    [RelayCommand]
    private async Task SaveTransactionToFolderAsync()
    {
        if (Transaction is null) return;

        var rm = new ResourceManager(typeof(TransactionDetailPageResources));

        try
        {
            await _printerService.SavePdf(Transaction);
        }
        catch (ArgumentNullException)
        {
            await Application.Current?.MainPage?.DisplayAlert(
                rm.GetString("PdfDownloadAlertTitle"),
                rm.GetString("PdfDownloadAlertErrorMessage"),
                rm.GetString("AlertCancelButton"))!;
            return;
        }
        
        await Application.Current?.MainPage?.DisplayAlert(
            rm.GetString("PdfDownloadAlertTitle"), 
            rm.GetString("PdfDownloadAlertMessage"), 
            rm.GetString("AlertCancelButton"))!;
    }

    [RelayCommand]
    private async Task PrintAsync()
    {
        if (Transaction is null) return;
        await _printerService.Print(Transaction);
    }
}