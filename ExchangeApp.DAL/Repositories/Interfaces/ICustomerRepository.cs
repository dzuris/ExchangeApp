using ExchangeApp.DAL.Entities.Customers;

namespace ExchangeApp.DAL.Repositories.Interfaces;

public interface ICustomerRepository : IRepository<CustomerEntity, Guid>
{
    new Task InsertAsync(IndividualCustomerEntity customer);
    new Task InsertAsync(BusinessCustomerEntity customer);
    new Task InsertAsync(MinorCustomerEntity customer);
}