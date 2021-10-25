/*
 * https://www.codewars.com/kata/5626b561280a42ecc50000d1
 */

using System;
using System.Collections.Generic;

namespace TakeANumberAndSumItsDigitsRaisedToconsecutivePowers
{
    public class SumDigPower
    {
        /*        private static double[,] _powers = new double[10,21]
                {
                    {Math.Pow(0, 1), Math.Pow(0, 2), Math.Pow(0, 3), Math.Pow(0, 4), Math.Pow(0, 5), Math.Pow(0, 6), Math.Pow(0, 7), Math.Pow(0, 8), Math.Pow(0, 9), Math.Pow(0, 10), Math.Pow(0, 11), Math.Pow(0, 12), Math.Pow(0, 13), Math.Pow(0, 14), Math.Pow(0, 15), Math.Pow(0, 16), Math.Pow(0, 17), Math.Pow(0, 18), Math.Pow(0, 19), Math.Pow(0, 20), Math.Pow(0, 21)},
                    {Math.Pow(1, 1), Math.Pow(1, 2), Math.Pow(1, 3), Math.Pow(1, 4), Math.Pow(1, 5), Math.Pow(1, 6), Math.Pow(1, 7), Math.Pow(1, 8), Math.Pow(1, 9), Math.Pow(1, 10), Math.Pow(1, 11), Math.Pow(1, 12), Math.Pow(1, 13), Math.Pow(1, 14), Math.Pow(1, 15), Math.Pow(1, 16), Math.Pow(1, 17), Math.Pow(1, 18), Math.Pow(1, 19), Math.Pow(1, 20), Math.Pow(1, 21)},
                    {Math.Pow(2, 1), Math.Pow(2, 2), Math.Pow(2, 3), Math.Pow(2, 4), Math.Pow(2, 5), Math.Pow(2, 6), Math.Pow(2, 7), Math.Pow(2, 8), Math.Pow(2, 9), Math.Pow(2, 10), Math.Pow(2, 11), Math.Pow(2, 12), Math.Pow(2, 13), Math.Pow(2, 14), Math.Pow(2, 15), Math.Pow(2, 16), Math.Pow(2, 17), Math.Pow(2, 18), Math.Pow(2, 19), Math.Pow(2, 20), Math.Pow(2, 21)},
                    {Math.Pow(3, 1), Math.Pow(3, 2), Math.Pow(3, 3), Math.Pow(3, 4), Math.Pow(3, 5), Math.Pow(3, 6), Math.Pow(3, 7), Math.Pow(3, 8), Math.Pow(3, 9), Math.Pow(3, 10), Math.Pow(3, 11), Math.Pow(3, 12), Math.Pow(3, 13), Math.Pow(3, 14), Math.Pow(3, 15), Math.Pow(3, 16), Math.Pow(3, 17), Math.Pow(3, 18), Math.Pow(3, 19), Math.Pow(3, 20), Math.Pow(3, 21)},
                    {Math.Pow(4, 1), Math.Pow(4, 2), Math.Pow(4, 3), Math.Pow(4, 4), Math.Pow(4, 5), Math.Pow(4, 6), Math.Pow(4, 7), Math.Pow(4, 8), Math.Pow(4, 9), Math.Pow(4, 10), Math.Pow(4, 11), Math.Pow(4, 12), Math.Pow(4, 13), Math.Pow(4, 14), Math.Pow(4, 15), Math.Pow(4, 16), Math.Pow(4, 17), Math.Pow(4, 18), Math.Pow(4, 19), Math.Pow(4, 20), Math.Pow(4, 21)},
                    {Math.Pow(5, 1), Math.Pow(5, 2), Math.Pow(5, 3), Math.Pow(5, 4), Math.Pow(5, 5), Math.Pow(5, 6), Math.Pow(5, 7), Math.Pow(5, 8), Math.Pow(5, 9), Math.Pow(5, 10), Math.Pow(5, 11), Math.Pow(5, 12), Math.Pow(5, 13), Math.Pow(5, 14), Math.Pow(5, 15), Math.Pow(5, 16), Math.Pow(5, 17), Math.Pow(5, 18), Math.Pow(5, 19), Math.Pow(5, 20), Math.Pow(5, 21)},
                    {Math.Pow(6, 1), Math.Pow(6, 2), Math.Pow(6, 3), Math.Pow(6, 4), Math.Pow(6, 5), Math.Pow(6, 6), Math.Pow(6, 7), Math.Pow(6, 8), Math.Pow(6, 9), Math.Pow(6, 10), Math.Pow(6, 11), Math.Pow(6, 12), Math.Pow(6, 13), Math.Pow(6, 14), Math.Pow(6, 15), Math.Pow(6, 16), Math.Pow(6, 17), Math.Pow(6, 18), Math.Pow(6, 19), Math.Pow(6, 20), Math.Pow(6, 21)},
                    {Math.Pow(7, 1), Math.Pow(7, 2), Math.Pow(7, 3), Math.Pow(7, 4), Math.Pow(7, 5), Math.Pow(7, 6), Math.Pow(7, 7), Math.Pow(7, 8), Math.Pow(7, 9), Math.Pow(7, 10), Math.Pow(7, 11), Math.Pow(7, 12), Math.Pow(7, 13), Math.Pow(7, 14), Math.Pow(7, 15), Math.Pow(7, 16), Math.Pow(7, 17), Math.Pow(7, 18), Math.Pow(7, 19), Math.Pow(7, 20), Math.Pow(7, 21)},
                    {Math.Pow(8, 1), Math.Pow(8, 2), Math.Pow(8, 3), Math.Pow(8, 4), Math.Pow(8, 5), Math.Pow(8, 6), Math.Pow(8, 7), Math.Pow(8, 8), Math.Pow(8, 9), Math.Pow(8, 10), Math.Pow(8, 11), Math.Pow(8, 12), Math.Pow(8, 13), Math.Pow(8, 14), Math.Pow(8, 15), Math.Pow(8, 16), Math.Pow(8, 17), Math.Pow(8, 18), Math.Pow(8, 19), Math.Pow(8, 20), Math.Pow(8, 21)},
                    {Math.Pow(9, 1), Math.Pow(9, 2), Math.Pow(9, 3), Math.Pow(9, 4), Math.Pow(9, 5), Math.Pow(9, 6), Math.Pow(9, 7), Math.Pow(9, 8), Math.Pow(9, 9), Math.Pow(9, 10), Math.Pow(9, 11), Math.Pow(9, 12), Math.Pow(9, 13), Math.Pow(9, 14), Math.Pow(9, 15), Math.Pow(9, 16), Math.Pow(9, 17), Math.Pow(9, 18), Math.Pow(9, 19), Math.Pow(9, 20), Math.Pow(9, 21)}
                };*/

