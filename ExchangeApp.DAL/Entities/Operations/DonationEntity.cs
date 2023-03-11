using ExchangeApp.Common.Enums;

namespace ExchangeApp.DAL.Entities.Operations;

public record DonationEntity : OperationEntityBase
{
    public required DonationType Type { get; set; }
    public required string Note { get; set; }
}