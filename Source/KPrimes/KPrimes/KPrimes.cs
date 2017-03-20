using System;
using System.Collections.Generic;
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
            return 0;
        }
    }
}
