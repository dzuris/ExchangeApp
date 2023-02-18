using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models;

public record TransactionDetailModel : ModelBase
{
    public int Id { get; set; }
    public required DateTime Time { get; set; }
    public required float CourseRate { get; set; }
    public required float Quantity { get; set; }
    public required TransactionType TransactionType { get; set; }
    public float Amount => GetAmount(TransactionType, Quantity, CourseRate);
    public float Rounding => GetRounding(Amount);
    public float TotalAmount => Amount + Rounding;

    public required string CurrencyCode { get; set; }
    public CurrencyListModel? Currency { get; set; }

    public required Guid EmployeeId { get; set; }
    public EmployeeListModel? Employee { get; set; }

    public Guid? CustomerId { get; set; }
    public CustomerListModel? Customer { get; set; }

    public static TransactionDetailModel Empty => new()
    {
        Time = DateTime.Now,
        CourseRate = 1,
        Quantity = 0,
        TransactionType = TransactionType.Buy,
        CurrencyCode = "",
        EmployeeId = Guid.Empty
    };

    /// <summary>
    /// Calculates rounding to 0.05.
    /// Values can only be &lt;-0.02,0.02&gt; except of values under 0.03.
    /// Returns 0 when amount is 0.
    /// </summary>
    /// <param name="amount">Not rounded amount</param>
    /// <returns>Round from amount to 0.05</returns>
    private static float GetRounding(float amount)
    {
        if (amount <= 0)
            return 0;

        if (amount < 0.03)
            return 0.05F - amount;

        // Rounds number to 0.05
        var roundedAmount = (float)Math.Round(amount * 20F) / 20F;

        return amount - roundedAmount;
    }

    /// <summary>
    /// Calculates amount of the transaction and round it to two decimals
    /// </summary>
    /// <param name="type">Buy or sell</param>
    /// <param name="quantity">How much money is user changing</param>
    /// <param name="courseRate">Course rate of the transaction</param>
    /// <returns>Rounded amount of the transaction to 2 decimal points</returns>
    private static float GetAmount(TransactionType type, float quantity, float courseRate)
    {
        float result;

        if (type == TransactionType.Buy)
        {
            result = quantity * courseRate;
        }
        else
        {
            result = quantity / courseRate;
        }

        // Rounds number to 2 decimal points
        return (float)Math.Round(result * 100F) / 100F;
    }
}