        //private static long[,] _powersL = new long[10, 21]
        //{
        //    {(long)Math.Pow(0, 1), (long)Math.Pow(0, 2), (long)Math.Pow(0, 3), (long)Math.Pow(0, 4), (long)Math.Pow(0, 5), (long)Math.Pow(0, 6), (long)Math.Pow(0, 7), (long)Math.Pow(0, 8), (long)Math.Pow(0, 9), (long)Math.Pow(0, 10), (long)Math.Pow(0, 11), (long)Math.Pow(0, 12), (long)Math.Pow(0, 13), (long)Math.Pow(0, 14), (long)Math.Pow(0, 15), (long)Math.Pow(0, 16), (long)Math.Pow(0, 17), (long)Math.Pow(0, 18), (long)Math.Pow(0, 19), (long)Math.Pow(0, 20), (long)Math.Pow(0, 21)},
        //    {(long)Math.Pow(1, 1), (long)Math.Pow(1, 2), (long)Math.Pow(1, 3), (long)Math.Pow(1, 4), (long)Math.Pow(1, 5), (long)Math.Pow(1, 6), (long)Math.Pow(1, 7), (long)Math.Pow(1, 8), (long)Math.Pow(1, 9), (long)Math.Pow(1, 10), (long)Math.Pow(1, 11), (long)Math.Pow(1, 12), (long)Math.Pow(1, 13), (long)Math.Pow(1, 14), (long)Math.Pow(1, 15), (long)Math.Pow(1, 16), (long)Math.Pow(1, 17), (long)Math.Pow(1, 18), (long)Math.Pow(1, 19), (long)Math.Pow(1, 20), (long)Math.Pow(1, 21)},
        //    {(long)Math.Pow(2, 1), (long)Math.Pow(2, 2), (long)Math.Pow(2, 3), (long)Math.Pow(2, 4), (long)Math.Pow(2, 5), (long)Math.Pow(2, 6), (long)Math.Pow(2, 7), (long)Math.Pow(2, 8), (long)Math.Pow(2, 9), (long)Math.Pow(2, 10), (long)Math.Pow(2, 11), (long)Math.Pow(2, 12), (long)Math.Pow(2, 13), (long)Math.Pow(2, 14), (long)Math.Pow(2, 15), (long)Math.Pow(2, 16), (long)Math.Pow(2, 17), (long)Math.Pow(2, 18), (long)Math.Pow(2, 19), (long)Math.Pow(2, 20), (long)Math.Pow(2, 21)},
        //    {(long)Math.Pow(3, 1), (long)Math.Pow(3, 2), (long)Math.Pow(3, 3), (long)Math.Pow(3, 4), (long)Math.Pow(3, 5), (long)Math.Pow(3, 6), (long)Math.Pow(3, 7), (long)Math.Pow(3, 8), (long)Math.Pow(3, 9), (long)Math.Pow(3, 10), (long)Math.Pow(3, 11), (long)Math.Pow(3, 12), (long)Math.Pow(3, 13), (long)Math.Pow(3, 14), (long)Math.Pow(3, 15), (long)Math.Pow(3, 16), (long)Math.Pow(3, 17), (long)Math.Pow(3, 18), (long)Math.Pow(3, 19), (long)Math.Pow(3, 20), (long)Math.Pow(3, 21)},
        //    {(long)Math.Pow(4, 1), (long)Math.Pow(4, 2), (long)Math.Pow(4, 3), (long)Math.Pow(4, 4), (long)Math.Pow(4, 5), (long)Math.Pow(4, 6), (long)Math.Pow(4, 7), (long)Math.Pow(4, 8), (long)Math.Pow(4, 9), (long)Math.Pow(4, 10), (long)Math.Pow(4, 11), (long)Math.Pow(4, 12), (long)Math.Pow(4, 13), (long)Math.Pow(4, 14), (long)Math.Pow(4, 15), (long)Math.Pow(4, 16), (long)Math.Pow(4, 17), (long)Math.Pow(4, 18), (long)Math.Pow(4, 19), (long)Math.Pow(4, 20), (long)Math.Pow(4, 21)},
        //    {(long)Math.Pow(5, 1), (long)Math.Pow(5, 2), (long)Math.Pow(5, 3), (long)Math.Pow(5, 4), (long)Math.Pow(5, 5), (long)Math.Pow(5, 6), (long)Math.Pow(5, 7), (long)Math.Pow(5, 8), (long)Math.Pow(5, 9), (long)Math.Pow(5, 10), (long)Math.Pow(5, 11), (long)Math.Pow(5, 12), (long)Math.Pow(5, 13), (long)Math.Pow(5, 14), (long)Math.Pow(5, 15), (long)Math.Pow(5, 16), (long)Math.Pow(5, 17), (long)Math.Pow(5, 18), (long)Math.Pow(5, 19), (long)Math.Pow(5, 20), (long)Math.Pow(5, 21)},
        //    {(long)Math.Pow(6, 1), (long)Math.Pow(6, 2), (long)Math.Pow(6, 3), (long)Math.Pow(6, 4), (long)Math.Pow(6, 5), (long)Math.Pow(6, 6), (long)Math.Pow(6, 7), (long)Math.Pow(6, 8), (long)Math.Pow(6, 9), (long)Math.Pow(6, 10), (long)Math.Pow(6, 11), (long)Math.Pow(6, 12), (long)Math.Pow(6, 13), (long)Math.Pow(6, 14), (long)Math.Pow(6, 15), (long)Math.Pow(6, 16), (long)Math.Pow(6, 17), (long)Math.Pow(6, 18), (long)Math.Pow(6, 19), (long)Math.Pow(6, 20), (long)Math.Pow(6, 21)},
        //    {(long)Math.Pow(7, 1), (long)Math.Pow(7, 2), (long)Math.Pow(7, 3), (long)Math.Pow(7, 4), (long)Math.Pow(7, 5), (long)Math.Pow(7, 6), (long)Math.Pow(7, 7), (long)Math.Pow(7, 8), (long)Math.Pow(7, 9), (long)Math.Pow(7, 10), (long)Math.Pow(7, 11), (long)Math.Pow(7, 12), (long)Math.Pow(7, 13), (long)Math.Pow(7, 14), (long)Math.Pow(7, 15), (long)Math.Pow(7, 16), (long)Math.Pow(7, 17), (long)Math.Pow(7, 18), (long)Math.Pow(7, 19), (long)Math.Pow(7, 20), (long)Math.Pow(7, 21)},
        //    {(long)Math.Pow(8, 1), (long)Math.Pow(8, 2), (long)Math.Pow(8, 3), (long)Math.Pow(8, 4), (long)Math.Pow(8, 5), (long)Math.Pow(8, 6), (long)Math.Pow(8, 7), (long)Math.Pow(8, 8), (long)Math.Pow(8, 9), (long)Math.Pow(8, 10), (long)Math.Pow(8, 11), (long)Math.Pow(8, 12), (long)Math.Pow(8, 13), (long)Math.Pow(8, 14), (long)Math.Pow(8, 15), (long)Math.Pow(8, 16), (long)Math.Pow(8, 17), (long)Math.Pow(8, 18), (long)Math.Pow(8, 19), (long)Math.Pow(8, 20), (long)Math.Pow(8, 21)},
        //    {(long)Math.Pow(9, 1), (long)Math.Pow(9, 2), (long)Math.Pow(9, 3), (long)Math.Pow(9, 4), (long)Math.Pow(9, 5), (long)Math.Pow(9, 6), (long)Math.Pow(9, 7), (long)Math.Pow(9, 8), (long)Math.Pow(9, 9), (long)Math.Pow(9, 10), (long)Math.Pow(9, 11), (long)Math.Pow(9, 12), (long)Math.Pow(9, 13), (long)Math.Pow(9, 14), (long)Math.Pow(9, 15), (long)Math.Pow(9, 16), (long)Math.Pow(9, 17), (long)Math.Pow(9, 18), (long)Math.Pow(9, 19), (long)Math.Pow(9, 20), (long)Math.Pow(9, 21)}
        //};

