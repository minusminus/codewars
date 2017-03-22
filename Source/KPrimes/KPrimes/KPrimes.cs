using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumberTheory;

namespace KPrimes
{
    public class KPrimes
    {
        public long[] CountKprimes(int k, long start, long end)
        {
            List<long> kprimes = new List<long>();
            PrimeNumbersFactors pnf = new PrimeNumbersFactors();
            PrimeNumbersCheck pchk = new PrimeNumbersCheck();

            if (k > 1)
            {
                if (start < (1 << k)) start = (1 << k);
                for (long i = start; i <= end; i++)
                {
                    Dictionary<long, long> factors = pnf.PollardRhoPrimeFactors(i);
                    if (factors.Sum(x => x.Value) == k)
                        kprimes.Add(i);
                }
            }
            else
            {
                if (start < 2) start = 2;
                for (long i = start; i <= end; i++)
                {
                    if (pchk.IsPrimeMRTest(i))
                        kprimes.Add(i);
                }
            }

            return kprimes.ToArray();
        }

        public int Puzzle(int s)
        {
            Console.WriteLine($"s = {s}");
            if (s < 138) return 0;
            int res = 0;

            Stopwatch sw = Stopwatch.StartNew();
            List<long> p1 = new List<long>();
            List<long> p3 = new List<long>();
            List<long> p7 = new List<long>();
            PrimeNumbersFactors pnf = new PrimeNumbersFactors();

            //maksymalna liczba jest s - 8 - 2 = s - 10, minimalna 2
            //wygenerowane listy sa zawsze posortowane
            for (long i = 2; i <= s - 10; i++)
            {
                Dictionary<long, long> factors = pnf.PollardRhoPrimeFactors(i);
                if (factors.Count == 0)
                    p1.Add(i);
                else
                {
                    long cnt = factors.Sum(x => x.Value);
                    if (cnt == 3)
                        p3.Add(i);
                    else if (cnt == 7)
                        p7.Add(i);
                }
            }
            Console.WriteLine($"lists: {sw.ElapsedMilliseconds} ms");
            if ((p1.Count == 0) || (p3.Count == 0) || (p7.Count == 0)) return 0;

            //bruteforce
            //for(int i=0; i<p1.Count; i++)
            //    for(int j=0; j<p3.Count; j++)
            //        for(int k=0; k<p7.Count; k++)
            //            if (p1[i] + p3[j] + p7[k] == s) res++;

            for (int i = 0; i < p1.Count; i++)
                for (int j = 0; j < p3.Count; j++)
                    if (p7.BinarySearch(s - (p1[i] + p3[j])) >= 0) res++;


            sw.Stop();
            Console.WriteLine($"check: {sw.ElapsedMilliseconds} ms");

            return res;
        }
    }
}
