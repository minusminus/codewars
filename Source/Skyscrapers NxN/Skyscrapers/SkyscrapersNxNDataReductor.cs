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
                    {
                        SkyscrapersCounters.ReduceRCDoublesCut++;
                        return false;
                    }
                    d.SetSingleElementMask(row, col, d.Data[row, col]);
                    proc.Add(new Tuple<int, int>(row, col));
                }
            }
            //else if (d.Data[row, col] == (mask ^ -1)) return false;
            return true;
        }

        private bool ReduceRowsCols(SkyscraperData d, List<Tuple<int, int>> proc)
        {
            int iproc = 0;
            while (iproc < proc.Count)
            {
                SkyscrapersCounters.ReduceRCReductions++;
                int row = proc[iproc].Item1;
                int col = proc[iproc].Item2;
                int mask = d.Data[row, col] ^ -1;
                //for (int i = 0; i < _n; i++)
                //{
                //    if (i != col) if (!TryReduceSingleElement(d, proc, row, i, mask)) return false;
                //    if (i != row) if (!TryReduceSingleElement(d, proc, i, col, mask)) return false;
                //}
                if (d.SetInRow[row] != SkyscraperData.InitialValues[_n])
                    for (int i = 0; i < _n; i++)
                    {
                        if (i != col) if (!TryReduceSingleElement(d, proc, row, i, mask)) return false;
                    }
                if (d.SetInCol[col] != SkyscraperData.InitialValues[_n])
                    for (int i = 0; i < _n; i++)
                    {
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
            //for (int i = 0; i < _n; i++)
            //    for (int j = 0; j < _n; j++)
            //    {
            //        if (d.CountBits(i, j) == 1) continue;
            //        int mask = 0, mask2 = 0;
            //        for (int k = 0; k < _n; k++)
            //        {
            //            if (k != j) mask |= d.Data[i, k];   //pozimo
            //            if (k != i) mask2 |= d.Data[k, j];  //pionowo
            //        }
            //        if (!TrySetSingleElement(d, proc, i, j, mask))
            //            TrySetSingleElement(d, proc, i, j, mask2);
            //        //bool b = TrySetSingleElement(d, proc, i, j, mask);
            //        //if (!b) b = TrySetSingleElement(d, proc, i, j, mask2);
            //        //if (b)
            //        //{
            //        //    ReduceRowsCols(d, proc);
            //        //    proc.Clear();
            //        //}
            //    }
            //return;

            //prekalkulowane maski dla danego pola, najpierw od lewej potem od prawej, co daje n^2 zamianst n^3
            int[] precalcmasks = new int[_n];
            for (int i = 0; i < _n; i++)
            {
                if (d.SetInRow[i] == SkyscraperData.InitialValues[_n]) continue;
                //od lewej
                precalcmasks[0] = 0;
                for (int j = 1; j < _n; j++)
                    precalcmasks[j] = precalcmasks[j - 1] | d.Data[i, j - 1];
                //i od prawej
                int tmpright = 0;//d.Data[i, _n - 1];
                for (int j = _n - 2; j >= 0; j--)
                {
                    tmpright |= d.Data[i, j + 1];
                    precalcmasks[j] |= tmpright;
                }
                //i przetworzenie wiersza
                for (int j = 1; j < _n; j++)
                {
                    if (d.CountBits(i, j) == 1) continue;
                    TrySetSingleElement(d, proc, i, j, precalcmasks[j]);
                    //if (TrySetSingleElement(d, proc, i, j, precalcmasks[j]))
                    //{
                    //    ReduceRowsCols(d, proc);
                    //    proc.Clear();
                    //}
                }
            }
            for (int i = 0; i < _n; i++)
            {
                if (d.SetInCol[i] == SkyscraperData.InitialValues[_n]) continue;
                //od lewej
                precalcmasks[0] = 0;
                for (int j = 1; j < _n; j++)
                    precalcmasks[j] = precalcmasks[j - 1] | d.Data[j - 1, i];
                //i od prawej
                int tmpright = 0;//d.Data[_n - 1, i];
                for (int j = _n - 2; j >= 0; j--)
                {
                    tmpright |= d.Data[j + 1, i];
                    precalcmasks[j] |= tmpright;
                }
                //i przetworzenie wiersza
                for (int j = 1; j < _n; j++)
                {
                    if (d.CountBits(j, i) == 1) continue;
                    TrySetSingleElement(d, proc, j, i, precalcmasks[j]);
                    //if (TrySetSingleElement(d, proc, j, i, precalcmasks[j]))
                    //{
                    //    ReduceRowsCols(d, proc);
                    //    proc.Clear();
                    //}
                }
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
