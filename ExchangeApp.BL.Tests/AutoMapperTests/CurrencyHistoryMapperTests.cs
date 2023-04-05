using ExchangeApp.BL.Models.Currency;
using ExchangeApp.Common.Tests.Seeds;
using ExchangeApp.DAL.Entities;

namespace ExchangeApp.BL.Tests.AutoMapperTests;

public class CurrencyHistoryMapperTests : MapperTestsBase
{
    [Fact]
    public void Map_Entity_To_Model_ShouldBe_Equal()
    {
        // Arrange
        var entity = CurrencyHistorySeeds.EntityToMap;

        // Act
        var model = Mapper.Map<CurrencyHistoryModel>(entity);

        // Assert
        var expectedExchangeRate = Math.Round(entity.Quantity / entity.AverageCourseRate, 2);
        Assert.Equal(entity.Id, model.Id);
        Assert.Equal(entity.Code, model.Code);
        Assert.Equal(entity.Quantity, model.Quantity);
        Assert.Equal(entity.AverageCourseRate, model.AverageCourseRate);
        Assert.Equal(entity.TimeStamp, model.TimeStamp);
        Assert.Equal(expectedExchangeRate, model.ExchangeRateValue);
    }

    [Fact]
    public void Map_Model_To_Entity_ShouldBe_Equal()
    {
        // Arrange
        var model = new CurrencyHistoryModel
        {
            Id = Guid.Parse("29D816EF-6DAF-4657-8FEB-779083ACBC97"),
            Code = "CHF",
            Quantity = 5300,
            AverageCourseRate = 1.1824456132M,
            TimeStamp = new DateTime(2022, 12, 30, 20, 05, 11)
        };

        // Act
        var mappedEntity = Mapper.Map<CurrencyHistoryEntity>(model);

        // Assert
        Assert.Equal(model.Id, mappedEntity.Id);
        Assert.Equal(model.Code, mappedEntity.Code);
        Assert.Equal(model.Quantity, mappedEntity.Quantity);
        Assert.Equal(model.AverageCourseRate, mappedEntity.AverageCourseRate);
        Assert.Equal(model.TimeStamp, mappedEntity.TimeStamp);
    }
}