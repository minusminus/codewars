using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAWIP
{
    public class GAWIP
    {
        private BitArray _products;
        private int[] _idx;

        private void CalcNextItermediate(int n, int curr, int ix)
        {
            Console.Write($"({n},{curr},{ix}) ");
            if (curr <= 1)
            {
                //Console.WriteLine($"[{ix}]={_idx[ix - 1]}");
                _products[_idx[ix - 1]] = true;
                Console.WriteLine($"*p={_idx[ix - 1]}");
            }
            else
            {
                _idx[ix] = _idx[ix - 1]*curr;
                for (int j = n - curr; j > 0; j--)
                {
                    Console.WriteLine($" p={_idx[ix]}, ");
                    CalcNextItermediate(n-curr, j, ix + 1);
                }
            }
        }

        private void CalcProducts(int n)
        {
            _products[1] = true;
            _products[n] = true;

            for (int i = n - 1; i >= 2; i--)
            {
                Console.WriteLine($"=== {i}:");
                CalcNextItermediate(n, i, 1);
            }
        }

        private string PrepareResult(int n)
        {
            long max = 0;
            int cnt = 0;
            long sum = 0;
            for (int i = 1; i < _products.Length; i++)
            {
                if (_products[i])
                {
                    cnt++;
                    max = (i > max) ? i : max;
                    sum += i;
                    Console.WriteLine($"{i}");
                }
            }
            Console.WriteLine($"cnt={cnt}, max={max}, sum={sum}");
            int i1, i2;
            if (cnt%2 == 1)
                i1 = i2 = cnt/2 + 1;
            else
            {
                i1 = cnt/2;
                i2 = i1 + 1;
            }
            int j = 0;
            int v1, v2;
            for (int i = 1; (i < _products.Length) && (j>=Math.Max(i1,i2)); i++)
            {
                if (_products[i])
                {
                    j++;
                    if (j == i1) v1 = i;
                    if (j == i2) v2 = i;
                }
            }

            var culture = CultureInfo.CreateSpecificCulture("en-US");
            return $"Range: {max-1} Average: {((double)sum/(double)cnt).ToString("F2", culture)} Median: {((double)(i1+i2)/2.0).ToString("F2", culture)}";
        }

        public string Part(long n)
        {
            //if (n == 1) return "Range: 0 Average: 1.00 Median: 1.00";

            //_products = new BitArray((int) n*(int) n);
            _products = new BitArray(1000000);  //buffer size experimentally selected
            _idx = new int[n + 1];
            _idx[0] = 1;

            CalcProducts((int)n);
            return PrepareResult((int)n);
        }
    }
}
