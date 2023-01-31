using AutoMapper;
using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities.Persons;

namespace ExchangeApp.DAL.Entities;

public record DonationEntity : IEntity
{
    public required int Id { get; set; }
    public required Guid EmployeeId { get; set; }
    public required string CurrencyCode { get; set; }
    public required DateTime Time { get; set; }
    public required float CourseRate { get; set; }
    public required float Quantity { get; set; }
    public required DonationType Type { get; set; }
    public required string Note { get; set; }
    public EmployeeEntity? Employee { get; set; }
    public CurrencyEntity? Currency { get; set; }
}

public class DonationEntityMapperProfile : Profile
{
    public DonationEntityMapperProfile()
    {
        CreateMap<DonationEntity, DonationEntity>();
    }
}
