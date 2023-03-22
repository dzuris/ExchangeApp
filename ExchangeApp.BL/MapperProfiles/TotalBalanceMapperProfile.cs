using AutoMapper;
using ExchangeApp.BL.Models.TotalBalance;
using ExchangeApp.DAL.Entities;

namespace ExchangeApp.BL.MapperProfiles;

public class TotalBalanceMapperProfile : Profile
{
    public TotalBalanceMapperProfile()
    {
        CreateMap<TotalBalanceEntity, TotalBalanceModel>().ReverseMap();
    }
}