using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models.Currency;

public record CurrencyListModel : ModelBase
{
    public required string Code { get; set; }
    public decimal? AverageCourseRate { get; set; }
    public decimal Quantity { get; set; }
    public required string PhotoUrl { get; set; }
    public required CurrencyState Status { get; set; }

    public decimal ExchangeRateValue => AverageCourseRate.HasValue && AverageCourseRate.Value != 0 ? Math.Round(Quantity / AverageCourseRate.Value, 2) : 0;
}