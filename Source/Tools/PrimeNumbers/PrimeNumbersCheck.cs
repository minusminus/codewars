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

        public bool IsPrimeMRTest(long x)
        {
            return
                !((x < 2) || MillerRabinTest(2, x) || MillerRabinTest(3, x) || MillerRabinTest(5, x) ||
                  MillerRabinTest(7, x));
        }
    }
}
