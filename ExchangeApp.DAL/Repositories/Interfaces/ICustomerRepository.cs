using ExchangeApp.DAL.Entities.Customers;

namespace ExchangeApp.DAL.Repositories.Interfaces;

public interface ICustomerRepository : IRepository<CustomerEntity, Guid>
{
    
}