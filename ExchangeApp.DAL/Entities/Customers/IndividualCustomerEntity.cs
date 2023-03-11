namespace ExchangeApp.DAL.Entities.Customers;

public record IndividualCustomerEntity : CustomerEntity
{
    public required string Nationality { get; set; }
}
