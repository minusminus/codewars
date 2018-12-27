using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumberTheory;

namespace PrimesInNumbers
{
    public class PrimesInNumbers
    {

        public String factors(int lst)
        {
            PrimeNumbersFactors pnf = new PrimeNumbersFactors();

            Dictionary<long, long> pfactors = pnf.PollardRhoPrimeFactors((long) lst);

            string res = "";
            if (pfactors.Count == 0)
            {
                res = $"({lst})";
            }
            else
            {
                foreach (var f in pfactors.OrderBy(x => x.Key))
                {
                    if (f.Value == 1)
                        res += $"({f.Key})";
                    else
                        res += $"({f.Key}**{f.Value})";
                }
            }

            return res;
        }
    }
}
