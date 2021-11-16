/*
 * https://www.codewars.com/kata/59568be9cc15b57637000054
 * BECOME IMMORTAL
 * 
 * Wartości xor generują w macierzy "munchkin squares".
 * Koncepcja jest po części opisana tutaj: https://blog.valerauko.net/2018/02/11/munching-the-squares-of-immortality/
 * 
 * W maksymalnym prostokącie są wszystkie permutacje wartości, można obliczyć jego wartość jako sumę ciągu arytmetycznego x ilość wierszy (bok prostokąta).
 * Dalej rekurencyjnie w 2 prostokąty: po prawej (uzupełnienie do końca) i na dole (tej samej szerokości).
 */
using System;

namespace CodeWars.Solutions.BecomeImmortal
{
    public static class BecomeImmortal
    {
        public static long ElderAge(long m, long n, long l, long newp) =>
            CalculateRect(0, m, 0, n, l, newp);// % newp;

        private static long CalculateRect(long startX, long widthX, long startY, long widthY, long l, long newp)
        {
            if ((widthX <= 0) || (widthY <= 0)) return 0;
            if ((widthX == 1) && (widthY == 1)) return SubtractL(startX ^ startY, l) % newp;

            long largestPowerOf2 = GetLargestPowerOf2(Math.Max(widthX, widthY));

            long result = SumInRect(startX ^ startY, largestPowerOf2, Math.Min(largestPowerOf2, Math.Min(widthX, widthY)), l, newp);
            result = (result + CalculateRect(startX + largestPowerOf2, widthX - largestPowerOf2, startY, Math.Min(largestPowerOf2, widthY), l, newp)) % newp;
            result = (result + CalculateRect(startX, widthX, startY + largestPowerOf2, widthY - largestPowerOf2, l, newp)) % newp;
            return result;
        }

        private static long GetLargestPowerOf2(long x)
        {
            long value = 1L << 62;
            while (((x & value) == 0) && (value != 0))
                value >>= 1;
            return value;
        }

        private static long SumInRect(long firstValue, long rowWidth, long numberOfRows, long l, long newp) =>
            (numberOfRows * SumSequence(SubtractL(firstValue, l), SubtractL(firstValue + rowWidth - 1, l), newp)) % newp;

        private static long SubtractL(long value, long l) =>
            (value < l) ? 0 : value - l;

        private static long SumSequence(long firstValue, long lastValue, long newp) =>
            (((lastValue - firstValue + 1) % newp) * ((firstValue + lastValue) % newp) / 2) % newp;
    }
}
