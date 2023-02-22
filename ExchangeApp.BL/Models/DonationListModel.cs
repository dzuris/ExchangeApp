namespace ExchangeApp.BL.Models;

public record DonationListModel : ModelBase
{
    public int Id { get; set; }
    public required DateTime Time { get; set; }

    public required Guid EmployeeId { get; set; }
    public EmployeeListModel? Employee { get; set; }
}