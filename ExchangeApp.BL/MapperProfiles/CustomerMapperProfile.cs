using AutoMapper;
using ExchangeApp.BL.Models.Customer;
using ExchangeApp.DAL.Entities.Customers;

namespace ExchangeApp.BL.MapperProfiles;

public class CustomerMapperProfile : Profile
{
    public CustomerMapperProfile()
    {
        CreateMap<CustomerEntity, CustomerListModel>();
        CreateMap<CustomerEntity, CustomerDetailModel>()
            .Include<IndividualCustomerEntity, IndividualCustomerDetailModel>()
            .Include<BusinessCustomerEntity, BusinessCustomerDetailModel>()
            .Include<MinorCustomerEntity, MinorCustomerDetailModel>();

        CreateMap<IndividualCustomerDetailModel, IndividualCustomerEntity>().ReverseMap();
        CreateMap<BusinessCustomerDetailModel, BusinessCustomerEntity>().ReverseMap();
        CreateMap<MinorCustomerDetailModel, MinorCustomerEntity>().ReverseMap();
    }
}