using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            //return (x*x + c)%n;

            long d = ((x*x)%n + c)%n;
            if (d < 0) d = n-d;
            return d;
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

            long x = startx;
            long y = startx;
            long d = 1;
            int iters = 0;

            while (d != n)
            {
                x = f(x, c, n);
                y = f(f(y, c, n), c, n);
                d = NumbersTheory.GCDBinary(Math.Abs(x - y), n);
                if ((d > 1) && (d < n))
                    if(!factors.Contains(d))
                        factors.Add(d);
                iters++;
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

        private long RandomLong(long min, long max, Random gen)
        {
            if (min == max) return min;
            byte[] buf = new byte[8];
            gen.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            return (Math.Abs(longRand % (max - min)) + min);
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
            //Console.WriteLine($"--- n={n} ---");
            PrimeNumbersCheck pcheck = new PrimeNumbersCheck();
            Dictionary<long, long> primeFactors = new Dictionary<long, long>();
            if (pcheck.IsPrimeMRTest(n)) return primeFactors;

            Random gen = new Random();
            List<long> toCheck = new List<long>() {n};
            long nv = n;
            while (nv > 1)
            {
                long v = nv;
                if (toCheck.Count > 0)
                {
                    v = toCheck[0];
                    toCheck.RemoveAt(0);
                }
                //long c = gen.Next(2, (int)v);
                //long startx = gen.Next(1, (int)v);
                long c = RandomLong(2, v, gen);
                long startx = RandomLong(1, v, gen);
                Console.WriteLine($"v={v}, x={startx}, c={c}");
                List<long> factors = GetPollardRhoFactorsList(v, startx, c);
                //foreach (long factor in factors) Console.Write($"{factor},");
                //Console.WriteLine("");
                foreach (long factor in factors)
                {
                    if (pcheck.IsPrimeMRTest(factor))
                    {
                        if (!primeFactors.ContainsKey(factor))
                        {
                            long cnt = GetFactorsCount(nv, factor, out nv);
                            primeFactors[factor] = cnt;
                            Console.WriteLine($"=== {factor} ^ {cnt}");
                            //ostatni dzielnik jest liczba pierwsza
                            if (pcheck.IsPrimeMRTest(nv))
                            {
                                primeFactors[nv] = 1;
                                nv = 1;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if(!toCheck.Contains(factor)) toCheck.Add(factor);
                        long d = n/factor;
                        if (!toCheck.Contains(d)) toCheck.Add(d);
                    }
                }
            }

            return primeFactors;
        }
    }
}
