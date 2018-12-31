using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqIntoSq
{
    /// <summary>
    /// 
    /// suma kwadratow n pierwszych liczb naturalnych: n(n+1)(2n+1)/6
    /// </summary>
    public class SIS
    {
        private readonly Stack<long> _solution = new Stack<long>();

        public bool Process(long n, long spaceleft)
        {
            if (spaceleft == 0) return true;
            for (long i = n; i*(i+1)*(2*i+1)/6 >= spaceleft; i--)
            {
                _solution.Push(i);
                long nextn = (long)Math.Floor(Math.Sqrt(spaceleft - i*i));
                if (Process(nextn, spaceleft - i*i)) return true;
                _solution.Pop();
            }
            return false;
        }

        public string PrepareResult()
        {
            long[] arr = _solution.ToArray();
            //Array.Reverse(arr);
            return string.Join(" ", arr);
        }

        public string Decompose(long n)
        {
            _solution.Clear();
            if (Process(n - 1, n*n))
                return PrepareResult();
            return "";
        }
    }
}
