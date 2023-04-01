using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities.Customers;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.Common.Tests.Seeds;

public static class CustomerSeeds
{
    public static readonly IndividualCustomerEntity EmptyIndividualCustomer = new()
    {
        Id = default,
        Created = default,
        FirstName = string.Empty,
        LastName = string.Empty,
        IdentificationNumber = default,
        BirthDate = default,
        Address = string.Empty,
        EvidenceType = default,
        EvidenceNumber = string.Empty,
        Nationality = string.Empty
    };

    public static readonly IndividualCustomerEntity IndividualCustomerOne = new()
    {
        Id = Guid.Parse("244E9E15-0860-4E51-AA42-BE02A99CB36E"),
        Created = new DateTime(2020, 9, 12, 16, 32, 54),
        FirstName = "Harry",
        LastName = "Maguire",
        IdentificationNumber = "930305/1",
        BirthDate = new DateOnly(1993, 3, 5),
        Address = "Manchester",
        EvidenceType = EvidenceType.DriverCard,
        EvidenceNumber = "AA111111",
        Nationality = "England"
    };

    public static readonly BusinessCustomerEntity BusinessCustomerOne = new()
    {
        Id = Guid.Parse("538C04EE-220B-4832-B6C2-D7D499D05F61"),
        Created = new DateTime(2018, 5, 6, 9, 33, 0),
        FirstName = "Snoop",
        LastName = "Dogg",
        IdentificationNumber = "7512091546",
        BirthDate = new DateOnly(1975, 9, 12),
        Address = "River street 5, New York",
        EvidenceType = EvidenceType.IdentificationCard,
        EvidenceNumber = "BB000000",
        Nationality = "American",
        TradeNameOfTheOwner = "My company s.r.o.",
        TradeAddress = "My company street 123",
        ICO = "4651634"
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IndividualCustomerEntity>().HasData(
            IndividualCustomerOne);

        modelBuilder.Entity<BusinessCustomerEntity>().HasData(
            BusinessCustomerOne);
    }
}