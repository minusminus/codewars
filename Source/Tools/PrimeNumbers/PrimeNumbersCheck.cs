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
        /// Test Millera-Rabina czy liczba n jest pierwsza, przy danym x
        /// patrz: Cormen i "Alogrytmika praktyczna"
        /// https://en.wikipedia.org/wiki/Miller–Rabin_primality_test
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool MillerRabinTest(long x, long n)
        {
            if (x >= n) return false;

            long d = 1;
            long y;
            long  t = 0;
            long l = n - 1;
            //ustalenie nieparzystego u
            while ((l & 1) == 0)
            {
                t++;
                l >>= 1;
            }
            //petla od 1 do t
            for (; (l > 0) || (t-- > 0); l >>= 1)
            {
                if ((l & 1) == 1) d = (d*x)%n;
                if (l == 0)
                {
                    x = d;
                    l = -l;
                }
                y = (x*x)%n;
                if ((y == 1) && (x != 1) && (x != n - 1)) return true;
                x = y;
            }
            return (x != 1);
        }

        public bool MillerRabinTest2(long a, int n)
        {
            //ustalenie nieparzystego d
            long d = n/2;
            long s = 1;
            while ((d & 1) != 0)
            {
                d /= 2;
                s++;
            }

            //n-1 = 2^s * d (with d odd by factoring powers of 2 from n−1)
            long x = NumbersTheory.ExpMod(a, d, n);
            long y = 0;
            while (s > 0)
            {
                y = (x * x) % n;
                if ((y == 1) && (x != 1) && (x != n - 1))   //nietrywialny dzielnik - liczba jest zlozona
                    return false;
                x = y;
                s--;
            }
            if (y != 1)
                return false;
            return true;
        }

        public bool MillerRabinTest3(int k, int n)
        {
            if (n < 2)
            {
                return false;
            }
            if (n != 2 && n % 2 == 0)
            {
                return false;
            }
            int s = n - 1;
            while (s % 2 == 0)
            {
                s >>= 1;
            }
            Random r = new Random();
            for (int i = 0; i < k; i++)
            {
                double a = r.Next((int)n - 1) + 1;
                int temp = s;
                int mod = (int)Math.Pow(a, (double)temp) % n;
                while (temp != n - 1 && mod != 1 && mod != n - 1)
                {
                    mod = (mod * mod) % n;
                    temp = temp * 2;
                }
                if (mod != n - 1 && temp % 2 == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsPrimeMRTest(long x)
        {
            //if ((x % 2 == 0) && (x > 2)) return false;
            //if ((x % 3 == 0) && (x > 3)) return false;
            //if (x < 2) return false;
            //if (x <= 3) return true;

            //return
            //    !((x < 2) || MillerRabinTest(2, x) || MillerRabinTest(3, x) || MillerRabinTest(5, x) ||
            //      MillerRabinTest(7, x));

            //bool b = (x < 2);
            //bool b2 = MillerRabinTest(2, x);
            //bool b3 = MillerRabinTest(3, x);
            //bool b5 = MillerRabinTest(5, x);
            //bool b7 = MillerRabinTest(7, x);
            ////return !(b || (b2 && b3 && b5 && b7));
            //return !(b2 && b3 && b5 && b7);
            ////return !(b2 || b3 || b5 || b7);

            //if n < 4,759,123,141, it is enough to test a = 2, 7, and 61;
            bool b2 = MillerRabinTest(2, x);
            bool b7 = MillerRabinTest(7, x);
            bool b61 = MillerRabinTest(61, x);
            return !(b2 || b7 || b61);

            //bool b2 = MillerRabinTest2(2, (int)x);
            //bool b7 = MillerRabinTest2(7, (int)x);
            //bool b61 = MillerRabinTest2(61, (int)x);
            //return !(b2 || b7 || b61);

            //bool b2 = MillerRabinTest3(2, (int)x);
            //bool b7 = MillerRabinTest3(7, (int)x);
            //bool b61 = MillerRabinTest3(61, (int)x);
            //return !(b2 || b7 || b61);
        }
    }
}
