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

        //public int DblLinear(int n)
        //{
        //    if (n == 0) return 1;

        //    int left = 1;
        //    int right = countElements(n);
        //    while (right - left > 1)
        //    {
        //        int half = (right - left)/2 + left;
        //        int cnt = countElements(half);
        //        if (cnt > n)
        //            right = half;
        //        else
        //            left = half;
        //    }

        //    int cntl = countElements(left);
        //    int cntr = countElements(right);
        //    List<int> tbl = new List<int>();
        //    for (int i = cntl; i <= cntr; i++)
        //    {
        //        tbl.Add(f1(i));
        //        tbl.Add(f2(i));
        //    }
        //    tbl.Sort();
        //    return tbl[n - cntl];
        //}

        public int DblLinear(int n)
        {
            List<int> res = new List<int>();
            res.Add(1);

            int istart = 0;
            int vmax = res[0];
            while (true)
            {
                List<int> tbl = new List<int>();
                for (int i = istart; i < res.Count; i++)
                {
                    int v;
                    //f1
                    if (res.Count + tbl.Count <= n)
                    {
                        v = f1(res[i]);
                        vmax = Math.Max(vmax, v);
                        if (!tbl.Contains(v))
                            tbl.Add(v);
                    }
                    else
                    {
                        v = f1(res[i]);
                        if ((v < vmax) && !tbl.Contains(v))
                            tbl.Add(v);
                    }
                    //f2
                    if (res.Count + tbl.Count <= n)
                    {
                        v = f2(res[i]);
                        vmax = Math.Max(vmax, v);
                        if (!tbl.Contains(v))
                            tbl.Add(v);
                    }
                    else
                    {
                        v = f2(res[i]);
                        if ((v < vmax) && !tbl.Contains(v))
                            tbl.Add(v);
                    }
                }
                if (tbl.Count == 0) break;
                istart = res.Count;
                res.AddRange(tbl);
                res = res.Distinct().ToList();
            }

            //res = res.Distinct().ToList();
            res.Sort();
            return res[n];
        }
    }
}
