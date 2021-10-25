using System;

namespace GTZ
{
    /// <summary>
    /// math done in solution.tif
    /// </summary>
    public class GTZ
    {
        private double calculate(int n)
        {
            double d = 1.0;
            double res = 1.0;
            for (int i = n; i > 1; i--)
            {
                d *= (1.0 / (double)i);
                res += d;
            }
            return res;
        }

        private double prepareResult(double d)
        {
            return Math.Floor(d * 1000000.0) / 1000000.0;
        }

        public double going(int n)
        {
            return prepareResult(calculate(n));
        }
    }
}
