using AutoMapper;
using ExchangeApp.BL.Models;
using ExchangeApp.DAL.Entities;

namespace ExchangeApp.BL.MapperProfiles;

public class DonationMapperProfile : Profile
{
    public DonationMapperProfile()
    {
        CreateMap<DonationEntity, DonationListModel>();
        CreateMap<DonationEntity, DonationDetailModel>();

        CreateMap<DonationDetailModel, DonationEntity>();
    }
}