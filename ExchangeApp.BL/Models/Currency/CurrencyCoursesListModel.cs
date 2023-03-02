using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models.Currency;

public record CurrencyCoursesListModel : ModelBase
{
    public required string Code { get; set; }
    public required string PhotoUrl { get; set; }
    public required decimal AverageCourseRate { get; set; }
    public decimal? BuyRate { get; set; }
    public decimal? SellRate { get; set; }
    public required CurrencyState Status { get; set; }
}