using AutoMapper;
using ExchangeApp.BL.Extensions;
using ExchangeApp.BL.Models;
using ExchangeApp.DAL.Entities.Persons;

namespace ExchangeApp.BL.MapperProfiles;

public class EmployeeMapperProfile : Profile
{
    public EmployeeMapperProfile()
    {
        CreateMap<EmployeeEntity, EmployeeListModel>()
            .Ignore(dst => dst.WholeName);
    }
}