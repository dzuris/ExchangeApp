using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace ExchangeApp.DAL.Entities;

public record CurrencyEntity : IEntity
{
    [Key]
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required string State { get; set; }
    public required string Symbol { get; set; }
    public required string PhotoUrl { get; set; }
    public required float MiddleCourse { get; set; }
    public required float AverageCourseRate { get; set; }
    public float? BuyRate { get; set; }
    public float? SellRate { get; set; }
    public float? BuyRateDeviation { get; set; }
    public float? SellRateDeviation { get; set; }
    public float? BuyRateDeviationPercent { get; set; }
    public float? SellRateDeviationPercent { get; set; }
}

public class CurrencyEntityMapperProfile : Profile
{
    public CurrencyEntityMapperProfile()
    {
        CreateMap<CurrencyEntity, CurrencyEntity>();
    }
}
