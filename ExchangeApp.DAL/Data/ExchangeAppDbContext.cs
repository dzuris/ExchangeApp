﻿using ExchangeApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using ExchangeApp.DAL.Entities.Customers;
using ExchangeApp.DAL.Seeds;

namespace ExchangeApp.DAL.Data;

public class ExchangeAppDbContext : DbContext
{
    public DbSet<CurrencyEntity> Currencies => Set<CurrencyEntity>();
    public DbSet<TransactionEntity> Transactions => Set<TransactionEntity>();
    public DbSet<DonationEntity> Donations => Set<DonationEntity>();
    public DbSet<TotalBalanceEntity> TotalBalances => Set<TotalBalanceEntity>();
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

        // Relationship Customer (1) - Transaction (1)
        modelBuilder.Entity<CustomerEntity>()
            .HasOne(i => i.Transaction)
            .WithOne(t => t.Customer)
            .HasForeignKey<CustomerEntity>(i => i.TransactionId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Using table-per-type configuration see https://learn.microsoft.com/en-us/ef/core/modeling/inheritance#table-per-type-configuration
        modelBuilder.Entity<CustomerEntity>().UseTptMappingStrategy();

        CurrencySeeds.Seed(modelBuilder);
    }
}
