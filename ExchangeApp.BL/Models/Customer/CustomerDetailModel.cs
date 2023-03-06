using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models.Customer;

public record CustomerDetailModel : ModelBase
{
    public Guid Id { get; set; }
    public required DateTime Created { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? IdentificationNumber { get; set; }
    public DateOnly? BirthDate { get; set; }
    public required string Address { get; set; }
    public required EvidenceType EvidenceType { get; set; }
    public required string EvidenceNumber { get; set; }
}