        private static long[,] _powersL = new long[10, 21]
        {
            {(long)Math.Pow(0, 0), (long)Math.Pow(0, 1), (long)Math.Pow(0, 2), (long)Math.Pow(0, 3), (long)Math.Pow(0, 4), (long)Math.Pow(0, 5), (long)Math.Pow(0, 6), (long)Math.Pow(0, 7), (long)Math.Pow(0, 8), (long)Math.Pow(0, 00), (long)Math.Pow(0, 01), (long)Math.Pow(0, 02), (long)Math.Pow(0, 03), (long)Math.Pow(0, 04), (long)Math.Pow(0, 05), (long)Math.Pow(0, 06), (long)Math.Pow(0, 07), (long)Math.Pow(0, 08), (long)Math.Pow(0, 09), (long)Math.Pow(0, 9), (long)Math.Pow(0, 10)},
            {(long)Math.Pow(1, 0), (long)Math.Pow(1, 1), (long)Math.Pow(1, 2), (long)Math.Pow(1, 3), (long)Math.Pow(1, 4), (long)Math.Pow(1, 5), (long)Math.Pow(1, 6), (long)Math.Pow(1, 7), (long)Math.Pow(1, 8), (long)Math.Pow(1, 00), (long)Math.Pow(1, 01), (long)Math.Pow(1, 02), (long)Math.Pow(1, 03), (long)Math.Pow(1, 04), (long)Math.Pow(1, 05), (long)Math.Pow(1, 06), (long)Math.Pow(1, 07), (long)Math.Pow(1, 08), (long)Math.Pow(1, 09), (long)Math.Pow(1, 9), (long)Math.Pow(1, 10)},
            {(long)Math.Pow(2, 0), (long)Math.Pow(2, 1), (long)Math.Pow(2, 2), (long)Math.Pow(2, 3), (long)Math.Pow(2, 4), (long)Math.Pow(2, 5), (long)Math.Pow(2, 6), (long)Math.Pow(2, 7), (long)Math.Pow(2, 8), (long)Math.Pow(2, 00), (long)Math.Pow(2, 01), (long)Math.Pow(2, 02), (long)Math.Pow(2, 03), (long)Math.Pow(2, 04), (long)Math.Pow(2, 05), (long)Math.Pow(2, 06), (long)Math.Pow(2, 07), (long)Math.Pow(2, 08), (long)Math.Pow(2, 09), (long)Math.Pow(2, 9), (long)Math.Pow(2, 10)},
            {(long)Math.Pow(3, 0), (long)Math.Pow(3, 1), (long)Math.Pow(3, 2), (long)Math.Pow(3, 3), (long)Math.Pow(3, 4), (long)Math.Pow(3, 5), (long)Math.Pow(3, 6), (long)Math.Pow(3, 7), (long)Math.Pow(3, 8), (long)Math.Pow(3, 00), (long)Math.Pow(3, 01), (long)Math.Pow(3, 02), (long)Math.Pow(3, 03), (long)Math.Pow(3, 04), (long)Math.Pow(3, 05), (long)Math.Pow(3, 06), (long)Math.Pow(3, 07), (long)Math.Pow(3, 08), (long)Math.Pow(3, 09), (long)Math.Pow(3, 9), (long)Math.Pow(3, 10)},
            {(long)Math.Pow(4, 0), (long)Math.Pow(4, 1), (long)Math.Pow(4, 2), (long)Math.Pow(4, 3), (long)Math.Pow(4, 4), (long)Math.Pow(4, 5), (long)Math.Pow(4, 6), (long)Math.Pow(4, 7), (long)Math.Pow(4, 8), (long)Math.Pow(4, 00), (long)Math.Pow(4, 01), (long)Math.Pow(4, 02), (long)Math.Pow(4, 03), (long)Math.Pow(4, 04), (long)Math.Pow(4, 05), (long)Math.Pow(4, 06), (long)Math.Pow(4, 07), (long)Math.Pow(4, 08), (long)Math.Pow(4, 09), (long)Math.Pow(4, 9), (long)Math.Pow(4, 10)},
            {(long)Math.Pow(5, 0), (long)Math.Pow(5, 1), (long)Math.Pow(5, 2), (long)Math.Pow(5, 3), (long)Math.Pow(5, 4), (long)Math.Pow(5, 5), (long)Math.Pow(5, 6), (long)Math.Pow(5, 7), (long)Math.Pow(5, 8), (long)Math.Pow(5, 00), (long)Math.Pow(5, 01), (long)Math.Pow(5, 02), (long)Math.Pow(5, 03), (long)Math.Pow(5, 04), (long)Math.Pow(5, 05), (long)Math.Pow(5, 06), (long)Math.Pow(5, 07), (long)Math.Pow(5, 08), (long)Math.Pow(5, 09), (long)Math.Pow(5, 9), (long)Math.Pow(5, 10)},
            {(long)Math.Pow(6, 0), (long)Math.Pow(6, 1), (long)Math.Pow(6, 2), (long)Math.Pow(6, 3), (long)Math.Pow(6, 4), (long)Math.Pow(6, 5), (long)Math.Pow(6, 6), (long)Math.Pow(6, 7), (long)Math.Pow(6, 8), (long)Math.Pow(6, 00), (long)Math.Pow(6, 01), (long)Math.Pow(6, 02), (long)Math.Pow(6, 03), (long)Math.Pow(6, 04), (long)Math.Pow(6, 05), (long)Math.Pow(6, 06), (long)Math.Pow(6, 07), (long)Math.Pow(6, 08), (long)Math.Pow(6, 09), (long)Math.Pow(6, 9), (long)Math.Pow(6, 10)},
            {(long)Math.Pow(7, 0), (long)Math.Pow(7, 1), (long)Math.Pow(7, 2), (long)Math.Pow(7, 3), (long)Math.Pow(7, 4), (long)Math.Pow(7, 5), (long)Math.Pow(7, 6), (long)Math.Pow(7, 7), (long)Math.Pow(7, 8), (long)Math.Pow(7, 00), (long)Math.Pow(7, 01), (long)Math.Pow(7, 02), (long)Math.Pow(7, 03), (long)Math.Pow(7, 04), (long)Math.Pow(7, 05), (long)Math.Pow(7, 06), (long)Math.Pow(7, 07), (long)Math.Pow(7, 08), (long)Math.Pow(7, 09), (long)Math.Pow(7, 9), (long)Math.Pow(7, 10)},
            {(long)Math.Pow(8, 0), (long)Math.Pow(8, 1), (long)Math.Pow(8, 2), (long)Math.Pow(8, 3), (long)Math.Pow(8, 4), (long)Math.Pow(8, 5), (long)Math.Pow(8, 6), (long)Math.Pow(8, 7), (long)Math.Pow(8, 8), (long)Math.Pow(8, 00), (long)Math.Pow(8, 01), (long)Math.Pow(8, 02), (long)Math.Pow(8, 03), (long)Math.Pow(8, 04), (long)Math.Pow(8, 05), (long)Math.Pow(8, 06), (long)Math.Pow(8, 07), (long)Math.Pow(8, 08), (long)Math.Pow(8, 09), (long)Math.Pow(8, 9), (long)Math.Pow(8, 10)},
            {(long)Math.Pow(9, 0), (long)Math.Pow(9, 1), (long)Math.Pow(9, 2), (long)Math.Pow(9, 3), (long)Math.Pow(9, 4), (long)Math.Pow(9, 5), (long)Math.Pow(9, 6), (long)Math.Pow(9, 7), (long)Math.Pow(9, 8), (long)Math.Pow(9, 00), (long)Math.Pow(9, 01), (long)Math.Pow(9, 02), (long)Math.Pow(9, 03), (long)Math.Pow(9, 04), (long)Math.Pow(9, 05), (long)Math.Pow(9, 06), (long)Math.Pow(9, 07), (long)Math.Pow(9, 08), (long)Math.Pow(9, 09), (long)Math.Pow(9, 9), (long)Math.Pow(9, 10)}
        };


