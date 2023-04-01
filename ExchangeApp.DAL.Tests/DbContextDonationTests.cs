using ExchangeApp.Common.Tests;
using ExchangeApp.Common.Tests.Seeds;
using ExchangeApp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ExchangeApp.DAL.Tests;

public class DbContextDonationTests : DbContextTestsBase
{
    private readonly DonationRepository _donationRepository;

    public DbContextDonationTests()
    {
        _donationRepository = new DonationRepository(ExchangeAppDbContextSUT, Mapper);
    }

    [Fact]
    public async Task InsertDonation_ShouldBeInDatabase()
    {
        // Arrange
        var entity = DonationSeeds.DonationToInsert;

        // Act
        await _donationRepository.InsertAsync(entity);
        await ExchangeAppDbContextSUT.SaveChangesAsync();

        // Assert
        var databaseEntity = 
            await ExchangeAppDbContextSUT.Donations.SingleOrDefaultAsync(e => e.Id == entity.Id);
        Assert.NotNull(databaseEntity);
        DeepAssert.Equal(entity, databaseEntity);
    }

    [Fact]
    public async Task GetDonationsList_FromUntilDates_ShouldHaveCount_One()
    {
        // Arrange
        var dateFrom = new DateTime(2020, 1, 1);
        var dateUntil = dateFrom.AddYears(1);

        // Act
        var donations = await _donationRepository.GetDonations(dateFrom, dateUntil);

        // Assert
        Assert.Equal(2, donations.Count());
    }
}