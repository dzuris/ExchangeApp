using ExchangeApp.Common.Enums;

namespace ExchangeApp.DAL.Entities;

public record TotalBalanceEntity : IEntity
{
    public required Guid Id { get; set; }
    public required TotalBalanceType Type { get; set; }
    public required DateOnly Date { get; set; }
}