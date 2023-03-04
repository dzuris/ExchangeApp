using AutoMapper;
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
}