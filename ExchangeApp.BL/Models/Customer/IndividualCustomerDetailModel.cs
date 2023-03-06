using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models.Customer;

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
        EvidenceType = EvidenceType.IdentificationCard,
        EvidenceNumber = string.Empty,
        Nationality = string.Empty
    };
}