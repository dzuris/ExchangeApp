using AutoMapper;

namespace ExchangeApp.DAL.Entities;

public record CompanyEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string OwnerTradeName { get; set; }
    public required string ICO { get; set; }
    public required string DIC { get; set; }
    public required string OwnerAddressStreetNumber { get; set; }
    public required string OwnerAddressPSC { get; set; }
    public required string OwnerAddressCity { get; set; }
    public string? PhoneNumber { get; set; }
    public ICollection<BranchEntity> Branches { get; set; } = new List<BranchEntity>();
}

public class CompanyEntityMapperProfile : Profile
{
    public CompanyEntityMapperProfile()
    {
        CreateMap<CompanyEntity, CompanyEntity>();
    }
}
