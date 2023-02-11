using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;

namespace ExchangeApp.DAL.Entities;

public record CurrencySaleEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string CurrencyCode { get; set; }
    public required int ActiveAboutAmount { get; set; }
    public float? Sale { get; set; }
    public float? SalePercent { get; set; }

    [ForeignKey(nameof(CurrencyCode))]
    public CurrencyEntity? Currency { get; set; }
}

public class CurrencySaleEntityMapperProfile : Profile
{
    public CurrencySaleEntityMapperProfile()
    {
        CreateMap<CurrencySaleEntity, CurrencySaleEntity>();
    }
}
