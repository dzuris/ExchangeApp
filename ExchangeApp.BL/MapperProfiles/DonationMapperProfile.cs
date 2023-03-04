using AutoMapper;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.DAL.Entities;

namespace ExchangeApp.BL.MapperProfiles;

public class DonationMapperProfile : Profile
{
    public DonationMapperProfile()
    {
        CreateMap<DonationEntity, DonationListModel>();
        //CreateMap<DonationEntity, DonationDetailModel>();

        CreateMap<DonationDetailModel, DonationEntity>()
            .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.Code))
            .ForMember(dst => dst.Currency, opt => opt.Ignore());
    }
}