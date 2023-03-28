namespace ExchangeApp.BL.Models.Customer;

public record CustomerListModel : ModelBase
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string EvidenceNumber { get; set; }
    public string WholeName => FirstName + " " + LastName;
}