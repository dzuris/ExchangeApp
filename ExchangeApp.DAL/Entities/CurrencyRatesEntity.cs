using AutoMapper;

namespace ExchangeApp.DAL.Entities;

public record CurrencyRatesEntity : IEntity
{
    public required Guid Id { get; set; }
    public required Guid BranchId { get; set; }
    public required string CurrencyCode { get; set; }
    public required float AverageCourseRate { get; set; }
    public float? BuyRate { get; set; }
    public float? SellRate { get; set; }
    public float? BuyRateDeviation { get; set; }
    public float? SellRateDeviation { get; set; }
    public float? BuyRateDeviationPercent { get; set; }
    public float? SellRateDeviationPercent { get; set; }
}

public class CurrencyRatesEntityMapperProfile : Profile
{
    public CurrencyRatesEntityMapperProfile()
    {
        CreateMap<CurrencyRatesEntity, CurrencyRatesEntity>();
    }
}
