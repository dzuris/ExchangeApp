using ExchangeApp.BL.Models.Customer;
using ExchangeApp.BL.Models.Operations;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models.Transaction;

public record TransactionListModel : OperationListModelBase
{
    public required TransactionType TransactionType { get; set; }

    public Guid? CustomerId { get; set; }
    public CustomerListModel? Customer { get; set; }

    public decimal Rounding => GetRounding(ExchangeRateValue);
    public decimal TotalAmount => ExchangeRateValue + Rounding;

    /// <summary>
    /// Calculates rounding to 0.05.
    /// Values can only be &lt;-0.02,0.02&gt; except of values under 0.03.
    /// Returns 0 when amount is 0.
    /// </summary>
    /// <param name="amount">Not rounded amount</param>
    /// <returns>Round from amount to 0.05</returns>
    private static decimal GetRounding(decimal amount)
    {
        switch (amount)
        {
            case <= 0:
                return 0;
            case < 0.03M:
                return 0.05M - amount;
            default:
            {
                // Rounds number to 0.05
                var roundedAmount = Math.Round(amount * 20) / 20;

                return roundedAmount - amount;
            }
        }
    }
}