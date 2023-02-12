using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Entities.Persons;
using ExchangeApp.DAL.Entities.Persons.Customers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using ExchangeApp.DAL.Seeds;

namespace ExchangeApp.DAL.Data;

public class ExchangeAppDbContext : DbContext
{
    public DbSet<CurrencyEntity> Currencies => Set<CurrencyEntity>();
    public DbSet<CurrencySaleEntity> CurrencySales => Set<CurrencySaleEntity>();
    public DbSet<TransactionEntity> Transactions => Set<TransactionEntity>();
    public DbSet<DonationEntity> Donations => Set<DonationEntity>();
    public DbSet<TotalBalanceEntity> TotalBalances => Set<TotalBalanceEntity>();
    public DbSet<PersonEntity> Persons => Set<PersonEntity>();
    public DbSet<EmployeeEntity> Employees => Set<EmployeeEntity>();
    public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();
    public DbSet<IndividualCustomerEntity> IndividualCustomers => Set<IndividualCustomerEntity>();
    public DbSet<BusinessCustomerEntity> BusinessCustomers => Set<BusinessCustomerEntity>();
    public DbSet<MinorCustomerEntity> MinorCustomers => Set<MinorCustomerEntity>();

    public ExchangeAppDbContext(DbContextOptions<ExchangeAppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relationship Employee (1) - Total Balance (n)
        modelBuilder.Entity<EmployeeEntity>()
            .HasMany<TotalBalanceEntity>()
            .WithOne(i => i.Employee)
            .OnDelete(DeleteBehavior.Restrict);

        // Relationship Employee (1) - Donation (n)
        modelBuilder.Entity<EmployeeEntity>()
            .HasMany<DonationEntity>()
            .WithOne(i => i.Employee)
            .OnDelete(DeleteBehavior.Restrict);

        // Relationship Employee (1) - Transaction (n)
        modelBuilder.Entity<EmployeeEntity>()
            .HasMany<TransactionEntity>()
            .WithOne(i => i.Employee)
            .OnDelete(DeleteBehavior.Restrict);

        // Relationship Currency (1) - Donation (n)
        modelBuilder.Entity<CurrencyEntity>()
            .HasMany<DonationEntity>()
            .WithOne(i => i.Currency)
            .OnDelete(DeleteBehavior.Restrict);

        // Relationship Currency (1) - Transaction (n)
        modelBuilder.Entity<CurrencyEntity>()
            .HasMany<TransactionEntity>()
            .WithOne(i => i.Currency)
            .OnDelete(DeleteBehavior.Restrict);

        // Relationship Currency (1) - Currency sale (n)
        modelBuilder.Entity<CurrencyEntity>()
            .HasMany<CurrencySaleEntity>()
            .WithOne(i => i.Currency)
            .OnDelete(DeleteBehavior.Restrict);

        // Relationship Transaction (1) - Customer (1)
        modelBuilder.Entity<TransactionEntity>()
            .HasOne(i => i.Customer)
            .WithOne(t => t.Transaction)
            .OnDelete(DeleteBehavior.Restrict);

        CurrencySeeds.Seed(modelBuilder);
    }
}
