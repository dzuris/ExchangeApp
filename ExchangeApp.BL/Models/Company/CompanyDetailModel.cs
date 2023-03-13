using System.ComponentModel.DataAnnotations;

namespace ExchangeApp.BL.Models.Company;

public record CompanyDetailModel : ModelBase
{
    public string TradeNameOfTheOwner { get; set; } = string.Empty;
    public string Ico { get; set; } = string.Empty;
    // DIC
    public string Tin { get; set; } = string.Empty;
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;
    public AddressModel Address { get; set; } = AddressModel.Empty;

    public static CompanyDetailModel Empty => new();
}