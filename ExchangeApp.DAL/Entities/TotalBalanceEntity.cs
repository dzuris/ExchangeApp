using AutoMapper;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.DAL.Entities;

public record TotalBalanceEntity : IEntity
{
    public required Guid Id { get; set; }
    public required Guid EmployeeId { get; set; }
    public required TotalBalanceType Type { get; set; }
    public required DateOnly Date { get; set; }
}

public class TotalBalanceEntityMapperProfile : Profile
{
    public TotalBalanceEntityMapperProfile()
    {
        CreateMap<TotalBalanceEntity, TotalBalanceEntity>();
    }
}
