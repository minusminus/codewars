using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAWIP
{
    struct Result
    {
        public int Range;
        public double Average;
        public double Median;
    }

    public class GAWIP
    {
        private BitArray _products;
        private int[] _idx;
        private Result res;

        private void CalcNextItermediate(int n, int curr, int ix)
        {
            if (curr == 1)
            {
                _products[_idx[ix - 1]] = true;
            }
            else
            {
                _idx[ix] = _idx[ix - 1]*curr;
                for(int j=n-curr; j>0; j--)
                    CalcNextItermediate(n, j, ix + 1);
            }
        }

        private void CalcProducts(int n)
        {
            _products[1] = true;
            _products[n] = true;

            for (int i = n - 1; i >= 2; i--)
            {
                CalcNextItermediate(n, i, 1);
            }
        }

        private void PrepareResult(int n)
        {
            long max = 0;
            int cnt = 0;
            _products.OfType<bool>().Select((b, i) =>
            {
                if (b)
                {
                    cnt++;
                    max = (i > max) ? i : max;
                    Console.WriteLine($"{i}");
                }
                return 0;
            });
        }

        public string Part(long n)
        {
            _products = new BitArray((int) n*(int) n);
            _idx = new int[n + 1];
            _idx[0] = 1;

            CalcProducts((int)n);
            PrepareResult((int)n);

            return "";
        }
    }
}
