namespace ExchangeApp.BL.Models.Currency;

public record CurrencyHistoryModel : ModelBase
{
    public Guid Id { get; set; }
    public required string Code { get; set; }
    public required decimal Quantity { get; set; }
    public required decimal AverageCourseRate { get; set; }
    public required DateTime TimeStamp { get; set; }

    public decimal ExchangeRateValue => AverageCourseRate != 0 ? Math.Round(Quantity / AverageCourseRate, 2) : 0;
}