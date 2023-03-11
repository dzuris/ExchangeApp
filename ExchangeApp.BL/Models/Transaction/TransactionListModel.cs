using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models.Transaction;

public record TransactionListModel : OperationListModelBase
{
    public required TransactionType TransactionType { get; set; }
}