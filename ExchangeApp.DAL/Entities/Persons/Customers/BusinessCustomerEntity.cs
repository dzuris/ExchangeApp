using AutoMapper;

namespace ExchangeApp.DAL.Entities.Persons.Customers;

public record BusinessCustomerEntity : CustomerEntity
{
    public required string TradeNameOfTheOwner { get; set; }
    public required string TradeAddress { get; set; }
    public required string ICO { get; set; }
    public required string Nationality { get; set; }
}

public class BusinessCustomerEntityMapperProfile : Profile
{
    public BusinessCustomerEntityMapperProfile()
    {
        CreateMap<BusinessCustomerEntity, BusinessCustomerEntity>();
    }
}
