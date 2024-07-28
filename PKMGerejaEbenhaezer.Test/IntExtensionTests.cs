using FluentAssertions;
using PKMGerejaEbenhaezer.DataAccess;

namespace PKMGerejaEbenhaezer.UnitTest;

public class IntExtensionTests
{
    [Theory]
    [InlineData(1, "I")]
    [InlineData(2, "II")]
    [InlineData(3, "III")]
    [InlineData(4, "IV")]
    [InlineData(5, "V")]
    [InlineData(6, "VI")]
    [InlineData(7, "VII")]
    [InlineData(8, "VIII")]
    [InlineData(9, "IX")]
    [InlineData(10, "X")]
    [InlineData(11, "XI")]
    [InlineData(12, "XII")]
    [InlineData(13, "XIII")]
    [InlineData(14, "XIV")]
    [InlineData(15, "XV")]
    [InlineData(16, "XVI")]
    [InlineData(17, "XVII")]
    [InlineData(18, "XVIII")]
    [InlineData(19, "XIX")]
    [InlineData(20, "XX")]
    [InlineData(21, "XXI")]
    [InlineData(22, "XXII")]
    [InlineData(23, "XXIII")]
    [InlineData(24, "XXIV")]
    [InlineData(25, "XXV")]
    [InlineData(26, "XXVI")]
    [InlineData(27, "XXVII")]
    [InlineData(28, "XXVIII")]
    [InlineData(29, "XXIX")]
    [InlineData(30, "XXX")]
    [InlineData(31, "XXXI")]
    [InlineData(32, "XXXII")]
    [InlineData(33, "XXXIII")]
    public void ToRomanNumeral_Should_ReturnEqualToExpected(int value, string expected)
    {
        //Act
        var result = value.ToRomanNumeral();

        //Assert
        result.Should().Be(expected);
    }
}
