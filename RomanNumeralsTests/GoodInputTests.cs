using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using RomanNumerals;

namespace RomanNumeralsTests
{
    /// <summary>
    /// Tests for valid inputs to the Roman numeral conversion methods.
    /// </summary>
    public class GoodInputTests
    {
        /// <summary>
        /// Verifies that converting integers to Roman numerals and back retains the original value.
        /// </summary>
        [Fact]
        public void ToRoman_WithValidInputs_ReturnsCorrectRomanNumerals()
        {
            // Arrange
            var knownValues = RomanNumeral.VALUES
                .Where(kvp => kvp.Value <= 3999)
                .GroupBy(kvp => kvp.Value)
                .ToDictionary(g => g.Key, g => g.First().Key);

            // Act & Assert
            foreach (var kvp in knownValues)
            {
                var romanNumeral = new RomanNumeral(kvp.Key);
                var parsed = RomanNumeral.ParseRoman(romanNumeral.ToString()).Number;

                // Confirms that the number remains the same when converting back and forth.
                Assert.Equal(kvp.Key, parsed);
            }
        }

        /// <summary>
        /// Verifies that parsing valid Roman numerals returns the correct integer values.
        /// </summary>
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

        /// <summary>
        /// Tests the conversion of the minimum valid integer (1) to a Roman numeral.
        /// </summary>
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

        /// <summary>
        /// Tests the conversion of the maximum valid integer (3999) to a Roman numeral.
        /// </summary>
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

        /// <summary>
        /// Tests parsing the minimum valid Roman numeral ("I") to an integer.
        /// </summary>
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

        /// <summary>
        /// Tests parsing the maximum valid Roman numeral ("MMMCMXCIX") to an integer.
        /// </summary>
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
