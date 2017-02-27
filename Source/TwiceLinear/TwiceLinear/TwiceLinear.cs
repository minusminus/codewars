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

        private int countElements(int n)
        {
            return n + (int)Math.Floor( f2inv(f1(n)) );
        }

        public int DblLinearBruteforce(int n)
        {
            List<int> tbl = new List<int>();
            tbl.Add(1);
            for (int i = 0; i < n; i++)
            {
                int v = f1(tbl[i]);
                if(!tbl.Contains(v)) tbl.Add(v);
                v = f2(tbl[i]);
                if (!tbl.Contains(v)) tbl.Add(v);
            }
            tbl.Sort();
            return tbl[n];
        }

        public int DblLinear(int n)
        {
            if (n == 0) return 1;

            int left = 1;
            int right = countElements(n);
            while (right - left > 1)
            {
                int half = (right - left)/2 + left;
                int cnt = countElements(half);
                if (cnt > n)
                    right = half;
                else
                    left = half;
            }

            int cntl = countElements(left);
            int cntr = countElements(right);
            List<int> tbl = new List<int>();
            for (int i = cntl; i <= cntr; i++)
            {
                tbl.Add(f1(i));
                tbl.Add(f2(i));
            }
            tbl.Sort();
            return tbl[n - cntl];
        }
    }
}
