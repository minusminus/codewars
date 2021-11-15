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
using System.Collections.Generic;

namespace CodeWars.Solutions.BecomeImmortal
{
    public static class BecomeImmortal
    {
        private class Area
        {
            public long StartX, WidthX, StartY, WidthY;

            public Area(long startX, long widthX, long startY, long widthY)
            {
                StartX = startX;
                WidthX = widthX;
                StartY = startY;
                WidthY = widthY;
            }
        }

        public static long ElderAge(long m, long n, long l, long newp)
        {
            List<Area> areas = new List<Area>() { new Area(0, m, 0, n) };

            long sum = 0;
            int i = 0;
            while (i < areas.Count)
            {
                if (i % 10000 == 0) Console.WriteLine($"{i}, {areas.Count}");
                sum += CalculateRect(areas[i], l, areas);
                i++;
            }
            return sum % newp;
        }

        private static long CalculateRect(Area area, long l, List<Area> areas)
        {
            //if ((area.WidthX <= 0) || (area.WidthY <= 0)) return 0;
            if ((area.WidthX == 1) && (area.WidthY == 1)) return SubtractL(area.StartX ^ area.StartY, l);

            long largestPowerOf2 = GetLargestPowerOf2(Math.Max(area.WidthX, area.WidthY));

            if ((area.WidthX - largestPowerOf2) > 0)
                areas.Add(new Area(area.StartX + largestPowerOf2, area.WidthX - largestPowerOf2, area.StartY, Math.Min(largestPowerOf2, area.WidthY)));
            if ((area.WidthY - largestPowerOf2) > 0)
                areas.Add(new Area(area.StartX, area.WidthX, area.StartY + largestPowerOf2, area.WidthY - largestPowerOf2));
            return SumInRect(area.StartX ^ area.StartY, largestPowerOf2, Math.Min(largestPowerOf2, Math.Min(area.WidthX, area.WidthY)), l);

            //return SumInRect(area.StartX ^ area.StartY, largestPowerOf2, Math.Min(largestPowerOf2, Math.Min(area.WidthX, area.WidthY)), l)
            //    + CalculateRect(area.StartX + largestPowerOf2, area.WidthX - largestPowerOf2, area.StartY, Math.Min(largestPowerOf2, area.WidthY), l)
            //    + CalculateRect(area.StartX, area.WidthX, area.StartY + largestPowerOf2, area.WidthY - largestPowerOf2, l);

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
            (lastValue - firstValue + 1) * (firstValue + lastValue) / 2;
    }
}
