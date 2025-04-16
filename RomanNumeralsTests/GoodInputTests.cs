using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using RomanNumerals;

namespace RomanNumeralsTests
{

    public class GoodInputTests
    {
        [Fact]
        public void ToRoman_WithValidInputs_ReturnsCorrectRomanNumerals()
        {
            // Arrange
            var knownValues = RomanNumeral.VALUES
                .Where(kvp => kvp.Value <= 3999) // Filter valid inputs (up to 3999)
                .ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

            // Act & Assert
            foreach (var kvp in knownValues)
            {
                var romanNumeral = new RomanNumeral(kvp.Key).ToString();
                Assert.Equal(kvp.Value, romanNumeral);
            }
        }

        [Fact]
        public void ParseRoman_WithValidRomanNumerals_ReturnsCorrectIntegers()
        {
            // Arrange
            var knownValues = RomanNumeral.VALUES
                .Where(kvp => kvp.Value <= 3999) // Filter valid inputs (up to 3999)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            // Act & Assert
            foreach (var kvp in knownValues)
            {
                var parsedNumber = RomanNumeral.ParseRoman(kvp.Key).Number;
                Assert.Equal(kvp.Value, parsedNumber);
            }
        }


        [Fact]
        public void ToRoman_WithMinimumValidInput_ReturnsCorrectRomanNumeral()
        {
            // Arrange
            var romanNumeral = new RomanNumeral(1);

            // Act
            var result = romanNumeral.ToString();

            // Assert
            Assert.Equal("I", result);
        }

        [Fact]
        public void ToRoman_WithMaximumValidInput_ReturnsCorrectRomanNumeral()
        {
            // Arrange
            var romanNumeral = new RomanNumeral(3999);

            // Act
            var result = romanNumeral.ToString();

            // Assert
            Assert.Equal("MMMCMXCIX", result);
        }

        [Fact]
        public void ParseRoman_WithMinimumValidInput_ReturnsCorrectInteger()
        {
            // Arrange
            var romanString = "I"; // Minimum valid Roman numeral

            // Act
            var result = RomanNumeral.ParseRoman(romanString).Number;

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void ParseRoman_WithMaximumValidInput_ReturnsCorrectInteger()
        {
            // Arrange
            var romanString = "MMMCMXCIX"; // Maximum valid Roman numeral

            // Act
            var result = RomanNumeral.ParseRoman(romanString).Number;

            // Assert
            Assert.Equal(3999, result);
        }
    }
}
