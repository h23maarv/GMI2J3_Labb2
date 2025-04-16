using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumerals
{
    /// <summary>
    /// Helper class for testing and error control
    /// </summary>
    public static class RomanKnown
    {
        public const int LIMIT = 4999;
        public static Dictionary<string, int>? FromRomanTable;

        /// <summary>
        /// Create a dictionary lookup table that can ensure that roman numeral exist for every integer
        /// {"I", 1}, {"II", 2}, ...
        /// </summary>
        public static void BuildLookupTable()
        {
            if (FromRomanTable == null)
            {
                FromRomanTable = new Dictionary<string, int>();
                for (int i = 1; i <= LIMIT; i++)
                {
                    var rNum = new RomanNumeral(i);
                    FromRomanTable.Add(rNum, i);
                }
            }
        }

        // KnownValues - a read only dictionary where the integers are the keys to roman values
        public static readonly IReadOnlyDictionary<int, string> values = new ReadOnlyDictionary<int, string>(new Dictionary<int, string>
    {
        {1, "I"},
        {2, "II"},
        {3, "III"},
        {4, "IV"},
        {5, "V"},
        {6, "VI"},
        {7, "VII"},
        {8, "VIII"},
        {9, "IX"},
        {10, "X"},
        {50, "L"},
        {100, "C"},
        {500, "D"},
        {1000, "M"},
        {31, "XXXI"},
        {148, "CXLVIII"},
        {294, "CCXCIV"},
        {312, "CCCXII"},
        {421, "CDXXI"},
        {528, "DXXVIII"},
        {621, "DCXXI"},
        {782, "DCCLXXXII"},
        {870, "DCCCLXX"},
        {941, "CMXLI"},
        {1043, "MXLIII"},
        {1110, "MCX"},
        {1226, "MCCXXVI"},
        {1301, "MCCCI"},
        {1485, "MCDLXXXV"},
        {1509, "MDIX"},
        {1607, "MDCVII"},
        {1754, "MDCCLIV"},
        {1832, "MDCCCXXXII"},
        {1993, "MCMXCIII"},
        {2074, "MMLXXIV"},
        {2152, "MMCLII"},
        {2212, "MMCCXII"},
        {2343, "MMCCCXLIII"},
        {2499, "MMCDXCIX"},
        {2574, "MMDLXXIV"},
        {2646, "MMDCXLVI"},
        {2723, "MMDCCXXIII"},
        {2892, "MMDCCCXCII"},
        {2975, "MMCMLXXV"},
        {3051, "MMMLI"},
        {3185, "MMMCLXXXV"},
        {3250, "MMMCCL"},
        {3313, "MMMCCCXIII"},
        {3408, "MMMCDVIII"},
        {3501, "MMMDI"},
        {3610, "MMMDCX"},
        {3743, "MMMDCCXLIII"},
        {3844, "MMMDCCCXLIV"},
        {3888, "MMMDCCCLXXXVIII"},
        {3940, "MMMCMXL"},
        {3999, "MMMCMXCIX"},
        {4000, "MMMM"},
        {4500, "MMMMD"},
        {4888, "MMMMDCCCLXXXVIII"},
        {4999, "MMMMCMXCIX"}
    });
    }
}
