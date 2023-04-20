namespace ExchangeApp.BL.Models.Operations;

public record OperationProfitModel : ModelBase
{
    public int Id { get; set; }
    public required string CurrencyCode { get; set; }
    public required decimal Quantity { get; set; }
    public required decimal CourseRate { get; set; }
    public required decimal Profit { get; set; }
}