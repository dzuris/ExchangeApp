using ExchangeApp.Common.Enums;

namespace ExchangeApp.DAL.Entities.Customers;

public record CustomerEntity : IEntity
{
    public required Guid Id { get; set; }
    public required DateTime Created { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? IdentificationNumber { get; set; }
    public DateOnly? BirthDate { get; set; }
    public required string Address { get; set; }
    public required EvidenceType EvidenceType { get; set; }
    public required string EvidenceNumber { get; set; }

    public required int TransactionId { get; set; }
    public TransactionEntity? Transaction { get; set; }
}
