using AutoMapper;
using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities.Persons;

namespace ExchangeApp.DAL.Entities;

public record ShutterEntity : IEntity
{
    public required Guid Id { get; set; }
    public required Guid EmployeeId { get; set; }
    public required ShutterType Type { get; set; }
    public required DateOnly Date { get; set; }
    public EmployeeEntity? Employee { get; set; }
}

public class ShutterEntityMapperProfile : Profile
{
    public ShutterEntityMapperProfile()
    {
        CreateMap<ShutterEntity, ShutterEntity>();
    }
}
