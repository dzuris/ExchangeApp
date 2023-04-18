using ExchangeApp.BL.Models.Currency;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.BL.Models.TotalBalance;
using ExchangeApp.BL.Models.Transaction;

namespace ExchangeApp.App.Services.Interfaces;

public interface IPrinterService
{
    Task SavePdf(TransactionDetailModel model, string? fileName = null);
    Task SavePdf(DonationDetailModel model, string? fileName = null);
    Task SavePdf(TotalBalanceModel model, string? fileName = null);
    Task Print(TransactionDetailModel model);
    Task Print(DonationDetailModel model);
    Task Print(TotalBalanceModel model);
    Task Print(List<CurrencyCoursesListModel> currencies);
    Task Print(List<CurrencyListModel> currencies);
    Task Print(List<CurrencyProfitModel> currencies, DateTime from, DateTime until);
}