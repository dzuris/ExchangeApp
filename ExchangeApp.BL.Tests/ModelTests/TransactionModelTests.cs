using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;
using ExchangeApp.Common.Tests.Seeds;

namespace ExchangeApp.BL.Tests.ModelTests;

public class TransactionModelTests
{
    [Fact]
    public void AmountTest_ShouldBe_ExpectedValue()
    {
        // Arrange
        var model = TransactionSeeds.TransactionDetailModel;

        // Assert
        Assert.Equal(332.64M, model.AmountDomesticCurrency);
    }

    [Fact]
    public void RoundingTest_ShouldBe_Zero()
    {
        // Arrange
        var model = TransactionSeeds.TransactionDetailModel;

        // Assert
        Assert.Equal(0.01M, model.Rounding);
    }

    [Fact]
    public void TotalAmount_ShouldBe_ExpectedValue()
    {
        // Arrange
        var model = TransactionSeeds.TransactionDetailModel;

        // Assert
        Assert.Equal(332.65M, model.TotalAmountDomesticCurrency);
    }

    [Fact]
    public void TransactionListModel_RoundingTest_ShouldBe_ExpectedValue()
    {
        // Arrange
        var model = new TransactionListModel
        {
            Id = 999,
            Created = DateTime.MinValue,
            Quantity = 5620,
            CurrencyCode = CurrencySeeds.PlnCurrency.Code,
            CourseRate = 4.62M,
            IsCanceled = false,
            TransactionType = TransactionType.Sell
        };

        // Assert
        Assert.Equal(0, model.Rounding);
    }
}