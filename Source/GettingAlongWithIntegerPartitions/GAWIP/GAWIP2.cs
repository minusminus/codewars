using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAWIP
{
    /// <summary>
    /// prawie "programowanie dynamiczne"
    /// z wykorzystaniem poprzednio wygenerowanych list dla mniejszych ilości "1"
    /// np 3+6*1 to 2+4*1 i 3+3*1
    /// czyli X z N*1 liczne nastepujaco:
    ///     X+(N-X)*1 i (X-1)+(N-X-1)*1 i (X-2)+(N-X-2)*1 i ... 2+(N-2)*1
    /// </summary>
    public class GAWIP2
    {
        private List<List<long>>[] _arr;

        private void CalcOneLevel(int total, int x)
        {
            int ones = total - x;
            //for (int i = Math.Min(ones, x); i >= 0; i--)
            ////for (int i = 1; i<=Math.Min(ones, x); i++)
            //{
            //    List<long> l = new List<long>();
            //    for (int j = 0; j <= i; j++)
            //    {
            //        //l.AddRange(_arr[x - j][j].Select(val => x * val));
            //        foreach (long val in _arr[x-j][j])
            //            l.Add(x*val);
            //    }
            //    //_arr[x][i] = l;
            //    _arr[x].Add(l);
            //}

            for (int v = x; v > 1; v--) //dla kazdej liczby od x do 2
            {
                List<long> l = new List<long>();

                for (int j = Math.Min(ones, v); j > 1; j--)
                {
                    foreach (long val in _arr[j][ones-j])
                        l.Add(v * val);
                }

                _arr[v].Add(l);
            }
        }

        private void CalcProducts(int n)
        {
            //inicjalizacja elementu 0*1
            for (int i = 1; i <= n; i++)
            {
                List<long> l = new List<long>() {i};
                _arr[i].Add(l); //0
                _arr[i].Add(l); //1
            }
            //obliczenia dla kazdej poczatkowej liczby: n, n-1, n-2, ..., 2
            //n i n-1 pomijane bo nie ma dla nich zadnej zmiany, zmiany dopiero pojawiaja sie gdy sa wolne dwie jedynki
            for (int x = n-2; x > 1; x--)
                CalcOneLevel(n, x);
        }

        private string PrepareResult(int n)
        {
            List<long> res = new List<long>();
            foreach (var xlist in _arr)
                foreach (var l in xlist)
                    res.AddRange(l);
            res = res.Distinct().ToList();
            res.Sort();

            //Console.WriteLine($"{res.Count}");
            //foreach (long l in res)
            //    Console.Write($"{l},");
            //Console.WriteLine("");

            long i1, i2;
            if (res.Count%2 == 1)   //res is 0 based
            {
                //i1 = i2 = res[res.Count/2 + 1];
                i1 = i2 = res[res.Count / 2];
            }
            else
            {
                //i1 = res[res.Count/2];
                //i2 = res[res.Count/2 + 1];
                i1 = res[res.Count / 2 - 1];
                i2 = res[res.Count / 2];
            }

            var culture = CultureInfo.CreateSpecificCulture("en-US");
            return $"Range: {res.Last() - res.First()} Average: {(Math.Floor(100.0 * ((double)res.Sum() / (double)res.Count)) / 100.0).ToString("F2", culture)} Median: {((double)(i1 + i2) / 2.0).ToString("F2", culture)}";
        }

        public string Part(long n)
        {
            _arr = new List<List<long>>[n + 1];
            for(int i=0; i<_arr.Length; i++)
                _arr[i] = new List<List<long>>();

            CalcProducts((int)n);
            return PrepareResult((int)n);
        }
    }
}
