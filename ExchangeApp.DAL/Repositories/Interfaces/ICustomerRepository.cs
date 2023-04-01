using ExchangeApp.DAL.Entities.Customers;

namespace ExchangeApp.DAL.Repositories.Interfaces;

public interface ICustomerRepository : IRepository<CustomerEntity, Guid>
{
    Task InsertAsync(IndividualCustomerEntity customer);
    Task InsertAsync(BusinessCustomerEntity customer);
    Task InsertAsync(MinorCustomerEntity customer);
}