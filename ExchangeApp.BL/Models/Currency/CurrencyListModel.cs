namespace ExchangeApp.BL.Models.Currency;

public record CurrencyListModel : ModelBase
{
    public required string Code { get; set; }
    public required string State { get; set; }
    public float Quantity { get; set; }
}