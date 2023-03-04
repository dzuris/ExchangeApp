using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models.Customer;

public record MinorCustomerDetailModel : CustomerDetailModel
{
    public static MinorCustomerDetailModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Created = DateTime.Now,
        FirstName = string.Empty,
        LastName = string.Empty,
        Address = string.Empty,
        EvidenceType = EvidenceType.IdentificationCard,
        EvidenceNumber = string.Empty,
        TransactionId = 0
    };
}