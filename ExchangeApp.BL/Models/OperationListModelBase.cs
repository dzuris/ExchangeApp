namespace ExchangeApp.BL.Models;

public record OperationListModelBase : ModelBase
{
    public int Id { get; set; }
    public required DateTime Time { get; set; }
    public required decimal Quantity { get; set; }
    public required string CurrencyCode { get; set; }
}