using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Entities.Persons;
using ExchangeApp.DAL.Entities.Persons.Customers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ExchangeApp.DAL.Data;

public class ExchangeAppDbContext : DbContext
{
    public DbSet<CompanyEntity> Companies { get; set; } = null!;
    public DbSet<BranchEntity> Branches { get; set; } = null!;
    public DbSet<CurrencyEntity> Currencies { get; set; } = null!;
    public DbSet<CurrencyRatesEntity> CurrencyRates { get; set; } = null!;
    public DbSet<CurrencySaleEntity> CurrencySales { get; set; } = null!;
    public DbSet<PersonEntity> Persons { get; set; } = null!;
    public DbSet<EmployeeEntity> Employees { get; set; } = null!;
    public DbSet<CustomerEntity> Customers { get; set; } = null!;
    public DbSet<IndividualCustomerEntity> IndividualCustomers { get; set; } = null!;
    public DbSet<BusinessCustomerEntity> BusinessCustomers { get; set; } = null!;
    public DbSet<MinorCustomerEntity> MinorCustomers { get; set; } = null!;
    public DbSet<DonationEntity> Donations { get; set; } = null!;
    public DbSet<ShutterEntity> Shutters { get; set; } = null!;
    public DbSet<TransactionEntity> Transactions { get; set; } = null!;

    public ExchangeAppDbContext(DbContextOptions<ExchangeAppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relationship Company (1) - Branches (n)
        modelBuilder.Entity<CompanyEntity>()
            .HasMany(companyEntity => companyEntity.Branches)
            .WithOne(branchEntity => branchEntity.Company)
            .OnDelete(DeleteBehavior.Cascade);

        // Relationship Branch (1) - Employees (n)
        modelBuilder.Entity<BranchEntity>()
            .HasMany(branchEntity => branchEntity.Employees)
            .WithOne(employeeEntity => employeeEntity.Branch)
            .OnDelete(DeleteBehavior.Cascade);

        // Relationship Branch (1) - Customers (n)
        modelBuilder.Entity<BranchEntity>()
            .HasMany(branchEntity => branchEntity.Customers)
            .WithOne(customerEntity => customerEntity.Branch)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