        private static int DigitsCount(long n)
        {
            return (int)Math.Floor(Math.Log10(n)) + 1;
        }

        private static long[] CreateZeroArray(int length)
        {
            long[] res = new long[length];
            for (int i = 0; i < length; i++) res[i] = 0;
            return res;
        }

        private static long[] InitNumberTable(long n, int maxdigits)
        {
            long[] res = CreateZeroArray(maxdigits);
            int j = maxdigits - 1;
            while (n > 0)
            {
                res[j] = n % 10;
                n = n / 10;
                j--;
            }
            return res;
        }

        //private static long GetRTCPValue(long[] digits)
        //{
        //    long res = 0;
        //    for (int i = 0; i < digits.Length; i++)
        //        res += _powersL[digits[i], i];
        //    return res;
        //}

        private static int IncNumber(long[] number)
        {
            int i = number.Length - 1;
            while (i >= 0)
            {
                number[i]++;
                if (number[i] <= 9) break;
                number[i] = 0;
                i--;
            }
            return i;
        }

        private static void RecalcDBTbl(long[] tbl, long[] number, int startidx, int firstvalidindex)
        {
            if (startidx == firstvalidindex)
            {
                tbl[startidx] = number[startidx];
                startidx++;
            }
            for(int i=startidx; i<tbl.Length; i++)
                tbl[i] = tbl[i - 1] + _powersL[number[i], i - firstvalidindex + 1];
        }

        private static void InitDBTable(long[] tbl, long[] number, int firstvalidindex)
        {
            RecalcDBTbl(tbl, number, firstvalidindex, firstvalidindex);
        }

        public static long[] SumDigPow(long a, long b)
        {
            List<long> res = new List<long>();

            int adigits = DigitsCount(a);
            int bdigits = DigitsCount(b);
            int maxdigits = adigits > bdigits ? adigits : bdigits;

            long[] number = InitNumberTable(a, maxdigits);
            long[] dp = CreateZeroArray(maxdigits);

            int lastchangedindex = maxdigits - 1;
            int firstvalidindex = maxdigits - adigits;
            InitDBTable(dp, number, firstvalidindex);
            for (long i = a; i <= b; i++)
            {
                RecalcDBTbl(dp, number, lastchangedindex, firstvalidindex);
                long v = dp[maxdigits - 1];
                if (v == i) res.Add(i);
                lastchangedindex = IncNumber(number);
                if (lastchangedindex < firstvalidindex) firstvalidindex--;
            }

            return res.ToArray();
        }
    }
}
