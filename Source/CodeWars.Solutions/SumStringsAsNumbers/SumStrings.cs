/*
 * https://www.codewars.com/kata/5324945e2ece5e1f32000370
 * Sum Strings as Numbers
 */
using System.Collections.Generic;
using System.Linq;

namespace CodeWars.Solutions.SumStringsAsNumbers
{
    /// <summary>
    /// A string representation of an integer will contain no characters besides the ten numerals "0" to "9",
    /// so there are only positive values.
    /// </summary>
    public static class SumStrings
    {
        public static string sumStrings(string a, string b)
        {
            (string longer, string shorter) = SortStrings(a, b);
            return string.Join("", SumWithCarry(longer, FillWithLeadingZeros(shorter, longer.Length - shorter.Length))).TrimStart('0');
        }

        private static (string longer, string shorter) SortStrings(string s1, string s2)
        {
            (string longer, string shorter) result = (s1, s2);
            if (s1.Length < s2.Length)
            {
                result.longer = s2;
                result.shorter = s1;
            }
            return result;
        }

        private static IEnumerable<char> FillWithLeadingZeros(string s, int zerosCount) =>
            new string('0', zerosCount) + s;

        private static IEnumerable<char> SumWithCarry(IEnumerable<char> s1, IEnumerable<char> s2)
        {
            bool carryFlag = false;

            char AddWithCarry(char c1, char c2)
            {
                int value = (int)char.GetNumericValue(c1) + (int)char.GetNumericValue(c2);
                if (carryFlag) value++;
                carryFlag = (value > 9);
                return (value % 10).ToString()[0];
            }

            char ProcessFinalCarry() => 
                carryFlag ? '1' : ' ';

            return s1.Reverse()
                .Zip(s2.Reverse(), AddWithCarry)
                .ToArray()  //require materialization before appending to ensure execution order
                .Append(ProcessFinalCarry())
                .Reverse()
                .Where(char.IsDigit);
        }
    }
}
