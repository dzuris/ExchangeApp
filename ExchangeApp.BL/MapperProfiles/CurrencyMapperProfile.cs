using AutoMapper;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.DAL.Entities;

namespace ExchangeApp.BL.MapperProfiles;

public class CurrencyMapperProfile : Profile
{
    private const int DecimalsRound = 5;

    public CurrencyMapperProfile()
    {
        CreateMap<CurrencyEntity, CurrencyEntity>();

        CreateMap<CurrencyEntity, CurrencyListModel>()
            .ForMember(dst 
                => dst.AverageCourseRate, opt 
                => opt.MapFrom(src => Math.Round(src.AverageCourseRate, DecimalsRound)));
        CreateMap<CurrencyEntity, CurrencyTransactionListModel>()
            .ForMember(dst 
                => dst.AverageCourseRate, opt 
                => opt.MapFrom(src => Math.Round(src.AverageCourseRate, DecimalsRound)));
        CreateMap<CurrencyEntity, CurrencyCoursesListModel>()
            .ForMember(dst 
                => dst.AverageCourseRate, opt 
                => opt.MapFrom(src => Math.Round(src.AverageCourseRate, DecimalsRound)));

        CreateMap<CurrencyEntity, CurrencyDetailModel>()
            .ForMember(dst 
                    => dst.AverageCourseRate, opt 
                    => opt.MapFrom(src => Math.Round(src.AverageCourseRate, DecimalsRound)));

        CreateMap<CurrencyDetailModel, CurrencyEntity>();
    }
}