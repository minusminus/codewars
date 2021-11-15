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
        public static long ElderAge(long m, long n, long l, long t) =>
            CalculateRect(0, m, 0, n, l) % t;

        private static long CalculateRect(long startX, long widthX, long startY, long widthY, long l)
        {
            if ((widthX <= 0) || (widthY <= 0)) return 0;
            if ((widthX == 1) && (widthY == 1)) return SubtractL(startX ^ startY, l);

            long largestPowerOf2 = GetLargestPowerOf2(Math.Max(widthX, widthY));

            return SumInRect(startX ^ startY, largestPowerOf2, Math.Min(largestPowerOf2, Math.Min(widthX, widthY)), l)
                + CalculateRect(startX + largestPowerOf2, widthX - largestPowerOf2, startY, Math.Min(largestPowerOf2, widthY), l)
                + CalculateRect(startX, widthX, startY + largestPowerOf2, widthY - largestPowerOf2, l);

            //wersja w 3 strony z mniejszą ilością zagłębień wywołań
            //long newWidthOnRight = widthX - largestPowerOf2;
            //long newHeightOnBottom = widthY - largestPowerOf2;
            //return SumInRect(startX ^ startY, largestPowerOf2, Math.Min(largestPowerOf2, Math.Min(widthX, widthY)), l)
            //    + ((newWidthOnRight <= 0) ? 0 : CalculateRect(startX + largestPowerOf2, newWidthOnRight, startY, Math.Min(largestPowerOf2, widthY), l))
            //    + ((newHeightOnBottom <= 0) ? 0 : CalculateRect(startX, Math.Min(largestPowerOf2, widthX), startY + largestPowerOf2, newHeightOnBottom, l))
            //    + (((newWidthOnRight <= 0) || (newHeightOnBottom <= 0)) ? 0 : CalculateRect(startX + largestPowerOf2, newWidthOnRight, startY + largestPowerOf2, newHeightOnBottom, l));
        }

        private static long GetLargestPowerOf2(long x)
        {
            uint value = 1u << 63;
            while (((x & value) == 0) && (value != 0))
                value >>= 1;
            return value;
        }

        private static long SumInRect(long firstValue, long rowWidth, long numberOfRows, long l) =>
            numberOfRows * SumSequence(SubtractL(firstValue, l), SubtractL(firstValue + rowWidth - 1, l));

        private static long SubtractL(long value, long l) =>
            (value < l) ? 0 : value - l;

        private static long SumSequence(long firstValue, long lastValue) =>
            firstValue == lastValue
            ? firstValue
            : (lastValue - firstValue + 1) * (firstValue + lastValue) / 2;
    }
}
