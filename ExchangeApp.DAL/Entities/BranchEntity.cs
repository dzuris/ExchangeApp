using AutoMapper;
using ExchangeApp.DAL.Entities.Persons;
using ExchangeApp.DAL.Entities.Persons.Customers;

namespace ExchangeApp.DAL.Entities;

public record BranchEntity : IEntity
{
    public required Guid Id { get; set; }
    public required Guid CompanyId { get; set; }
    public required string BranchName { get; set; }
    public required string BranchAddress { get; set; }
    public required string BranchPhoneNumber { get; set;}
    public CompanyEntity? Company { get; set; }
    public ICollection<EmployeeEntity> Employees { get; set; } = new List<EmployeeEntity>();
    public ICollection<CustomerEntity> Customers { get; set; } = new List<CustomerEntity>();
}

public class BranchEntityMapperProfile : Profile
{
    public BranchEntityMapperProfile()
    {
        CreateMap<BranchEntity, BranchEntity>();
    }
}
