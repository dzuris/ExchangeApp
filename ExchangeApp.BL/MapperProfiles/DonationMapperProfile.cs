using AutoMapper;
using ExchangeApp.BL.Extensions;
using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.DAL.Entities;

namespace ExchangeApp.BL.MapperProfiles;

public class DonationMapperProfile : Profile
{
    public DonationMapperProfile()
    {
        CreateMap<DonationEntity, DonationListModel>();
        //CreateMap<DonationEntity, DonationDetailModel>();

        //CreateMap<DonationDetailModel, DonationEntity>()
        //    .Ignore(dst => dst.Employee)
        //    .Ignore(dst => dst.Currency);
    }
}