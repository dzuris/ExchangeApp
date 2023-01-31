using AutoMapper;
using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities.Persons;

namespace ExchangeApp.DAL.Entities.Persons.Customers;

public record CustomerEntity : PersonEntity
{
    public string? IdentificationNumber { get; set; }
    public DateOnly? BirthDate { get; set; }
    public required string Address { get; set; }
    public required EvidenceType EvidenceType { get; set; }
    public required string EvidenceNumber { get; set; }
}

public class CustomerEntityMapperProfile : Profile
{
    public CustomerEntityMapperProfile()
    {
        CreateMap<CustomerEntity, CustomerEntity>();
    }
}
