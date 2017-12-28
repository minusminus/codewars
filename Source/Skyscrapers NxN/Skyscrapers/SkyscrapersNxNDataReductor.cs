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

        private void TryReduceSingleElement(SkyscraperData d, List<Tuple<int, int>> proc, int row, int col, int mask)
        {
            SkyscrapersCounters.ReduceRCLoops++;
            if (d.CountBits(row, col) > 1)
            {
                SkyscrapersCounters.ReduceRCLoopsRemoves++;
                d.RemoveElementMask(row, col, mask);
                //if ((d.CountBits(row, i) == 1) && (!proc.Any(x => (x.Item1 == row) && (x.Item2 == i))))
                if (d.CountBits(row, col) == 1)
                {
                    SkyscrapersCounters.ReduceRCReductions++;
                    proc.Add(new Tuple<int, int>(row, col));
                }
            }
        }

        private void ReduceRowsCols(SkyscraperData d, List<Tuple<int, int>> proc)
        {
            int iproc = 0;
            while (iproc < proc.Count)
            {
                SkyscrapersCounters.ReduceRCIters++;
                int row = proc[iproc].Item1;
                int col = proc[iproc].Item2;
                int mask = d.Data[row, col] ^ -1;
                for (int i = 0; i < _n; i++)
                {
                    if (i != col) TryReduceSingleElement(d, proc, row, i, mask);
                    if (i != row) TryReduceSingleElement(d, proc, i, col, mask);
                }
                iproc++;
            }
        }

        private bool TrySetSingleElement(SkyscraperData d, List<Tuple<int, int>> proc, int row, int col, int mask)
        {
            int v = (mask ^ -1) & d.Data[row, col]; //zostaja jedynki tam gdzie w masce sa 0
            if (d.CountBits(v) == 1)
            {
                d.Data[row, col] = v;
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

        public void ReduceData(SkyscraperData d, List<Tuple<int, int>> proc)
        {
            while (proc.Count > 0)
            {
                ReduceRowsCols(d, proc);
                proc.Clear();
                SetRowsColsWherePossible(d, proc);
            }
        }
    }
}
