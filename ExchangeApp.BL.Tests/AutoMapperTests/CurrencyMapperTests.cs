using ExchangeApp.BL.Models.Currency;
using ExchangeApp.Common.Enums;
using ExchangeApp.Common.Tests;
using ExchangeApp.Common.Tests.Seeds;
using ExchangeApp.DAL.Entities;

namespace ExchangeApp.BL.Tests.AutoMapperTests;

public class CurrencyMapperTests : MapperTestsBase
{
    [Fact]
    public void MapEntityToEntity_ShouldBeAll_IgnoreHistory()
    {
        // Arrange
        var entity = new CurrencyEntity
        {
            Code = "EUR",
            Quantity = 5002.12M,
            PhotoUrl = "eur.png",
            AverageCourseRate = 1,
            BuyRate = null,
            SellRate = 1,
            Status = CurrencyStatus.Own,
            History = new List<CurrencyHistoryEntity>()
            {
                new()
                {
                    Id = 1,
                    Code = "EUR",
                    Quantity = 5002.12M,
                    AverageCourseRate = 1,
                    TimeStamp = new DateTime(2020, 1, 1, 15, 0, 0)
                }
            }
        };

        // Act
        var mappedEntity = Mapper.Map<CurrencyEntity>(entity);

        // Assert
        Assert.Equal(entity.Code, mappedEntity.Code);
        Assert.Equal(entity.Quantity, mappedEntity.Quantity);
        Assert.Equal(entity.PhotoUrl, mappedEntity.PhotoUrl);
        Assert.Equal(entity.AverageCourseRate, mappedEntity.AverageCourseRate);
        Assert.Equal(entity.BuyRate, mappedEntity.BuyRate);
        Assert.Equal(entity.SellRate, mappedEntity.SellRate);
        Assert.Equal(entity.Status, mappedEntity.Status);
        Assert.NotEqual(entity.History, mappedEntity.History);
        Assert.Empty(mappedEntity.History);
    }

    [Fact]
    public void MapEntityToListModel_ShouldBeEqual()
    {
        // Arrange
        var entity = CurrencySeeds.CurrencyToMap;

        // Act
        var mappedListModel = Mapper.Map<CurrencyListModel>(entity);

        // Assert
        Assert.Equal(entity.Code, mappedListModel.Code);
        Assert.Equal(entity.AverageCourseRate, mappedListModel.AverageCourseRate);
        Assert.Equal(entity.Quantity, mappedListModel.Quantity);
        Assert.Equal(entity.PhotoUrl, mappedListModel.PhotoUrl);
        Assert.Equal(entity.Status, mappedListModel.Status);
        Assert.Equal(8215.66M, mappedListModel.ExchangeRateValue);
    }

    [Fact]
    public void MapDetailModel_ToEntity_ShouldBe_Valid()
    {
        // Arrange
        var model = new CurrencyDetailModel
        {
            Code = "CODE",
            Quantity = 15000,
            PhotoUrl = "code.png",
            AverageCourseRate = 10045.4512413M,
            BuyRate = null,
            SellRate = 10001,
            Status = CurrencyStatus.Own
        };

        // Act
        var mappedEntity = Mapper.Map<CurrencyEntity>(model);

        // Assert
        Assert.Equal(model.Code, mappedEntity.Code);
        Assert.Equal(model.Quantity, mappedEntity.Quantity);
        Assert.Equal(model.PhotoUrl, mappedEntity.PhotoUrl);
        Assert.Equal(model.AverageCourseRate, mappedEntity.AverageCourseRate);
        Assert.Equal(model.BuyRate, mappedEntity.BuyRate);
        Assert.Equal(model.SellRate, mappedEntity.SellRate);
        Assert.Equal(model.Status, mappedEntity.Status);
        Assert.Empty(mappedEntity.History);
    }
}