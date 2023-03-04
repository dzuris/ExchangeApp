namespace ExchangeApp.DAL.Entities.Customers;

public record BusinessCustomerEntity : CustomerEntity
{
    public required string TradeNameOfTheOwner { get; set; }
    public required string TradeAddress { get; set; }
    public required string ICO { get; set; }
    public required string Nationality { get; set; }
}
