using System;
using System.Collections.Generic;

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
            return (NumbersTheory.ExpMod(x, 2, n) + c)%n;
        }

        /// <summary>
        /// Metoda wyznaczajaca dzielniki n.
        /// Zwraca liste dzielnikow. Petla ograniczona do 25 iteracji.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="startx"></param>
        /// <param name="c"></param>
        /// <returns>Zwraca listę dzielników, każdy może wystąpic na niej tylko raz</returns>
        public List<long> GetPollardRhoFactorsList(long n, long startx, long c)
        {
            const int maxiters = 25;    //maksymalna ilosc iteracji algorytmu
            List<long> factors = new List<long>();

            long x = startx;
            long y = startx;
            long d = 1;
            int iters = 0;

            while ((d != n) && (iters < maxiters))
            {
                x = f(x, c, n);
                y = f(f(y, c, n), c, n);
                d = NumbersTheory.GCDBinary(Math.Abs(x - y), n);
                if ((d > 1) && (d < n))
                    if (!factors.Contains(d))
                        factors.Add(d);
                iters++;
            }
            return factors;
        }

        /// <summary>
        /// Metoda wyznaczajaca dzielniki n.
        /// Zwraca pierwszy znaleziony dzielnik.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="startx"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public List<long> GetPollardRhoSingleFactor(long n, long startx, long c)
        {
            List<long> factors = new List<long>();

            long x = startx;
            long y = startx;
            long d = 1;

            while (d != n)
            {
                x = f(x, c, n);
                y = f(f(y, c, n), c, n);
                d = NumbersTheory.GCDBinary(Math.Abs(x - y), n);
                if ((d > 1) && (d < n))
                {
                    factors.Add(d);
                    break;
                }
            }
            return factors;
        }

        private long GetFactorPower(long n, long factor, out long newn)
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

        /// <summary>
        /// Liczy ilosc potege podzielnika, aktualizuje liczbe. 
        /// Jezeli ostatnim podzielnikiem jest liczba pierwsza to zwraca true, w przeciwnym wypadku false.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="factor"></param>
        /// <param name="newn"></param>
        /// <param name="primeFactors"></param>
        /// <param name="pcheck"></param>
        /// <returns></returns>
        private bool GetPrimeFactorPowerAndUpdateList(long n, long factor, out long newn, Dictionary<long, long> primeFactors, PrimeNumbersCheck pcheck)
        {
            long cnt = GetFactorPower(n, factor, out newn);
            if (cnt > 0)
            {
                primeFactors[factor] = cnt;
                //ostatni dzielnik jest liczba pierwsza
                if (pcheck.IsPrimeMRTest(newn))
                {
                    primeFactors[newn] = 1;
                    newn = 1;
                    return true;
                }
            }
            return false;
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
        /// dziala dla liczb 32bit
        /// 
        /// https://www.cs.colorado.edu/~srirams/courses/csci2824-spr14/pollardsRho.html
        /// http://www.geeksforgeeks.org/pollards-rho-algorithm-prime-factorization/
        /// https://en.wikipedia.org/wiki/Pollard%27s_rho_algorithm
        /// https://pl.wikipedia.org/wiki/Algorytm_faktoryzacji_rho_Pollarda
        /// 
        /// opierajac sie na pierwszym znalezionym dzielniku dziala ok 2,5x szybciej niz w przypadku listy z ograniczona iloscia iteracji (1200ms do 3000ms)
        /// </summary>
        /// <param name="n"></param>
        /// <returns>zwraca słownik w postaci (czynnik, ilość)</returns>
        public Dictionary<long, long> PollardRhoPrimeFactors(long n)
        {
            PrimeNumbersCheck pcheck = new PrimeNumbersCheck();
            Dictionary<long, long> primeFactors = new Dictionary<long, long>();
            if (pcheck.IsPrimeMRTest(n)) return primeFactors;

            //okreslenie poteg 2
            if (GetPrimeFactorPowerAndUpdateList(n, 2, out n, primeFactors, pcheck)) return primeFactors;

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
                //List<long> factors = GetPollardRhoFactorsList(v, startx, c);
                List<long> factors = GetPollardRhoSingleFactor(v, startx, c);   //singlefactor ok 2,5x szybciej niz z lista i ograniczeniem petli
                foreach (long factor in factors)
                {
                    if (pcheck.IsPrimeMRTest(factor))
                    {
                        if (!primeFactors.ContainsKey(factor))
                        {
                            if (GetPrimeFactorPowerAndUpdateList(nv, factor, out nv, primeFactors, pcheck)) break;
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
