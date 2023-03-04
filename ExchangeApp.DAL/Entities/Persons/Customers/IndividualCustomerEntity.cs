using AutoMapper;

namespace ExchangeApp.DAL.Entities.Persons.Customers;

public record IndividualCustomerEntity : CustomerEntity
{
    public required string Nationality { get; set; }
}
