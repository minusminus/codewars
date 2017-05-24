using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC
{
    /// <summary>
    /// algorytm podobny do wykorzystanego w GettingAlongWithIntegerPartitions (wersja GAWIP2)
    /// 
    /// 
    /// UWAGA: ograniczenie wyniku do int wynika z definicji zadania (klasy do uzupelnienia w codewars)
    /// </summary>
    public class CCC
    {
        private int DPProcess(int money, int[] sortedcoins, int[,] dp)
        {
            return 0;
        }

        public int CountCombinations(int money, int[] coins)
        {
            int[] sortedcoins = coins.Where(x => (x > 0) && (x <= money))
                .OrderBy(x => x)
                .ToArray();
            int[,] dp = new int[money, sortedcoins.Count()];

            return DPProcess(money, sortedcoins, dp);
        }
    }
}
