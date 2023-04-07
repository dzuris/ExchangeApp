namespace ExchangeApp.BL.Models.Company;

public record BranchDetailModel : ModelBase
{
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public AddressModel Address { get; set; } = AddressModel.Empty;

    public static BranchDetailModel Empty => new();
}