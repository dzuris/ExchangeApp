namespace ExchangeApp.BL.Tests.UtilitiesTests;

public class UtilitiesTests
{
    [Fact]
    public void StrToDecimal_DecimalWithComma_ShouldBe_ValidNumber()
    {
        // Arrange
        const string numberStr = "25,1645";

        // Act
        var result = Utilities.Utilities.StrToDecimal(numberStr);

        // Assert
        Assert.Equal(25.1645M, result);
    }

    [Fact]
    public void StrToDecimal_DecimalWithDot_ShouldBe_ValidNumber()
    {
        // Arrange
        const string numberStr = "1.5142";

        // Act
        var result = Utilities.Utilities.StrToDecimal(numberStr);

        // Assert
        Assert.Equal(1.5142M, result);
    }

    [Fact]
    public void StrToDecimal_NotValidNumber_ShouldBe_Null()
    {
        // Arrange
        const string notNumberStr = "45.a21";

        // Act
        var result = Utilities.Utilities.StrToDecimal(notNumberStr);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void StrToDecimal_IntegerNumber_ShouldBe_ValidNumber()
    {
        // Arrange
        const string integerNumberStr = "15";

        // Act
        var result = Utilities.Utilities.StrToDecimal(integerNumberStr);

        // Assert
        Assert.Equal(15M, result);
    }

    [Fact]
    public void GetDateFromID_ValidDate_ShouldBe_EqualDateTime()
    {
        // Arrange
        const string dateStr = "060812";
        var expectedDate = new DateTime(2006, 8, 12);

        // Act
        var result = Utilities.Utilities.GetDateTimeFromIdentificationNumber(dateStr);

        // Assert
        Assert.Equal(expectedDate, result);
    }

    [Fact]
    public void GetDateFromID_ValidDate_WomanMonth_ShouldBe_EqualDateTime()
    {
        // Arrange
        const string dateStr = "065812";
        var expectedDate = new DateTime(2006, 8, 12);

        // Act
        var result = Utilities.Utilities.GetDateTimeFromIdentificationNumber(dateStr);

        // Assert
        Assert.Equal(expectedDate, result);
    }

    [Fact]
    public void GetDateFromID_ValidDate_TooLongButValid_ShouldBe_EqualDateTime()
    {
        // Arrange
        const string dateStr = "88091521";
        var expectedDate = new DateTime(1988, 9, 15);

        // Act
        var result = Utilities.Utilities.GetDateTimeFromIdentificationNumber(dateStr);

        // Assert
        Assert.Equal(expectedDate, result);
    }

    [Fact]
    public void GetDateFromID_InvalidDate_ShouldBe_Null()
    {
        // Arrange
        const string dateStr = "020015";

        // Act
        var result = Utilities.Utilities.GetDateTimeFromIdentificationNumber(dateStr);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetDateFromID_TooShortString_ShouldBe_Null()
    {
        // Arrange
        const string dateStr = "02015";

        // Act
        var result = Utilities.Utilities.GetDateTimeFromIdentificationNumber(dateStr);

        // Assert
        Assert.Null(result);
    }
}