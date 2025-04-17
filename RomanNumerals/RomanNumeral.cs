using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumerals
{
    /// <summary>
    /// Represents a Roman numeral and provides methods for conversion between integers and Roman numerals.
    /// </summary>
    public class RomanNumeral
    {
        #region Constants

        //0
        public const string NULLA = "NULLA";

        //values - a read only dictionary where the numerals are the keys to values
        public static readonly IReadOnlyDictionary<string, int> VALUES = new ReadOnlyDictionary<string, int>(new Dictionary<string, int>
    {
        {"I",       1 },
        {"IV",      4 },
        {"V",       5 },
        {"IX",      9 },
        {"X",       10 },
        {"XIIX",    18 },
        {"IIXX",    18 },
        {"XL",      40 },
        {"L",       50 },
        {"XC",      90 },
        {"C",       100 },
        {"CD",      400 },
        {"D",       500 },
        {"CM",      900 },
        {"M",       1000 },

        //alternatives from Middle Ages and Renaissance
        {"O",       11 },
        {"F",       40 },
        {"P",       400 },
        {"G",       400 },
        {"Q",       500 }
    });

        //all the options that are used for parsing, in their order of value
        public static readonly string[] NUMERAL_OPTIONS =
        {
        "M", "CM", "D", "Q", "CD", "P", "G", "C", "XC", "L", "F", "XL", "IIXX", "XIIX", "O", "X", "IX", "V", "IV", "I"
    };

        //subtractive notation uses these numerals
        public static readonly string[] SUBTRACTIVE_NOTATION =
        {
        "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I"
    };

        //the additive notation uses these numerals
        public static readonly string[] ADDITIVE_NOTATION =
        {
        "M", "D", "C", "L", "X", "V", "I"
    };

        #endregion

        private readonly int _number;

        public int Number => _number;

        /// <summary>
        /// Constructor that create an integer number
        /// </summary>
        /// <param name="number"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public RomanNumeral(int number)
        {
            if (number < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Number should be positive.");
            }
            _number = number;
        }

        /// <summary>
        /// Here the actual conversion from an integer to a Roman Numeral take place
        /// </summary>
        /// <param name="notation"></param>
        /// <returns></returns>
        public override string ToString()
        {
            return ToRoman(RomanNumeralNotation.Subtractive);
        }

        /// <summary>
        /// Called by overridden RomanNumeral ToString for member Number
        /// </summary>
        /// <param name="notation"></param>
        /// <returns></returns>
        public string ToRoman(RomanNumeralNotation notation)
        {
            if (Number < 1 || Number > 3999)
            {
                throw new ArgumentOutOfRangeException(nameof(Number), "Number must be between 1 and 3999.");
            }

            if (Number == 0)
            {
                return NULLA;
            }

            // check notation for right set of characters
            string[] numerals = notation == RomanNumeralNotation.Additive ? ADDITIVE_NOTATION : SUBTRACTIVE_NOTATION;

            var resultRomanNumeral = new StringBuilder();
            var value = Number;
            var position = 0;

            while (value > 0)
            {
                var numeral = numerals[position];
                var numeralValue = VALUES[numeral];

                if (value >= numeralValue)
                {
                    value -= numeralValue;
                    resultRomanNumeral.Append(numeral);

                    if (numeral.Length > 1) // Skip invalid repeated subtractive combinations
                    {
                        position++;
                    }
                }
                else
                {
                    position++;
                }
            }

            return resultRomanNumeral.ToString();
        }

        /// <summary>
        /// Here the actual conversion to an Integer from a Roman Numeral take place
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>

        // Förbättrade valideringar och felhantering
        public static RomanNumeral ParseRoman(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException("Input cannot be null or empty.", nameof(str));
            }

            // Uppercase the string
            var strToRead = str.ToUpper();

            // Check for invalid characters
            if (!strToRead.All(c => NUMERAL_OPTIONS.Any(option => option.Contains(c))))
            {
                throw new ArgumentException($"Input contains invalid characters: {str}", nameof(str));
            }

            // Validate the Roman numeral string after range check
            ValidateRomanNumeral(str);

            // Check simple numbers directly in dictionary
            if (VALUES.ContainsKey(strToRead))
            {
                return new RomanNumeral(VALUES[strToRead]);
            }

            // Parse the string to calculate the value
            var resultNumber = 0;
            var numeralOptionPointer = 0;

            while (!string.IsNullOrEmpty(strToRead) && numeralOptionPointer < NUMERAL_OPTIONS.Length)
            {
                var numeral = NUMERAL_OPTIONS[numeralOptionPointer];

                if (!strToRead.StartsWith(numeral))
                {
                    numeralOptionPointer++;
                    continue;
                }

                resultNumber += VALUES[numeral];
                strToRead = strToRead.Substring(numeral.Length);

                if (numeral.Length > 1)
                {
                    numeralOptionPointer++;
                }
            }

            // If the string is not fully parsed, it's invalid
            if (!string.IsNullOrEmpty(strToRead))
            {
                throw new ArgumentException($"Invalid Roman numeral: {str}", nameof(str));
            }

            // Check for out-of-range values
            if (resultNumber > 3999)
            {
                throw new ArgumentOutOfRangeException(nameof(str), "Roman numeral value cannot exceed 3999.");
            }

            return new RomanNumeral(resultNumber);
        }

        /// <summary>
        /// Validates the structure and rules of a Roman numeral string.
        /// </summary>
        private static void ValidateRomanNumeral(string str)
        {
            // Rules for non-repeatable numerals
            var nonRepeatable = new[] { "V", "L", "D" };
            foreach (var numeral in nonRepeatable)
            {
                if (CountOccurrences(str, numeral) > 1)
                {
                    throw new ArgumentException($"Invalid repetition of numeral: {numeral}");
                }
            }

            // Rules for repeatable numerals (max 3 times in a row)
            var repeatable = new[] { "I", "X", "C", "M" };
            foreach (var numeral in repeatable)
            {
                if (str.Contains(new string(numeral[0], 4))) // e.g., "IIII", "XXXX", "CCCC", "MMMM"
                {
                    throw new ArgumentException($"Invalid repetition of numeral: {numeral}");
                }
            }

            // Validate invalid combinations like "IVIV", "IXIX", etc.
            var invalidSubtractiveCombinations = new[] { "IVIV", "IXIX", "XLXL", "XCXC", "CDCD", "CMCM" };
            foreach (var combination in invalidSubtractiveCombinations)
            {
                if (str.Contains(combination))
                {
                    throw new ArgumentException($"Invalid repetition of subtractive combination: {combination}");
                }
            }

            // Validate order of numerals
            if (!IsValidRomanOrder(str))
            {
                throw new ArgumentException($"Invalid order of Roman numerals: {str}");
            }

            // Validate value range (ensure no representation exceeds 3999)
            if (str.Contains("MMMM")) // Directly check for invalid "MMMM"
            {
                throw new ArgumentException($"Invalid value: Roman numeral exceeds 3999.");
            }
        }

        /// <summary>
        /// Checks if the order of characters in a Roman numeral string is valid.
        /// </summary>
        private static bool IsValidRomanOrder(string str)
        {
            // Define the valid order of Roman numerals
            var validOrder = "MDCLXVI";
            var lastIndex = int.MaxValue;

            foreach (var c in str)
            {
                var currentIndex = validOrder.IndexOf(c);
                if (currentIndex > lastIndex)
                {
                    // Allow valid additive combinations like "VI", "XI", etc.
                    if (lastIndex - currentIndex > 1)
                    {
                        return false; // Numerals are out of order
                    }
                }
                lastIndex = currentIndex;
            }

            return true;
        }

        /// <summary>
        /// Counts the occurrences of a specific substring in a string.
        /// </summary>
        public static int CountOccurrences(string str, string value)
        {
            int count = 0;
            int index = 0;
            while ((index = str.IndexOf(value, index)) != -1)
            {
                count++;
                index += value.Length;
            }
            return count;
        }

        /// CODE BELOW THIS LINE IS FOR ARITHMETRIC OPERATIONS WITH ROMAN NUMERALS AND IS VOLUNTARY TO PERFORM

        public static int operator +(int r1, RomanNumeral r2)
        {
            var r = new RomanNumeral(r1) + r2;
            return r.Number;
        }

        public static string operator +(string r1, RomanNumeral r2)
        {
            var r = RomanNumeral.ParseRoman(r1) + r2;
            return r.ToString();
        }

        public static RomanNumeral operator +(RomanNumeral r1, string r2)
        {
            return r1 + RomanNumeral.ParseRoman(r2);
        }

        public static RomanNumeral operator +(RomanNumeral r1, int r2)
        {
            var n = r1.Number + r2;
            return new RomanNumeral(n);
        }

        public static RomanNumeral operator +(RomanNumeral r1, RomanNumeral r2)
        {
            var n = r1.Number + r2.Number;
            return new RomanNumeral(n);
        }

        public static int operator -(int r1, RomanNumeral r2)
        {
            var r = new RomanNumeral(r1) - r2;
            return r.Number;
        }

        public static string operator -(string r1, RomanNumeral r2)
        {
            var r = RomanNumeral.ParseRoman(r1) - r2;
            return r.ToString();
        }

        public static RomanNumeral operator -(RomanNumeral r1, RomanNumeral r2)
        {
            var n = r1.Number - r2.Number;

            if (n < 0)
            {
                n = 0;
            }

            return new RomanNumeral(n);
        }

        public static implicit operator int(RomanNumeral r)
        {
            return (r?.Number).GetValueOrDefault();
        }

        public static implicit operator string(RomanNumeral r)
        {
            return r.ToString();
        }

        public static implicit operator RomanNumeral(int r)
        {
            return new RomanNumeral(r);
        }

        public static implicit operator RomanNumeral(string r)
        {
            return ParseRoman(r);
        }
    }
}
