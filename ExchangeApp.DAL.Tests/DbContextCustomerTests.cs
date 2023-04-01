using ExchangeApp.Common.Enums;
using ExchangeApp.Common.Tests;
using ExchangeApp.DAL.Entities.Customers;
using ExchangeApp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ExchangeApp.DAL.Tests;

public class DbContextCustomerTests : DbContextTestsBase
{
    private readonly CustomerRepository _customerRepository;

    public DbContextCustomerTests()
    {
        _customerRepository = new CustomerRepository(ExchangeAppDbContextSUT);
    }

    [Fact]
    public async Task Add_NewIndividualCustomer_ShouldBeInDatabase()
    {
        // Arrange
        var customer = new IndividualCustomerEntity
        {
            Id = Guid.Parse("4EF6BB5E-3898-42FC-A012-F2EA13F2EF93"),
            Created = new DateTime(2023, 2, 20, 10, 11, 11),
            FirstName = "Peťko",
            LastName = "Okoličáni",
            IdentificationNumber = "991212/9999",
            BirthDate = new DateOnly(1999, 12, 12),
            Address = "Adresa pri riečke 17, Nové mesto nad Váhom",
            EvidenceType = EvidenceType.IdentificationCard,
            EvidenceNumber = "AA000000",
            Nationality = "Slovenská"
        };

        // Act
        await _customerRepository.InsertAsync(customer);
        await ExchangeAppDbContextSUT.SaveChangesAsync();

        // Assert
        var databaseEntity =
            await ExchangeAppDbContextSUT.IndividualCustomers.SingleOrDefaultAsync(e => e.Id == customer.Id);
        Assert.NotNull(databaseEntity);
        DeepAssert.Equal(customer, databaseEntity);
    }
}