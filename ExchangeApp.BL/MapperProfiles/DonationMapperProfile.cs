using AutoMapper;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Entities.Operations;

namespace ExchangeApp.BL.MapperProfiles;

public class DonationMapperProfile : Profile
{
    public DonationMapperProfile()
    {
        CreateMap<DonationEntity, DonationListModel>()
            .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dst => dst.Time, opt => opt.MapFrom(src => src.Time))
            .ForMember(dst => dst.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dst => dst.CurrencyCode, opt => opt.MapFrom(src => src.CurrencyCode));
        //CreateMap<DonationEntity, DonationDetailModel>();

        CreateMap<DonationDetailModel, DonationEntity>()
            .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.CurrencyCode))
            .ForMember(dst => dst.Currency, opt => opt.Ignore());
    }
}