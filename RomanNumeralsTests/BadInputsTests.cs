using RomanNumerals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumeralsTests
{
    /// <summary>
    /// Tests for invalid inputs to the Roman numeral conversion methods.
    /// </summary>
    public class BadInputTests
    {
        /// <summary>
        /// Verifies that parsing an empty string throws an exception.
        /// </summary>
        [Fact]
        public void ParseRoman_WithEmptyString_ThrowsArgumentException()
        {
            // Arrange
            var invalidInput = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => RomanNumeral.ParseRoman(invalidInput));
        }

        /// <summary>
        /// Verifies that parsing invalid Roman numeral strings throws an exception.
        /// </summary>
        [Fact]
        public void ParseRoman_WithInvalidRomanNumeral_ThrowsArgumentException()
        {
            // Arrange
            var invalidInputs = new[] { "IIII", "VV", "MMMMM", "ABC", "123", "IVIV" };

            // Act & Assert
            foreach (var input in invalidInputs)
            {
                Assert.Throws<ArgumentException>(() => RomanNumeral.ParseRoman(input));
            }
        }

        /// <summary>
        /// Verifies that parsing strings with unsupported characters throws an exception.
        /// </summary>
        [Fact]
        public void ParseRoman_WithUnsupportedCharacters_ThrowsArgumentException()
        {
            // Arrange
            var invalidInputs = new[] { "!", "@", "#", "$", "%", "^", "&", "*" };

            // Act & Assert
            foreach (var input in invalidInputs)
            {
                Assert.Throws<ArgumentException>(() => RomanNumeral.ParseRoman(input));
            }
        }

        /// <summary>
        /// Verifies that parsing unsupported data types throws an exception.
        /// </summary>
        [Fact]
        public void ParseRoman_WithUnsupportedDataTypes_ThrowsException()
        {
            // Arrange
            var invalidInputs = new object[]
            {
        123,                // Integer
        123.45,             // Double
        new List<string> { "I", "V" }, // List of strings
        new Dictionary<string, int> { { "I", 1 } }, // Dictionary
        new object(),       // Generic object
        true,               // Boolean
        null                // Null
            };

            // Act & Assert
            foreach (var input in invalidInputs)
            {
                Assert.ThrowsAny<Exception>(() => RomanNumeral.ParseRoman(input?.ToString()!));
            }
        }

        /// <summary>
        /// Verifies that parsing non-repeatable numerals multiple times throws an exception.
        /// </summary>
        [Fact]
        public void ParseRoman_WithNonRepeatableNumerals_ThrowsArgumentException()
        {
            // Arrange
            var invalidInputs = new[] { "VV", "LL", "DD", "VVV", "LLL", "DDD" };

            // Act & Assert
            foreach (var input in invalidInputs)
            {
                Assert.Throws<ArgumentException>(() => RomanNumeral.ParseRoman(input));
            }
        }

        /// <summary>
        /// Verifies that parsing numerals with too many repetitions throws an exception.
        /// </summary>
        [Fact]
        public void ParseRoman_WithTooManyRepetitions_ThrowsArgumentException()
        {
            // Arrange
            var invalidInputs = new[] { "IIII", "XXXX", "CCCC", "MMMM" };

            // Act & Assert
            foreach (var input in invalidInputs)
            {
                Assert.Throws<ArgumentException>(() => RomanNumeral.ParseRoman(input));
            }
        }

        /// <summary>
        /// Verifies that parsing repeated subtractive notations throws an exception.
        /// </summary>
        [Fact]
        public void ParseRoman_WithRepeatedSubtractiveNotation_ThrowsArgumentException()
        {
            // Arrange
            var invalidInputs = new[] { "IVIV", "IXIX", "XLXL", "XCXC", "CDCD", "CMCM" };

            // Act & Assert
            foreach (var input in invalidInputs)
            {
                Assert.Throws<ArgumentException>(() => RomanNumeral.ParseRoman(input));
            }
        }

        /// <summary>
        /// Verifies that parsing Roman numerals with invalid order throws an exception.
        /// </summary>
        [Fact]
        public void ParseRoman_WithInvalidOrder_ThrowsArgumentException()
        {
            // Arrange
            var invalidInputs = new[] { "IM", "VX", "LC", "DM", "IL", "IC", "XM" };

            // Act & Assert
            foreach (var input in invalidInputs)
            {
                Assert.Throws<ArgumentException>(() => RomanNumeral.ParseRoman(input));
            }
        }

        /// <summary>
        /// Verifies that parsing valid subtractive notations returns the correct values.
        /// </summary>
        [Fact]
        public void ParseRoman_WithValidSubtractiveNotation_ReturnsCorrectValue()
        {
            // Arrange
            var validInputs = new Dictionary<string, int>
    {
        { "IV", 4 },
        { "IX", 9 },
        { "XL", 40 },
        { "XC", 90 },
        { "CD", 400 },
        { "CM", 900 }
    };

            // Act & Assert
            foreach (var kvp in validInputs)
            {
                var result = RomanNumeral.ParseRoman(kvp.Key);
                Assert.Equal(kvp.Value, result.Number);
            }
        }

        /// <summary>
        /// Verifies that parsing strings with invalid characters throws an exception.
        /// </summary>
        [Fact]
        public void ParseRoman_WithInvalidCharacters_ThrowsArgumentException()
        {
            // Arrange
            var invalidInputs = new[] { "A", "B", "123", "!", "@", "IVX", "MCMZ" };

            // Act & Assert
            foreach (var input in invalidInputs)
            {
                Assert.Throws<ArgumentException>(() => RomanNumeral.ParseRoman(input));
            }
        }

        /// <summary>
        /// Verifies that parsing empty or null strings throws an exception.
        /// </summary>
        [Fact]
        public void ParseRoman_WithEmptyOrNullString_ThrowsArgumentException()
        {
            // Arrange
            var invalidInputs = new[] { "", null };

            // Act & Assert
            foreach (var input in invalidInputs)
            {
                Assert.Throws<ArgumentException>(() => RomanNumeral.ParseRoman(input!));
            }
        }
    }
}
