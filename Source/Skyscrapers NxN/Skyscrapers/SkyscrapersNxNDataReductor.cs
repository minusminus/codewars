using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscrapers
{
    public class SkyscrapersNxNDataReductor
    {
        private readonly int _n;

        public SkyscrapersNxNDataReductor(int N)
        {
            _n = N;
        }

        private bool TryReduceSingleElement(SkyscraperData d, List<Tuple<int, int>> proc, int row, int col, int mask)
        {
            if (d.CountBits(row, col) > 1)
            {
                d.RemoveElementMask(row, col, mask);
                if (d.CountBits(row, col) == 1)
                {
                    if (((d.Data[row, col] & d.SetInRow[row]) != 0) || ((d.Data[row, col] & d.SetInCol[col]) != 0))
                        return false;
                    d.SetSingleElementMask(row, col, d.Data[row, col]);
                    proc.Add(new Tuple<int, int>(row, col));
                }
            }
            return true;
        }

        private bool ReduceRowsCols(SkyscraperData d, List<Tuple<int, int>> proc)
        {
            int iproc = 0;
            while (iproc < proc.Count)
            {
                int row = proc[iproc].Item1;
                int col = proc[iproc].Item2;
                int mask = d.Data[row, col] ^ -1;
                for (int i = 0; i < _n; i++)
                {
                    if (i != col) if (!TryReduceSingleElement(d, proc, row, i, mask)) return false;
                    if (i != row) if (!TryReduceSingleElement(d, proc, i, col, mask)) return false;
                }
                iproc++;
            }
            return true;
        }

        private bool TrySetSingleElement(SkyscraperData d, List<Tuple<int, int>> proc, int row, int col, int mask)
        {
            int v = (mask ^ -1) & d.Data[row, col]; //zostaja jedynki tam gdzie w masce sa 0
            if (d.CountBits(v) == 1)
            {
                //d.Data[row, col] = v;
                d.SetSingleElementMask(row, col, v);
                proc.Add(new Tuple<int, int>(row, col));
                return true;
            }
            return false;
        }

        private void SetRowsColsWherePossible(SkyscraperData d, List<Tuple<int, int>> proc)
        {
            for (int i = 0; i < _n; i++)
                for (int j = 0; j < _n; j++)
                {
                    if (d.CountBits(i, j) == 1) continue;
                    int mask = 0, mask2 = 0;
                    for (int k = 0; k < _n; k++)
                    {
                        if (k != j) mask |= d.Data[i, k];   //pozimo
                        if (k != i) mask2 |= d.Data[k, j];  //pionowo
                    }
                    if (!TrySetSingleElement(d, proc, i, j, mask))
                        TrySetSingleElement(d, proc, i, j, mask2);
                }
        }

        public bool ReduceData(SkyscraperData d, List<Tuple<int, int>> proc)
        {
            while (proc.Count > 0)
            {
                if (!ReduceRowsCols(d, proc)) return false;
                proc.Clear();
                SetRowsColsWherePossible(d, proc);
            }
            return true;
        }
    }
}
