using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models.Currency;

public record CurrencyListModel : ModelBase
{
    public required string Code { get; set; }
    public decimal AverageCourseRate { get; set; }
    public decimal Quantity { get; set; }
    public required string PhotoUrl { get; set; }
    public required CurrencyStatus Status { get; set; }

    public decimal ExchangeRateValue => AverageCourseRate != 0 ? Math.Round(Quantity / AverageCourseRate, 2) : 0;
}