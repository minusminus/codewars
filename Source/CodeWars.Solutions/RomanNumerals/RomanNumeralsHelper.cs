/*
 * https://www.codewars.com/kata/51b66044bce5799a7f000003
 */
using System.Collections.Generic;
using System.Linq;

namespace CodeWars.Solutions.RomanNumerals
{
    public static class RomanNumeralsHelper
    {
        private static readonly string[,] MapDigitToRoman = new string[4, 10]
        {
            { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"},
            { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC"},
            { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM"},
            { "", "M", "MM", "MMM", "", "", "", "", "", ""}
        };

        private static readonly Dictionary<char, int> MapRomanToNumber = new Dictionary<char, int>()
        {
            {'M', 1000},
            {'D', 500},
            {'C', 100},
            {'L', 50},
            {'X', 10},
            {'V', 5},
            {'I', 1}
        };

        private static readonly Dictionary<string, int> MapRomanDoublesToNumber = new Dictionary<string, int>()
        {
            {"IV", 4},
            {"IX", 9},
            {"XL", 40},
            {"XC", 90},
            {"CD", 400},
            {"CM", 900}
        };


        public static string ToRoman(int n) => 
            ((n > 0) && (n < 4000)) ? ConvertToRoman(n) : "";

        public static int FromRoman(string romanNumeral)
        {
            try
            {
                return ConvertFromRoman(romanNumeral);
            }
            catch
            {
                return 0;
            }
        }

        private static string ConvertToRoman(int n) =>
            string.Join("",
                SplitNumberToDigtsBackwards(n)
                    .Select((digit, i) => MapDigitToRoman[i, digit])
                    .Reverse()
                );

        private static IEnumerable<int> SplitNumberToDigtsBackwards(int n)
        {
            while (n > 0)
            {
                yield return n % 10;
                n /= 10;
            }
        }

        private static int ConvertFromRoman(string romanNumeral) =>
            ConvertFromRomanAllExceptLastChar(romanNumeral, out int i) + ConvertFromRomanLastChar(romanNumeral, i);

        private static int ConvertFromRomanAllExceptLastChar(string romanNumeral, out int index)
        {
            index = 0;
            int res = 0;
            while (index < romanNumeral.Length - 1)
            {
                int valueCurrent = MapRomanToNumber[romanNumeral[index]];
                int valueNext = MapRomanToNumber[romanNumeral[index + 1]];
                if (valueCurrent < valueNext)
                {
                    res += MapRomanDoublesToNumber[romanNumeral[index].ToString() + romanNumeral[index + 1].ToString()];
                    index++;
                }
                else
                    res += valueCurrent;
                index++;
            }
            return res;
        }

        private static int ConvertFromRomanLastChar(string romanNumeral, int lastCharIndex) => 
            (lastCharIndex == romanNumeral.Length - 1) ? MapRomanToNumber[romanNumeral[lastCharIndex]] : 0;
    }
}
