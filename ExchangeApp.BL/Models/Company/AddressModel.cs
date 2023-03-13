namespace ExchangeApp.BL.Models.Company;

public record AddressModel : ModelBase
{
    public string Street { get; set; } = string.Empty;
    public string StreetNumber { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    public static AddressModel Empty => new()
    {
        Street = string.Empty,
        StreetNumber = string.Empty,
        PostalCode = string.Empty,
        City = string.Empty
    };
}