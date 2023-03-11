using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities.Customers;

namespace ExchangeApp.DAL.Entities.Operations;

public record TransactionEntity : OperationEntityBase
{
    public required TransactionType TransactionType { get; set; }

    public Guid? CustomerId { get; set; }
    public CustomerEntity? Customer { get; set; }
}