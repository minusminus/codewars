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
    public class SkyscrapersNxN
    {
        private readonly int _n;

        public SkyscrapersNxN(int N)
        {
            _n = N;
        }

        public SkyscraperData CreateInitialData()
        {
            SkyscraperData d = new SkyscraperData(_n);
            for(int i=0; i<_n; i++)
                for (int j = 0; j < _n; j++)
                    d.Data[i, j] = SkyscraperData.InitialValues[_n];
            return d;
        }

        private bool CheckDataReduced(SkyscraperData d)
        {
            for (int i = 0; i < _n; i++)
                for (int j = 0; j < _n; j++)
                    if (d.CountBits(i, j) != 1) return false;
            return true;
        }

        private void ReduceRowsCols(SkyscraperData d, List<Tuple<int, int>> proc)
        {
            int iproc = 0;
            while (iproc < proc.Count)
            {
                int row = proc[iproc].Item1;
                int col = proc[iproc].Item2;
                int mask = d.Data[row, col] ^ -1;
                for (int i = 0; i < _n; i++)
                {
                    if (i == col) continue;
                    d.RemoveElementMask(row, i, mask);
                    if ((d.CountBits(row, i) == 1) && (proc.Any(x => (x.Item1 == row) && (x.Item2 == i))))
                        proc.Add(new Tuple<int, int>(row, i));
                }
                for (int i = 0; i < _n; i++)
                {
                    if (i == row) continue;
                    d.RemoveElementMask(i, col, mask);
                    if ((d.CountBits(row, i) == 1) && (proc.Any(x => (x.Item1 == i) && (x.Item2 == col))))
                        proc.Add(new Tuple<int, int>(i, col));
                }
                iproc++;
            }
        }

        public void ApplyConstraints(SkyscraperData d, int[] constraints)
        {
            //ograniczenia od lewej do prawej po N rzedow
            //po przejsciu N rzedow obrot tablicy w prawo i analiza kolejnych rzedow
            for (int i = constraints.Length - 1; i >= 0; i--)
            {
                if (constraints[i] > 0)
                {
                    int row = _n - 1 - i%_n;
                    if (constraints[i] == _n)
                    {
                        for (int k = 0; k < _n; k++)
                            d.Data[row, k] = SkyscraperData.Masks[k + 1];
                    }
                    else if (constraints[i] == 1)
                    {
                        d.Data[row, 0] = SkyscraperData.Masks[_n];
                    }
                    else
                    {
                        for(int el=_n; el>1; el--)
                            for (int k = 0; k < constraints[i] - 1 - (_n - el); k++)
                                d.RemoveElement(row, k, el);
                    }
                }
                if (i%_n == 0)
                    d.RotateRight();
            }
            //po nalozeniu ograniczen uwzglednienie wszystkich powstalych pozycji jednoelementowych (redukcja w wierszach i kolumnach)
            List<Tuple<int, int>> proc = new List<Tuple<int, int>>();
            for(int i=0; i<_n; i++)
                for(int j=0; j<_n; j++)
                    if (d.CountBits(i, j) == 1)
                        proc.Add(new Tuple<int, int>(i, j));
            ReduceRowsCols(d, proc);
        }

        public int[][] Solve(int[] constraints)
        {
            SkyscraperData d = CreateInitialData();
            ApplyConstraints(d, constraints);


            //if(!CheckDataReduced(d)) throw new Exception("Data not reduced");
            int[][] res = new int[_n][];
            for (int i = 0; i < _n; i++)
            {
                res[i]=new int[_n];
                for (int j = 0; j < _n; j++)
                    res[i][j] = Array.IndexOf(SkyscraperData.Masks, d.Data[i, j]);
            }
            return res;
        }
    }
}
