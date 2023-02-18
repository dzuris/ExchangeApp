namespace ExchangeApp.BL.Models;

public record CustomerListModel : ModelBase
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string WholeName => FirstName + " " + LastName;
}