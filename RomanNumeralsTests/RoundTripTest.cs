using System;
using Xunit;
using RomanNumerals;

namespace RomanNumeralsTests
{
    public class RoundTripTest
    {
        [Fact]
        public void IntegerToRomanAndBack_RetainsOriginalValue()
        {
            // Arrange
            const int minValue = 1;
            const int maxValue = 3999;

            // Act & Assert
            for (int i = minValue; i <= maxValue; i++)
            {
                // Convert integer to Roman numeral
                var romanNumeral = new RomanNumeral(i).ToString();

                // Convert Roman numeral back to integer
                var parsedNumber = RomanNumeral.ParseRoman(romanNumeral).Number;

                // Assert that the original integer is retained
                Assert.Equal(i, parsedNumber);
            }
        }
    }
}
