﻿using AutoMapper;
using ExchangeApp.BL.Models.Customer;
using ExchangeApp.DAL.Entities.Customers;

namespace ExchangeApp.BL.MapperProfiles;

public class CustomerMapperProfile : Profile
{
    public CustomerMapperProfile()
    {
        CreateMap<CustomerEntity, CustomerListModel>();
    }
}