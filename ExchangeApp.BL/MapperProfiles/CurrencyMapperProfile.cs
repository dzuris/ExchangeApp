using AutoMapper;
using ExchangeApp.BL.Models;
using ExchangeApp.DAL.Entities;

namespace ExchangeApp.BL.MapperProfiles;

public class CurrencyMapperProfile : Profile
{
    public CurrencyMapperProfile()
    {
        CreateMap<CurrencyEntity, CurrencyListModel>();
        CreateMap<CurrencyEntity, CurrencyDetailModel>()
            .ReverseMap();
    }
}