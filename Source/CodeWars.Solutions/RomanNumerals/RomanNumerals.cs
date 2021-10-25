/*
 * https://www.codewars.com/kata/roman-numerals-encoder
 * https://www.codewars.com/kata/roman-numerals-decoder
 */
using System;
using System.Collections.Generic;

namespace RomanNumerals
{
    public class RomanNumerals
    {
        private int DigitsCount(long n)
        {
            return (int)Math.Floor(Math.Log10(n)) + 1;
        }

        private long[] NumberToTbl(long n, int digitscnt)
        {
            long[] res = new long[digitscnt];
            int j = digitscnt - 1;
            while (n > 0)
            {
                res[j] = n % 10;
                n = n / 10;
                j--;
            }
            return res;
        }

        //private static long[] _powers = new long[4] {1000, 100, 10, 1};

        private static readonly string[,] ToRoman = new string[4,10]
        {
            { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"},
            { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC"},
            { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM"},
            { "", "M", "MM", "MMM", "", "", "", "", "", ""}
        };

        public string Encode(long val)
        {
            if (val <= 0) return "";

            int digitscnt = DigitsCount(val);
            long[] tbl = NumberToTbl(val, digitscnt);

            string res = "";
            for (int i = 0; i < digitscnt; i++)
                res += ToRoman[digitscnt - i - 1, tbl[i]];
            return res;
        }

        private static readonly Dictionary<char, int> RomanVal = new Dictionary<char, int>()
        {
            {'M', 1000},
            {'D', 500},
            {'C', 100},
            {'L', 50},
            {'X', 10},
            {'V', 5},
            {'I', 1}
        };

        private static readonly  Dictionary<string, int> RomanValDbls = new Dictionary<string, int>()
        {
            {"IV", 4},
            {"IX", 9},
            {"XL", 40},
            {"XC", 90},
            {"CD", 400},
            {"CM", 900}
        };

        public long Decode(string val)
        {
            long res = 0;
            int i = 0;
            while (i < val.Length)
            {
                if (i == val.Length - 1)
                {
                    //last char
                    res += RomanVal[val[i]];
                    break;
                }
                int v1 = RomanVal[val[i]];
                int v2 = RomanVal[val[i + 1]];
                if (v1 < v2)
                {
                    res += RomanValDbls[val[i].ToString() + val[i + 1].ToString()];
                    i += 2;
                }
                else
                {
                    res += RomanVal[val[i]];
                    i += 1;
                }
            }
            return res;
        }
    }
}
