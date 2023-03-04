namespace ExchangeApp.BL.Models.Donation;

public record DonationListModel : ModelBase
{
    public int Id { get; set; }
    public required DateTime Time { get; set; }
}