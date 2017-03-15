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
        public Dictionary<long, long> PollardRhoFactors(long n)
        {
            Dictionary<long, long> primeFactors = new Dictionary<long, long>();
            Random gen = new Random();
            PrimeNumbersCheck pcheck = new PrimeNumbersCheck();

            while (true)
            {
                if (pcheck.IsPrimeMRTest(n)) break;

                long c = gen.Next(2, (int) n);
                long startx = gen.Next(1, (int) n);
                List<long> factors = GetPollardRhoFactorsList(n, startx, c);
                foreach (long factor in factors)
                {
                    if (pcheck.IsPrimeMRTest(factor))
                    {
                        if (primeFactors.ContainsKey(factor))
                            primeFactors[factor] += 1;
                        else
                            primeFactors[factor] = 1;
                    }
                }
            }
            return primeFactors;
        }
    }
}
