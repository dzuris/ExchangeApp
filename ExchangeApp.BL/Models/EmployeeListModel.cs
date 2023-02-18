namespace ExchangeApp.BL.Models;

public record EmployeeListModel : ModelBase
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string WholeName => FirstName + " " + LastName;

    public static EmployeeListModel Empty => new()
    {
        FirstName = string.Empty,
        LastName = string.Empty
    };
}