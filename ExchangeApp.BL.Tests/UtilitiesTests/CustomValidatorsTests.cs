using ExchangeApp.BL.Utilities;

namespace ExchangeApp.BL.Tests.UtilitiesTests;

public class CustomValidatorsTests
{
    [Fact]
    public void ValidateName_ValidName_ShouldBe_True()
    {
        // Arrange
        const string name = "John";
        const string surname = "Travolta";

        // Act
        var res1 = CustomValidators.ValidateName(name);
        var res2 = CustomValidators.ValidateName(surname);

        // Assert
        Assert.True(res1);
        Assert.True(res2);
    }

    [Fact]
    public void ValidateName_ValidNameWithSpecialCharacters_ShouldBe_Valid()
    {
        // Arrange
        const string name = "Peťka";
        const string surname = "Mrkvičková";

        // Act
        var res1 = CustomValidators.ValidateName(name);
        var res2 = CustomValidators.ValidateName(surname);

        // Assert
        Assert.True(res1);
        Assert.True(res2);
    }

    [Fact]
    public void ValidateID_ValidIDWithOneSpace_ShouldBe_True()
    {
        // Arrange
        const string id = "880612 164";

        // Act
        var res = CustomValidators.ValidateIdentificationNumber(id);

        // Assert
        Assert.True(res);
    }

    [Fact]
    public void ValidateID_ValidIDWithMoreSpaces_ShouldBe_True()
    {
        // Arrange
        const string id = "880612  164";

        // Act
        var res = CustomValidators.ValidateIdentificationNumber(id);

        // Assert
        Assert.True(res);
    }

    [Fact]
    public void ValidateID_ValidIDWithOneTab_ShouldBe_True()
    {
        // Arrange
        const string id = "880612   164";

        // Act
        var res = CustomValidators.ValidateIdentificationNumber(id);

        // Assert
        Assert.True(res);
    }

    [Fact]
    public void ValidateID_ValidIDWithCombinationOfWhitespaces_ShouldBe_True()
    {
        // Arrange
        const string id = "880612         164";

        // Act
        var res = CustomValidators.ValidateIdentificationNumber(id);

        // Assert
        Assert.True(res);
    }

    [Fact]
    public void ValidateID_ValidIDWithSpacesAndBackslash_ShouldBe_True()
    {
        // Arrange
        const string id = "880612 / 164";

        // Act
        var res = CustomValidators.ValidateIdentificationNumber(id);

        // Assert
        Assert.True(res);
    }

    [Fact]
    public void ValidateID_ValidIDWithBackslash_ShouldBe_True()
    {
        // Arrange
        const string id = "850112/9999";

        // Act
        var res = CustomValidators.ValidateIdentificationNumber(id);

        // Assert
        Assert.True(res);
    }

    [Fact]
    public void ValidateID_ValidIDWithoutDelimiter_ShouldBe_True()
    {
        // Arrange
        const string id = "2307062222";

        // Act
        var res = CustomValidators.ValidateIdentificationNumber(id);

        // Assert
        Assert.True(res);
    }

    [Fact]
    public void ValidateID_InvalidID_MonthZero_ShouldBe_False()
    {
        // Arrange
        const string id = "000012/451";

        // Act
        var res = CustomValidators.ValidateIdentificationNumber(id);

        // Assert
        Assert.False(res);
    }

    [Fact]
    public void ValidateID_InvalidID_DaysZero_ShouldBe_False()
    {
        // Arrange
        const string id = "001200/1564";

        // Act
        var res = CustomValidators.ValidateIdentificationNumber(id);

        // Assert
        Assert.False(res);
    }

    [Fact]
    public void ValidateID_InvalidID_ControlNumberFiveDigits_ShouldBe_False()
    {
        // Arrange
        const string id = "007001/61764";

        // Act
        var res = CustomValidators.ValidateIdentificationNumber(id);

        // Assert
        Assert.False(res);
    }

    [Fact]
    public void ValidateID_InvalidID_ContainsChar_ShouldBe_False()
    {
        // Arrange
        const string id = "88124a/1564";

        // Act
        var res = CustomValidators.ValidateIdentificationNumber(id);

        // Assert
        Assert.False(res);
    }

    [Fact]
    public void ValidateID_InvalidID_MonthOverflow_ShouldBe_False()
    {
        // Arrange
        const string id = "881526/100";

        // Act
        var res = CustomValidators.ValidateIdentificationNumber(id);

        // Assert
        Assert.False(res);
    }

    [Fact]
    public void ValidateID_InvalidID_DaysOverflow_ShouldBe_False()
    {
        // Arrange
        const string id = "880636/2164";

        // Act
        var res = CustomValidators.ValidateIdentificationNumber(id);

        // Assert
        Assert.False(res);
    }
}