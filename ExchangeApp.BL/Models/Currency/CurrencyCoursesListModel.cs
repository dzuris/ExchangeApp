using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models.Currency;

public record CurrencyCoursesListModel : ModelBase
{
    public required string Code { get; set; }
    public required string State { get; set; }
    public required float Quantity { get; set; }
    public required string PhotoUrl { get; set; }
    public required float AverageCourseRate { get; set; }
    public required float BuyRate { get; set; }
    public required float SellRate { get; set; }
    public required CurrencyState Status { get; set; }
}