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
    /// </summary>
    public class CCC
    {
        private List<List<long>>[] _arr;

        public int CountCombinations(int money, int[] coins)
        {
            Array.Sort(coins);  //wartosci do rozkladu posortowane
            _arr = new List<List<long>>[coins.Length + 1];
            for (int i = 0; i < _arr.Length; i++)
                _arr[i] = new List<List<long>>();

            return 0;
        }
    }
}
