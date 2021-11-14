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
        private static int level;

        private static void LevelDown()
        {
            level++;
            Console.WriteLine($"{level}");
        }

        private static void LevelUp()
        {
            Console.WriteLine($"-{level}");
            level--;
        }

        public static long ElderAge(long m, long n, long l, long t)
        {
            level = 0;
            return CalculateRect(0, m, 0, n, l) % t;
        }

        private static long CalculateRect(long startX, long widthX, long startY, long widthY, long l)
        {
            if ((widthX <= 0) || (widthY <= 0)) return 0;
            LevelDown();
            Console.WriteLine($"xy: ({startX}, {startY}), w: ({widthX}, {widthY})");
            if ((widthX == 1) && (widthY == 1)) 
            {
                LevelUp();
                return SubtractL(startX ^ startY, l); 
            }

            long largestPowerOf2 = GetLargestPowerOf2(Math.Max(widthX, widthY));
            Console.WriteLine($"powerof2: {largestPowerOf2}");

            long res = SumInRect(startX ^ startY, largestPowerOf2, Math.Min(largestPowerOf2, Math.Min(widthX, widthY)), l)
                + CalculateRect(startX + largestPowerOf2, widthX - largestPowerOf2, startY, Math.Min(largestPowerOf2, widthY), l)
                + CalculateRect(startX, widthX, startY + largestPowerOf2, widthY - largestPowerOf2, l);
            LevelUp();
            return res;
        }

        private static long GetLargestPowerOf2(long x)
        {
            uint value = 1u << 63;
            while (((x & value) == 0) && (value != 0))
                value >>= 1;
            return value;
        }

        private static long SumInRect(long firstValue, long rowWidth, long numberOfRows, long l)
        {
            //return numberOfRows * SumSequence(SubtractL(firstValue, l), SubtractL(firstValue + rowWidth - 1, l));
            long res = numberOfRows * SumSequence(SubtractL(firstValue, l), SubtractL(firstValue + rowWidth - 1, l));
            Console.WriteLine($"first: {firstValue}, width: {rowWidth}, numRows: {numberOfRows}, result: {res}");
            return res;
        }

        private static long SubtractL(long value, long l) =>
            value < (2 * l) ? 0 : value - l;

        private static long SumSequence(long firstValue, long lastValue) =>
            firstValue == lastValue
            ? firstValue
            : (lastValue - firstValue + 1) * (firstValue + lastValue) / 2;
    }
}
