using System.ComponentModel.DataAnnotations;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.DAL.Entities;

public record CurrencyEntity : IEntity
{
    [Key]
    public required string Code { get; set; }
    public decimal Quantity { get; set; }
    public required string PhotoUrl { get; set; }
    public decimal AverageCourseRate { get; set; } = 1;
    public decimal? BuyRate { get; set; }
    public decimal? SellRate { get; set; }
    public CurrencyState Status { get; set; } = CurrencyState.NotInUse;
}
