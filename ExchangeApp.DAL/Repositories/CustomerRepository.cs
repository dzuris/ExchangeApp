using ExchangeApp.DAL.Entities.Customers;
using ExchangeApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Repositories;

public class CustomerRepository : RepositoryBase<CustomerEntity, Guid>, ICustomerRepository
{
    public CustomerRepository(DbContext appDbContext) 
        : base(appDbContext)
    {
    }

    public async Task InsertAsync(IndividualCustomerEntity customer)
    {
        await AppDbContext
            .Set<IndividualCustomerEntity>()
            .AddAsync(customer);
    }

    public async Task InsertAsync(BusinessCustomerEntity customer)
    {
        await AppDbContext
            .Set<BusinessCustomerEntity>()
            .AddAsync(customer);
    }

    public async Task InsertAsync(MinorCustomerEntity customer)
    {
        await AppDbContext
            .Set<MinorCustomerEntity>()
            .AddAsync(customer);
    }
}