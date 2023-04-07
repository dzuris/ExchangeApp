using ExchangeApp.BL.Models.TotalBalance;
using ExchangeApp.Common.Enums;
using ExchangeApp.Common.Tests;
using ExchangeApp.Common.Tests.Seeds;
using ExchangeApp.DAL.Entities;

namespace ExchangeApp.BL.Tests.AutoMapperTests;

public class TotalBalanceMapperTests : MapperTestsBase
{
    [Fact]
    public void Map_Model_To_Entity_ShouldBe_Equal()
    {
        // Arrange
        var model = new TotalBalanceModel
        {
            Id = 35,
            Type = TotalBalanceType.Monthly,
            Created = new DateTime(2000, 10, 9, 15, 33, 15),
            LastTotalBalance = DateTime.MinValue
        };

        // Act
        var mappedEntity = Mapper.Map<TotalBalanceEntity>(model);

        // Assert
        Assert.Equal(model.Id, mappedEntity.Id);
        Assert.Equal(model.Type, mappedEntity.Type);
        Assert.Equal(model.Created, mappedEntity.Created);
        Assert.Equal(model.LastTotalBalance, mappedEntity.LastTotalBalance);
    }

    [Fact]
    public void Map_Entity_To_Entity_ShouldBe_Equal()
    {
        // Arrange
        var entity = TotalBalanceSeeds.TotalBalanceToMap;

        // Act
        var mappedEntity = Mapper.Map<TotalBalanceEntity>(entity);

        // Assert
        DeepAssert.Equal(entity, mappedEntity);
    }
}