using RomanNumerals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumeralsTests
{
    public class BadInputTests
    {
        [Fact]
        public void ParseRoman_WithEmptyString_ThrowsArgumentException()
        {
            // Arrange
            var invalidInput = "";

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => RomanNumeral.ParseRoman(invalidInput));
        }

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

        [Fact]
        public void ParseRoman_WithOutOfRangeRomanNumeral_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var invalidInputs = new[] { "MMMM", "MMMMCMXCIX" }; // Values exceeding 3999

            // Act & Assert
            foreach (var input in invalidInputs)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => RomanNumeral.ParseRoman(input));
            }
        }

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

    }
}
