using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities.Customers;

namespace ExchangeApp.DAL.Entities.Operations;

public record TransactionEntity : IEntity
{
    public required int Id { get; set; }
    public required DateTime Time { get; set; }
    public required decimal CourseRate { get; set; }
    public required decimal QuantityForeignCurrency { get; set; }
    public required TransactionType TransactionType { get; set; }
    public bool IsCanceled { get; set; }

    public Guid? CustomerId { get; set; }
    public CustomerEntity? Customer { get; set; }

    public required string CurrencyCode { get; set; }
    public CurrencyEntity? Currency { get; set; }
}