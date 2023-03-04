using ExchangeApp.DAL.Entities.Persons.Customers;

namespace ExchangeApp.DAL.Repositories.Interfaces;

public interface ICustomerRepository : IRepository<CustomerEntity, Guid>
{
    
}