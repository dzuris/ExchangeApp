using ExchangeApp.Common.Enums;

namespace ExchangeApp.DAL.Entities;

public record TotalBalanceEntity : IEntity
{
    public required int Id { get; set; }
    public required TotalBalanceType Type { get; set; }
    public required DateTime Created { get; set; }
    public DateTime LastTotalBalance { get; set; }
}