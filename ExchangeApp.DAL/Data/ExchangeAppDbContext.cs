using ExchangeApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using ExchangeApp.DAL.Entities.Customers;
using ExchangeApp.DAL.Entities.Operations;
using ExchangeApp.DAL.Seeds;

namespace ExchangeApp.DAL.Data;

public class ExchangeAppDbContext : DbContext
{
    private readonly bool _seedCurrencyData;

    public DbSet<CurrencyEntity> Currencies => Set<CurrencyEntity>();
    public DbSet<CurrencyHistoryEntity> CurrenciesHistory => Set<CurrencyHistoryEntity>();
    public DbSet<OperationEntityBase> Operations => Set<OperationEntityBase>();
    public DbSet<TransactionEntity> Transactions => Set<TransactionEntity>();
    public DbSet<DonationEntity> Donations => Set<DonationEntity>();
    public DbSet<TotalBalanceEntity> TotalBalances => Set<TotalBalanceEntity>();
    public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();
    public DbSet<IndividualCustomerEntity> IndividualCustomers => Set<IndividualCustomerEntity>();
    public DbSet<BusinessCustomerEntity> BusinessCustomers => Set<BusinessCustomerEntity>();
    public DbSet<MinorCustomerEntity> MinorCustomers => Set<MinorCustomerEntity>();

    public ExchangeAppDbContext(DbContextOptions options, bool seedCurrencyData = true) : base(options)
    {
        _seedCurrencyData = seedCurrencyData;
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

        // Relationship Transaction (1) - Customer (1)
        modelBuilder.Entity<CustomerEntity>()
            .HasOne<TransactionEntity>()
            .WithOne(i => i.Customer)
            .OnDelete(DeleteBehavior.Restrict);

        // Auto-generated Id for operations
        modelBuilder.Entity<OperationEntityBase>()
            .Property(o => o.Id)
            .ValueGeneratedOnAdd();

        // Unique total balance rows
        modelBuilder.Entity<TotalBalanceEntity>()
            .HasIndex(e => new { e.Type, e.Created })
            .IsUnique();

        // Using table-per-type configuration see https://learn.microsoft.com/en-us/ef/core/modeling/inheritance#table-per-type-configuration
        modelBuilder.Entity<CustomerEntity>().UseTptMappingStrategy();

        if (_seedCurrencyData)
        {
            CurrencySeeds.Seed(modelBuilder);
        }
    }
}
