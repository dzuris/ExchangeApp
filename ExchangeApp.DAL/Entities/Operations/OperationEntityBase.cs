namespace ExchangeApp.DAL.Entities.Operations;

public abstract record OperationEntityBase : IEntity
{
    public int Id { get; set; }
    public required DateTime Time { get; set; }
    public required decimal Quantity { get; set; }
    public required decimal CurrencyQuantityBefore { get; set; }
    public required decimal CourseRate { get; set; }
    public decimal AverageCourseRate { get; set; }
    public bool IsCanceled { get; set; }

    public required string CurrencyCode { get; set; }
    public CurrencyEntity? Currency { get; set; }
}