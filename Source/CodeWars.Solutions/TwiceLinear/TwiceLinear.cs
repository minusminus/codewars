/*
 * https://www.codewars.com/kata/twice-linear
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace TwiceLinear
{
    public class TwiceLinear
    {
        private int f1(int x)
        {
            return 2*x + 1;
        }

        private int f2(int x)
        {
            return 3*x + 1;
        }

        private float f2inv(int y)
        {
            return (y - 1f)/3f;
        }

        private int countElements(int n)
        {
            return n + (int)Math.Floor( f2inv(f1(n)) );
        }

        //public int DblLinear(int n)
        //{
        //    if (n == 0) return 1;

        //    int left = 1;
        //    int right = countElements(n);
        //    while (right - left > 1)
        //    {
        //        int half = (right - left)/2 + left;
        //        int cnt = countElements(half);
        //        if (cnt > n)
        //            right = half;
        //        else
        //            left = half;
        //    }

        //    int cntl = countElements(left);
        //    int cntr = countElements(right);
        //    List<int> tbl = new List<int>();
        //    for (int i = cntl; i <= cntr; i++)
        //    {
        //        tbl.Add(f1(i));
        //        tbl.Add(f2(i));
        //    }
        //    tbl.Sort();
        //    return tbl[n - cntl];
        //}

        /// <summary>
        /// Dane do tablicy generowane są kolejnymi poziomami, zaczynając od pierwszego z elementem 1.
        /// W kolejnych krokach generowane są nowe elementy tylko dla ostatniego poziomu (poprzednie już są wygenerowane).
        /// Sprawdzanie, czy są powtórzenia w trakcie generowania bardzo mocno spowalnia - zostało usunięte.
        /// Generowane są wszystkie liczby do osiągnięcia n:
        /// - później dodawane do następnego poziomu są tylko te, które nie przekroszyły maks z dotychczas wygenerowanych
        /// - po osiągnięciu n generowanie jest tylko dla liczb, które mogą zmieścić się poniżej maks (ta opcja ma mniej kodu)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int DblLinear(int n)
        {
            List<int> res = new List<int>();
            res.Add(1);

            int istart = 0;
            int vmax = res[0];
            int halfmax = (int)Math.Ceiling((vmax - 1)/2.0f);
            while (true)
            {
                List<int> tbl = new List<int>();
                for (int i = istart; i < res.Count; i++)
                {
                    int v;
                    //f1
                    if (res.Count + tbl.Count <= n)
                    {
                        v = f1(res[i]);
                        vmax = Math.Max(vmax, v);
                        halfmax = (int)Math.Ceiling((vmax - 1) / 2.0f);
                        tbl.Add(v);
                    }
                    else
                    {
                        if (res[i] <= halfmax)
                        {
                            v = f1(res[i]);
                            //if (v < vmax)
                                tbl.Add(v);
                        }
                    }
                    //f2
                    if (res.Count + tbl.Count <= n)
                    {
                        v = f2(res[i]);
                        vmax = Math.Max(vmax, v);
                        halfmax = (int)Math.Ceiling((vmax - 1) / 2.0f);
                        tbl.Add(v);
                    }
                    else
                    {
                        if (res[i] <= halfmax)
                        {
                            v = f2(res[i]);
                            //if (v < vmax)
                                tbl.Add(v);
                        }
                    }
                }
                if (tbl.Count == 0) break;
                istart = res.Count;
                res.AddRange(tbl);
            }

            res = res.Distinct().ToList();
            res.Sort();
            return res[n];
        }

        /// <summary>
        /// Ze strony
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int DblLinear_SortedSet(int n)
        {
            SortedSet<int> u = new SortedSet<int> { 1 };

            while (n > 0)
            {
                u.Add(2 * u.Min + 1);
                u.Add(3 * u.Min + 1);

                u.Remove(u.Min);
                n--;
            }
            return u.Min;
        }

        /// <summary>
        /// Ze strony.
        /// Obrzydliwie szybkie i proste!
        /// Rozwinięty pomysł minimum, zapisujacy do tablicy kolejne minimalne posortowane wartosci.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int DblLinear_2(int n)
        {
            var h = new int[++n];
            int x2 = 1, x3 = 1;
            int i = 0, j = 0;
            for (int index = 0; index < n; index++)
            {
                h[index] = x2 < x3 ? x2 : x3;
                if (h[index] == x2) x2 = 2 * h[i++] + 1;
                if (h[index] == x3) x3 = 3 * h[j++] + 1;
            }
            return h[--n];
        }
    }
}
