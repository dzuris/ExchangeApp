using ExchangeApp.BL.Models.Currency;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models.Donation;

public record DonationDetailModel : ModelBase
{
    public int Id { get; set; }
    public required DateTime Time { get; set; }
    public required decimal CourseRate { get; set; }
    public decimal AverageCourseRate { get; set; }
    public required decimal Quantity { get; set; }
    public required decimal CurrencyQuantityBefore { get; set; }
    public required DonationType Type { get; set; }
    public required string Note { get; set; }
    public bool IsCanceled { get; set; }

    public required string CurrencyCode { get; set; }
    public CurrencyListModel? Currency { get; set; }

    public static DonationDetailModel Empty => new()
    {
        Time = DateTime.Now,
        CourseRate = 0,
        Quantity = 0,
        CurrencyQuantityBefore = 0,
        Type = DonationType.Deposit,
        Note = string.Empty,
        CurrencyCode = string.Empty
    };
}