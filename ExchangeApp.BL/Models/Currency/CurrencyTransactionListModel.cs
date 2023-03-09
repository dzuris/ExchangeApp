namespace ExchangeApp.BL.Models.Currency;

public record CurrencyTransactionListModel : ModelBase
{
    public required string Code { get; set; }
    public decimal? AverageCourseRate { get; set; }
    public required decimal Quantity { get; set; }
    public required decimal BuyRate { get; set; }
    public required decimal SellRate { get; set; }
}