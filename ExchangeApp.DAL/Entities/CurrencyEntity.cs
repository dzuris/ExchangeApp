using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.DAL.Entities;

public record CurrencyEntity : IEntity
{
    [Key]
    public required string Code { get; set; }
    public float Quantity { get; set; }
    public required string PhotoUrl { get; set; }
    public float AverageCourseRate { get; set; } = 1;
    public float? BuyRate { get; set; }
    public float? SellRate { get; set; }
    public CurrencyState Status { get; set; } = CurrencyState.NotInUse;
}
