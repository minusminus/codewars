using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscrapers
{
    /// <summary>
    /// rozwiazanie na podstawie: http://norvig.com/sudoku.html
    /// 
    /// ograniczenie N do 9
    /// </summary>
    class SkyscrapersNxN
    {
        private readonly string _initString;
        private readonly int _n;

        public SkyscrapersNxN(int N)
        {
            _n = N;
            _initString = "123456789".Substring(0, N);
        }

        private SkyscraperData CreateInitialData()
        {
            SkyscraperData d = new SkyscraperData(_n);
            for(int i=0; i<_n; i++)
                for (int j = 0; j < _n; j++)
                    d.Data[i, j] = _initString;
            return d;
        }

        private bool CheckDataReduced(SkyscraperData d)
        {
            for (int i = 0; i < _n; i++)
                for (int j = 0; j < _n; j++)
                    if (d.Data[i, j].Length != 1) return false;
            return true;
        }

        private void ApplyConstraints(SkyscraperData d, int[] constraints)
        {
            for(int i=0; i<constraints.Length; i++)
                if (constraints[i] > 0)
                {
                    
                }
        }

        public int[][] Solve(int[] constraints)
        {
            SkyscraperData d = CreateInitialData();
            ApplyConstraints(d, constraints);

            return null;
        }
    }
}
