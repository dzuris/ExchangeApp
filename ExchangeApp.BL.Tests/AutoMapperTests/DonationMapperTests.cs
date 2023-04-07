using ExchangeApp.BL.Models.Currency;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.Common.Enums;
using ExchangeApp.Common.Tests;
using ExchangeApp.Common.Tests.Seeds;
using ExchangeApp.DAL.Entities.Operations;

namespace ExchangeApp.BL.Tests.AutoMapperTests;

public class DonationMapperTests : MapperTestsBase
{
    [Fact]
    public void Map_Model_To_Entity_ShouldBe_Equal()
    {
        // Arrange
        var currency = CurrencySeeds.CurrencyToMap;
        var model = new DonationDetailModel
        {
            Id = 5,
            Created = new DateTime(2022, 5, 1, 16, 18, 56),
            CourseRate = 5.254M,
            AverageCourseRate = 5.654M,
            Quantity = 50000,
            CurrencyQuantityBefore = 120000,
            Type = DonationType.Deposit,
            Note = string.Empty,
            IsCanceled = true,
            CurrencyCode = currency.Code,
            Currency = new CurrencyListModel
            {
                Code = currency.Code,
                AverageCourseRate = currency.AverageCourseRate,
                Quantity = currency.Quantity,
                PhotoUrl = currency.PhotoUrl,
                Status = currency.Status
            }
        };

        // Act
        var mappedEntity = Mapper.Map<DonationEntity>(model);

        // Assert
        Assert.Equal(model.Id, mappedEntity.Id);
        Assert.Equal(model.Created, mappedEntity.Created);
        Assert.Equal(model.CourseRate, mappedEntity.CourseRate);
        Assert.Equal(model.AverageCourseRate, mappedEntity.AverageCourseRate);
        Assert.Equal(model.Quantity, mappedEntity.Quantity);
        Assert.Equal(model.CurrencyQuantityBefore, mappedEntity.CurrencyQuantityBefore);
        Assert.Equal(model.Type, mappedEntity.Type);
        Assert.Equal(model.Note, mappedEntity.Note);
        Assert.Equal(model.IsCanceled, mappedEntity.IsCanceled);
        Assert.Equal(model.CurrencyCode, mappedEntity.CurrencyCode);
        Assert.Null(mappedEntity.Currency);
    }

    [Fact]
    public void Map_Entity_To_Model_ShouldBe_Equal()
    {
        // Arrange
        var entity = DonationSeeds.DonationToMap;

        // Act
        var mappedModel = Mapper.Map<DonationDetailModel>(entity);

        // Assert
        var currency = CurrencySeeds.CurrencyToMap;
        var currencyListModel = new CurrencyListModel
        {
            Code = currency.Code,
            AverageCourseRate = currency.AverageCourseRate,
            Quantity = currency.Quantity,
            PhotoUrl = currency.PhotoUrl,
            Status = currency.Status
        };
        Assert.Equal(entity.Id, mappedModel.Id);
        Assert.Equal(entity.Created, mappedModel.Created);
        Assert.Equal(entity.CourseRate, mappedModel.CourseRate);
        Assert.Equal(entity.AverageCourseRate, mappedModel.AverageCourseRate);
        Assert.Equal(entity.Quantity, mappedModel.Quantity);
        Assert.Equal(entity.CurrencyQuantityBefore, mappedModel.CurrencyQuantityBefore);
        Assert.Equal(entity.Type, mappedModel.Type);
        Assert.Equal(entity.Note, mappedModel.Note);
        Assert.Equal(entity.IsCanceled, mappedModel.IsCanceled);
        Assert.Equal(entity.CurrencyCode, mappedModel.CurrencyCode);
        DeepAssert.Equal(currencyListModel, mappedModel.Currency);
    }
}