namespace ExchangeApp.BL.Models.Currency;

public record CurrencyProfitModel : ModelBase
{
    public required string Code { get; set; }
    public required string PhotoUrl { get; set; }
    public decimal Profit { get; set; }
}