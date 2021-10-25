using System.Collections.Generic;
using System.Linq;
using NumberTheory;

namespace ConsecutiveKPrimes
{
    public class ConsecutiveKPrimes
    {
        private static readonly PrimeNumbersFactors pnf = new PrimeNumbersFactors();

        private bool IsKPrime(int k, long n)
        {
            //PrimeNumbersFactors pnf = new PrimeNumbersFactors();
            Dictionary<long, long> factors = pnf.PollardRhoPrimeFactors(n);
            return (factors.Sum(x => x.Value) == k);
        }

        public int ConsecKprimes(int k, long[] arr)
        {
            if (arr.Length < 2) return 0;

            int res = 0;
            bool lastkprime = false;
            for (int i = 0; i < arr.Length; i++)
            {
                bool currkprime = IsKPrime(k, arr[i]);
                if ((lastkprime && currkprime) == true) res++;
                lastkprime = currkprime;
            }
            return res;
        }
    }
}
