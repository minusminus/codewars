/*
 * https://www.codewars.com/kata/59568be9cc15b57637000054
 * BECOME IMMORTAL
 * 
 * Wartości xor generują w macierzy "munchkin squares".
 * Koncepcja jest po części opisana tutaj: https://blog.valerauko.net/2018/02/11/munching-the-squares-of-immortality/
 * 
 * W maksymalnym kwadracie są wszystkie permutacje wartości, można obliczyć jego wartość jako sumę ciągu arytmetycznego x ilość wierszy (bok kwadratu).
 * Dalej rekurencyjnie w 3 prostokąty: po prawej, na dole, po skosie na dole (prawo-dół).
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
            if ((widthX == 0) || (widthY == 0)) return 0;

            long largestPowerOf2 = GetLargestPowerOf2(Math.Min(widthX, widthY));

            return SumInSquare(startX ^ startY, largestPowerOf2, l)
                + CalculateRect(startX + largestPowerOf2, widthX - largestPowerOf2, startY, largestPowerOf2, l)
                + CalculateRect(startX, largestPowerOf2, startY + largestPowerOf2, widthY - largestPowerOf2, l)
                + CalculateRect(startX + largestPowerOf2, widthX - largestPowerOf2, startY + largestPowerOf2, widthY - largestPowerOf2, l);
        }

        private static long GetLargestPowerOf2(long x)
        {
            uint value = 1u << 63;
            while (((x & value) == 0) && (value != 0))
                value >>= 1;
            return value;
        }

        private static long SumInSquare(long firstValue, long width, long l) => 
            width * SumSequence(SubtractL(firstValue, l), SubtractL(firstValue + width - 1, l));

        private static long SubtractL(long value, long l) =>
            value < (2 * l) ? 0 : value - l;

        private static long SumSequence(long firstValue, long lastValue) =>
            firstValue == lastValue
            ? firstValue
            : (lastValue - firstValue + 1) * (firstValue + lastValue) / 2;
    }
}
