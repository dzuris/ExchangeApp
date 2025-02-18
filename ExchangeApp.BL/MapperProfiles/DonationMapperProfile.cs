﻿using AutoMapper;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.DAL.Entities.Operations;

namespace ExchangeApp.BL.MapperProfiles;

public class DonationMapperProfile : Profile
{
    public DonationMapperProfile()
    {
        CreateMap<DonationEntity, DonationEntity>();
        CreateMap<DonationEntity, DonationListModel>();

        CreateMap<DonationDetailModel, DonationEntity>()
            .ForMember(dst => dst.CurrencyCode, opt => opt.MapFrom(src => src.CurrencyCode))
            .ForMember(dst => dst.Currency, opt => opt.Ignore());

        CreateMap<DonationEntity, DonationDetailModel>()
            .ForMember(dst => dst.Currency, opt => opt.MapFrom(src => src.Currency));
    }
}