using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static long[] _powers = new long[4] {1000, 100, 10, 1};

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

        public long Decode(string val)
        {
            return 0;
        }
    }
}
