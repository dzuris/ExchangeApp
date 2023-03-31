using ExchangeApp.BL.Models.Donation;
using ExchangeApp.BL.Models.TotalBalance;
using ExchangeApp.BL.Models.Transaction;

namespace ExchangeApp.App.Services.Interfaces;

public interface IPrinterService
{
    Task SavePdf(TransactionDetailModel model);
    Task SavePdf(DonationDetailModel model);
    Task SavePdf(TotalBalanceModel model);
    Task Print(TransactionDetailModel model);
}