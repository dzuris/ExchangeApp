using ExchangeApp.Common.Enums;

namespace ExchangeApp.DAL.Entities.Operations;

public record DonationEntity : OperationEntityBase
{
    public required decimal Quantity { get; set; }
    public required DonationType Type { get; set; }
    public required string Note { get; set; }
}