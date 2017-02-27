/*
 * https://www.codewars.com/kata/twice-linear
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwiceLinear
{
    public class TwiceLinear
    {
        private int f1(int x)
        {
            return 2*x + 1;
        }

        private int f2(int x)
        {
            return 3*x + 1;
        }

        private float f2inv(int y)
        {
            return (y - 1f)/3f;
        }

        public int DblLinear(int n)
        {
            if (n == 0) return 1;
            int res = 0;

            return res;
        }
    }
}
