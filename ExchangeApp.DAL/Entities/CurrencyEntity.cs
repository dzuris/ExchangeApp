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
}

public class CurrencyEntityMapperProfile : Profile
{
    public CurrencyEntityMapperProfile()
    {
        CreateMap<CurrencyEntity, CurrencyEntity>();
    }
}
