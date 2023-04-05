namespace ExchangeApp.DAL.Entities;

public record CurrencyHistoryEntity
{
    public Guid Id { get; set; }
    public required string Code { get; set; }
    public required decimal Quantity { get; set; }
    public required decimal AverageCourseRate { get; set; }
    public required DateTime TimeStamp { get; set; }
}