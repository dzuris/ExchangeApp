using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models;

public record IndividualCustomerDetailModel : CustomerDetailModel
{
    public required string Nationality { get; set; }

    public static IndividualCustomerDetailModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Created = DateTime.Now,
        FirstName = string.Empty,
        LastName = string.Empty,
        Address = string.Empty,
        EvidenceType = EvidenceType.None,
        EvidenceNumber = string.Empty,
        TransactionId = 0,
        Nationality = string.Empty
    };
}