using System.Diagnostics;
using ExchangeApp.App.Services.Interfaces;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Company;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.BL.Models.TotalBalance;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;
using iText.Kernel.Geom;
using Path = System.IO.Path;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;

namespace ExchangeApp.App.Services;

public partial class PrinterService : IPrinterService
{
    private readonly ISettingsFacade _settingsFacade;
    private readonly ICurrencyFacade _currencyFacade;
    private readonly IDonationFacade _donationFacade;
    private readonly ITransactionFacade _transactionFacade;
    private readonly IOperationFacade _operationFacade;
    private readonly ITotalBalanceFacade _totalBalanceFacade;
    private readonly ICustomerFacade _customerFacade;

    private const string DomesticCurrencyCode = "EUR";
    private const string DecimalFormatTwoDecimals = "### ### ### ##0.00;-# ##0.00";
    private const string AverageCourseFormat = "0.000000";
    private static readonly PageSize OperationDocumentPageSize = PageSize.A5;
    private const string BoldFontFile = "Sono-SemiBold.ttf";
    private const string CommonFontFile = "Sono-Regular.ttf";
    private const int HeaderFontSize = 10;
    private const int CommonFontSize = 8;
    private const int SmallFontSize = 6;

    public PrinterService(
        ISettingsFacade settingsFacade, 
        ICurrencyFacade currencyFacade, 
        IDonationFacade donationFacade, 
        ITransactionFacade transactionFacade, 
        IOperationFacade operationFacade, 
        ITotalBalanceFacade totalBalanceFacade, 
        ICustomerFacade customerFacade)
    {
        _settingsFacade = settingsFacade;
        _currencyFacade = currencyFacade;
        _donationFacade = donationFacade;
        _transactionFacade = transactionFacade;
        _operationFacade = operationFacade;
        _totalBalanceFacade = totalBalanceFacade;
        _customerFacade = customerFacade;
    }

    public async Task Print(TransactionDetailModel model)
    {
        var fileName = await GetTransactionFileNameWithPath(model);

        if (fileName is null) return;

        if (!File.Exists(fileName)) return;

        var info = new ProcessStartInfo(fileName)
        {
            Verb = "PrintTo",
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden
        };
        Process.Start(info);

        //try
        //{
        //    var info = new ProcessStartInfo(fileName)
        //    {
        //        UseShellExecute = true
        //    };

        //    Process.Start(info);
        //}
        //catch (System.ComponentModel.Win32Exception ex)
        //{
        //    Debug.WriteLine(ex.Message);
        //    await Application.Current?.MainPage?.DisplayAlert(
        //        "Error", "An error occurred while printing the file.",
        //        "OK")!;
        //}
        //catch (Exception ex)
        //{
        //    Debug.WriteLine(ex.Message);
        //    await Application.Current?.MainPage?.DisplayAlert(
        //        "Error", "An error 2 occurred while printing the file.",
        //        "OK")!;
        //}
    }
    
    private async Task<string?> GetTransactionFileNameWithPath(TransactionDetailModel model)
    {
        var saveFolderPath = await _settingsFacade.GetSaveFolderPathAsync();

        if (saveFolderPath is null) return null;

        var year = model.Time.Year.ToString();
        var month = model.Time.Month.ToString();

        var directory = Path.Combine(saveFolderPath, year, month, "Transakcie");

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var transactionTypeString = model.TransactionType == TransactionType.Buy
            ? "nákup"
            : "predaj";
        var fileName = Path.Combine(directory,
            model.IsCanceled
                ? $"{model.Id}_{transactionTypeString}_storno.pdf"
                : $"{model.Id}_{transactionTypeString}.pdf");

        return fileName;
    }

    private async Task<string?> GetDonationFileNameWithPath(DonationDetailModel model)
    {
        var saveFolderPath = await _settingsFacade.GetSaveFolderPathAsync();

        if (saveFolderPath is null) return null;

        var year = model.Time.Year.ToString();
        var month = model.Time.Month.ToString();

        var directory = Path.Combine(saveFolderPath, year, month, "Dotácie");

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var fileName = Path.Combine(directory, model.IsCanceled ? $"{model.Id}_storno.pdf" : $"{model.Id}.pdf");

        return fileName;
    }

    private async Task<string?> GetTotalBalanceFileNameWithPath(TotalBalanceModel model)
    {
        var saveFolderPath = await _settingsFacade.GetSaveFolderPathAsync();

        if (saveFolderPath is null) return null;

        var year = model.Created.Year.ToString();
        var month = model.Created.Month.ToString();

        var directory = model.Type == TotalBalanceType.Monthly
            ? Path.Combine(saveFolderPath, year, month, "Uzávierky")
            : Path.Combine(saveFolderPath, year, month, "Uzávierky", "Denné");

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var fileName = model.Type switch
        {
            TotalBalanceType.Daily => Path.Combine(directory,
                $"{model.Id}_{model.Created.ToString("yyMMdd")}_denná.pdf"),
            TotalBalanceType.Monthly => Path.Combine(directory, $"{model.Id}_mesačná.pdf"),
            _ => null
        };

        return fileName;
    }
    
    private static string AddressToString(AddressModel address) 
        => $"{address.Street} {address.StreetNumber},  {address.PostalCode} {address.City}";
}