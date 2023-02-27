namespace ExchangeApp.BL.Models.Currency;

public record CurrencyNewTransactionModel : ModelBase
{
    public required string Code { get; set; }
    public required float Quantity { get; set; }
    public required float BuyRate { get; set; }
    public required float SellRate { get; set; }
}