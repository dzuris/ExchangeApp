﻿using AutoMapper;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.DAL.Entities;

namespace ExchangeApp.BL.MapperProfiles;

public class CurrencyMapperProfile : Profile
{
    public CurrencyMapperProfile()
    {
        CreateMap<CurrencyEntity, CurrencyEntity>()
            .ForMember(dst => dst.History, opt => opt.Ignore());

        CreateMap<CurrencyEntity, CurrencyListModel>();
        CreateMap<CurrencyEntity, CurrencyTransactionListModel>();
        CreateMap<CurrencyEntity, CurrencyCoursesListModel>();

        CreateMap<CurrencyEntity, CurrencyDetailModel>();

        CreateMap<CurrencyDetailModel, CurrencyEntity>()
            .ForMember(dst => dst.History, opt => opt.Ignore());

        CreateMap<CurrencyHistoryEntity, CurrencyHistoryModel>().ReverseMap();
    }
}