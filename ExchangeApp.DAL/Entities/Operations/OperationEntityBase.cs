namespace ExchangeApp.DAL.Entities.Operations;

public abstract record OperationEntityBase : IEntity
{
    public int Id { get; set; }
    public DateTime Time { get; set; }
    public decimal CourseRate { get; set; }
    public bool IsCanceled { get; set; }

    public string CurrencyCode { get; set; }
    public CurrencyEntity? Currency { get; set; }
}