using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NumberTheory
{

    /// <summary>
    /// Klasa weryfikujący czy liczba jest pierwsza
    /// </summary>
    public class PrimeNumbersCheck
    {
        /// <summary>
        /// Test Millera-Rabina czy liczba n jest pierwsza, przy danym a
        /// patrz: Cormen i "Alogrytmika praktyczna" (rozdzial 31.8)
        /// https://en.wikipedia.org/wiki/Miller–Rabin_primality_test
        /// 
        /// Okresla czy liczba jest zlozona (true), podejrzenie ze pierwsza (false)
        /// </summary>
        /// <param name="a">podstawa</param>
        /// <param name="n">nieparzysta liczba do przetestowania</param>
        /// <returns>true - złożona, false - może być pierwsza</returns>
        public bool MillerRabinTest(long a, int n)
        {
            //dziala na zbiorze a [1, 2, .. n-1]
            if (a >= n) return false;

            //n-1 = 2^t * u -> a^(n-1)=(a^n)^2^t, gdzie t>=1 i u nieparzyste
            long u = n - 1;
            long t = 0;
            while ((u & 1) == 0)
            {
                u /= 2;
                t++;
            }

            long x = NumbersTheory.ExpMod(a, u, n);
            long y = 0;
            for (; t > 0; t--)
            {
                y = (x * x) % n;
                //y = NumbersTheory.ExpMod(x, 2, n);
                if ((y == 1) && (x != 1) && (x != n - 1)) //nietrywialny dzielnik - liczba jest zlozona
                    return true;
                x = y;
            }
            //liczba jest zlozona jezeli y!=1
            return (y != 1);
        }


        public bool IsPrimeMRTest(long x)
        {
            if (x < 2) return false;
            if (x % 2 == 0) return (x == 2);
            if (x % 3 == 0) return (x == 3);
            //if (x % 5 == 0) return (x == 5);

            //if n < 4,759,123,141, it is enough to test a = 2, 7, and 61;
            return !(MillerRabinTest(2, (int)x) || MillerRabinTest(7, (int)x) || MillerRabinTest(61, (int)x));
            //liczby 64bit sa za duze, dziala tylko na 32bit
            //if n < 18,446,744,073,709,551,616 = 2^64, it is enough to test a = 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, and 37.
            //return !(MillerRabinTest(2, x) || MillerRabinTest(3, x) || MillerRabinTest(5, x) || MillerRabinTest(7, x) ||
            //          MillerRabinTest(11, x) || MillerRabinTest(13, x) || MillerRabinTest(17, x) || MillerRabinTest(19, x) || 
            //          MillerRabinTest(23, x) || MillerRabinTest(29, x) || MillerRabinTest(31, x) || MillerRabinTest(37, x));
        }
    }
}
