using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberTheory
{
    /// <summary>
    /// Klasa wyznaczająca czynniki pierwsze danej liczby
    /// </summary>
    public class PrimeNumbersFactors
    {
        private long f(long x, long c, long n)
        {
            return (x*x + c)%n;
        }

        /// <summary>
        /// Metoda wyznaczajaca dzielniki n.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="startx"></param>
        /// <param name="c"></param>
        /// <returns>Zwraca listę dzielników, każdy może wystąpic na niej tylko raz</returns>
        public List<long> GetPollardRhoFactorsList(long n, long startx, long c)
        {
            List<long> factors = new List<long>();

            //Random gen = new Random();
            //long c = gen.Next(2, (int)n);
            long x = startx;
            long y = startx;
            long d = 1;

            while (d != n)
            {
                x = f(x, c, n);
                y = f(f(y, c, n), c, n);
                d = NumbersTheory.GCDBinary(Math.Abs(x - y), n);
                if ((d > 1) && (d < n))
                    if(!factors.Contains(d))
                        factors.Add(d);
            }

            return factors;
        }

        private long GetFactorsCount(long n, long factor, out long newn)
        {
            long res = 0;
            while (n%factor == 0)
            {
                n /= factor;
                res++;
            }
            newn = n;
            return res;
        }

        private void UpdateFactorsList(Dictionary<long, long> primeFactors, long factor, long count)
        {
            //if (primeFactors.ContainsKey(factor))
            //    primeFactors[factor] += count;
            //else
            //    primeFactors[factor] = count;
            if (!primeFactors.ContainsKey(factor))
                primeFactors[factor] = count;
        }

        /// <summary>
        /// Metoda faktoryzacji na czynniki pierwsze z wykorzystaniem algorytmu Rho Pollarda
        /// 
        /// https://www.cs.colorado.edu/~srirams/courses/csci2824-spr14/pollardsRho.html
        /// http://www.geeksforgeeks.org/pollards-rho-algorithm-prime-factorization/
        /// https://en.wikipedia.org/wiki/Pollard%27s_rho_algorithm
        /// https://pl.wikipedia.org/wiki/Algorytm_faktoryzacji_rho_Pollarda
        /// </summary>
        /// <param name="n"></param>
        /// <returns>zwraca słownik w postaci (czynnik, ilość)</returns>
        public Dictionary<long, long> PollardRhoPrimeFactors(long n)
        {
            PrimeNumbersCheck pcheck = new PrimeNumbersCheck();
            Dictionary<long, long> primeFactors = new Dictionary<long, long>();
            if (pcheck.IsPrimeMRTest(n)) return primeFactors;

            Random gen = new Random();
            //List<long> toCheck = new List<long>() {n};
            //while (toCheck.Count > 0)
            //{
            //    long v = toCheck[0];
            //    toCheck.RemoveAt(0);

            //    long c = gen.Next(2, (int) v);
            //    long startx = gen.Next(1, (int) v);
            //    List<long> factors = GetPollardRhoFactorsList(v, startx, c);
            //    foreach (long factor in factors)
            //    {
            //        long df = n/factor;
            //        bool pf = pcheck.IsPrimeMRTest(factor);
            //        bool pdf = pcheck.IsPrimeMRTest(df);
            //        if (pf)
            //            UpdateFactorsList(primeFactors, factor, GetFactorsCount(n, factor));
            //        else if (!pdf)
            //            toCheck.Add(df);
            //        if (pdf)
            //            UpdateFactorsList(primeFactors, df, GetFactorsCount(n, df));
            //        else if (!pf)
            //            toCheck.Add(factor);
            //    }
            //}

            long v = n;
            while (v > 1)
            {
                long c = gen.Next(2, (int)v);
                long startx = gen.Next(1, (int)v);
                List<long> factors = GetPollardRhoFactorsList(v, startx, c);
                foreach (long factor in factors)
                {
                    if (pcheck.IsPrimeMRTest(factor))
                    {
                        long cnt = GetFactorsCount(v, factor, out v);
                        primeFactors[factor] = cnt;
                        //Console.WriteLine($"{factor} ^ {cnt}");
                        //ostatni dzielnik jest liczba pierwsza
                        if (pcheck.IsPrimeMRTest(v))
                        {
                            primeFactors[v] = 1;
                            break;
                        }
                    }
                }
            }

            return primeFactors;
        }
    }
}
