using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models.TotalBalance;

public record TotalBalanceModel : ModelBase
{
    public int Id { get; set; }
    public required TotalBalanceType Type { get; set; }
    public required DateTime Created { get; set; }
    public DateTime LastTotalBalance { get; set; }
}