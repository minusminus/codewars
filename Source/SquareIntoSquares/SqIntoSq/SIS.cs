using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqIntoSq
{
    /// <summary>
    /// Rekurencyjne rozwiazanie przegladajace w glab od n-1.
    /// Dopasowanie zaczyna sie od najwiekszych elementow do najmniejszych, dopasowywane jest pozostale pole powierzchni.
    /// Petla w dol trwa dopoki suma kwadratow liczb do danego x jest wieksza rowna pozostalej powierzchni
    /// i jezeli jest mniejsza, to nie ma mozliwosci dopasowania.
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
                if (nextn >= i) nextn = i - 1;
                if (Process(nextn, spaceleft - i*i)) return true;
                _solution.Pop();
            }
            return false;
        }

        public string PrepareResult()
        {
            return string.Join(" ", _solution.ToArray());
        }

        public string Decompose(long n)
        {
            if (n < 2) return null;

            _solution.Clear();
            if (Process(n - 1, n*n))
                return PrepareResult();
            return null;
        }
    }
}
