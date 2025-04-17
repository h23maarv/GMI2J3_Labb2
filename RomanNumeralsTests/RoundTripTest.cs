using System;
using Xunit;
using RomanNumerals;

namespace RomanNumeralsTests
{
    /// <summary>
    /// Tests the round-trip conversion of integers to Roman numerals and back.
    /// </summary>
    public class RoundTripTest
    {
        /// <summary>
        /// Verifies that converting integers to Roman numerals and back retains the original value.
        /// </summary>
        [Fact]
        public void IntegerToRomanAndBack_RetainsOriginalValue()
        {
            // Arrange
            const int minValue = 1;
            const int maxValue = 3998;

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
