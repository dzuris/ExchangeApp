using ExchangeApp.BL.Models.Currency;
using ExchangeApp.BL.Models.Person;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models.Donation;

public record DonationDetailModel : ModelBase
{
    public int Id { get; set; }
    public required DateTime Time { get; set; }
    public required float CourseRate { get; set; }
    public required float Quantity { get; set; }
    public required DonationType Type { get; set; }
    public required string Note { get; set; }
    public bool IsCanceled { get; set; }

    public Guid? EmployeeId { get; set; }
    public EmployeeListModel? Employee { get; set; }

    public required string Code { get; set; }
    public CurrencyListModel? Currency { get; set; }

    public static DonationDetailModel Empty => new()
    {
        Time = DateTime.Now,
        CourseRate = 0,
        Quantity = 0,
        Type = DonationType.Deposit,
        Note = string.Empty,
        Code = string.Empty
    };
}