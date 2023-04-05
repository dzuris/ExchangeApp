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

    private const string TemporaryFolderName = "ExchangeAppFolder";
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

    private static string GetTemporaryFolder()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), TemporaryFolderName);

        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }

        return tempDir;
    }

    private static string AddressToString(AddressModel address) 
        => $"{address.Street} {address.StreetNumber},  {address.PostalCode} {address.City}";
}