using ExchangeApp.BL.Models.Operations;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models.Donation;

public record DonationListModel : OperationListModelBase
{
    public required DonationType Type { get; set; }
    public required string Note { get; set; }
}