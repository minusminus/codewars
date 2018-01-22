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
                //ReduceRowsCols(d, proc);
                //proc.Clear();
                return true;
            }
            return false;
        }

        private void SetRowsColsWherePossible(SkyscraperData d, List<Tuple<int, int>> proc)
        {
            SkyscrapersCounters.SetRowsCols++;

            //prekalkulowane maski dla danego pola, najpierw od lewej potem od prawej, co daje n^2 zamianst n^3
            //int[,] masksrow = new int[_n, _n];
            //int[,] maskscol = new int[_n, _n];
            //for (int i = 0; i < _n; i++)
            //{
            //    //od lewej
            //    masksrow[i, 0] = 0;
            //    maskscol[i, 0] = 0;
            //    if (d.SetInRow[i] != SkyscraperData.InitialValues[_n])
            //        for (int j = 1; j < _n; j++)
            //            masksrow[i, j] = masksrow[i, j - 1] | d.Data[i, j - 1];
            //    if (d.SetInCol[i] != SkyscraperData.InitialValues[_n])
            //        for (int j = 1; j < _n; j++)
            //            maskscol[i, j] = maskscol[i, j - 1] | d.Data[j - 1, i];
            //    //i od prawej
            //    int tmprightrow = 0, tmprightcol = 0;
            //    if (d.SetInRow[i] != SkyscraperData.InitialValues[_n])
            //        for (int j = _n - 2; j >= 0; j--)
            //        {
            //            tmprightrow |= d.Data[i, j + 1];
            //            masksrow[i, j] |= tmprightrow;
            //        }
            //    if (d.SetInCol[i] != SkyscraperData.InitialValues[_n])
            //        for (int j = _n - 2; j >= 0; j--)
            //        {
            //            tmprightcol |= d.Data[j + 1, i];
            //            maskscol[i, j] |= tmprightcol;
            //        }
            //}

            //for (int i = 0; i < _n; i++)
            //    if (d.SetInRow[i] != SkyscraperData.InitialValues[_n])
            //        for (int j = 0; j < _n; j++)
            //        {
            //            if (d.CountBits(i, j) == 1) continue;
            //            int mask = masksrow[i, j];
            //            int mask2 = maskscol[j, i];
            //            if (!TrySetSingleElement(d, proc, i, j, mask))
            //                TrySetSingleElement(d, proc, i, j, mask2);
            //            //TrySetSingleElement(d, proc, i, j, mask);
            //            //TrySetSingleElement(d, proc, i, j, mask2);
            //        }

            //for (int i = 0; i < _n; i++)
            //    if (d.SetInRow[i] != SkyscraperData.InitialValues[_n])
            //        for (int j = 0; j < _n; j++)
            //        {
            //            if (d.CountBits(i, j) == 1) continue;
            //            int mask = 0, mask2 = 0;
            //            for (int k = 0; k < _n; k++)
            //            {
            //                if (k != j) mask |= d.Data[i, k]; //pozimo
            //                if (k != i) mask2 |= d.Data[k, j]; //pionowo
            //            }
            //            if (!TrySetSingleElement(d, proc, i, j, mask))
            //                TrySetSingleElement(d, proc, i, j, mask2);
            //        }

            for (int i = 0; i < _n; i++)
                if (d.SetInRow[i] != SkyscraperData.InitialValues[_n])
                    for (int j = 0; j < _n; j++)
                    {
                        if (d.CountBits(i, j) == 1) continue;
                        int mask = 0, mask2 = 0;
                        for (int k = 0; k < _n; k++)
                        {
                            if (k != j) mask |= d.Data[i, k]; //pozimo
                            if (k != i) mask2 |= d.Data[k, j]; //pionowo
                        }
                        if (!TrySetSingleElement(d, proc, i, j, mask))
                            TrySetSingleElement(d, proc, i, j, mask2);
                    }
        }

        private void SetRowsColsWherePossibleDoubles(SkyscraperData d, List<Tuple<int, int>> proc)
        {
            //wierszami
            for (int i = 0; i < _n; i++)
            {
                //listy indeksow elementow dla masek
                Dictionary<int, List<int>> rowels = new Dictionary<int, List<int>>();
                for (int j = 0; j < _n; j++)
                {
                    //rozbicie elementu (wybranie ustawionych bitow)
                    List<int> el = new List<int>();
                    for (int m = 1; m <= _n; m++)
                        if ((d.Data[i, j] & SkyscraperData.Masks[m]) != 0) el.Add(SkyscraperData.Masks[m]);
                    //wyznaczenie list z calego wiersza dla wszystkich par danego elementu 
                    for (int f = 0; f < el.Count - 1; f++)
                        for (int n = f + 1; n < el.Count; n++)
                        {
                            int p = el[f] | el[n];
                            if (rowels.ContainsKey(p)) continue;
                            List<int> tl = new List<int>();
                            for (int k = 0; k < _n; k++)
                                if ((d.Data[i, k] & p) == p) tl.Add(k);
                            rowels.Add(p, tl);
                        }
                }
                //analiza par
                int removedmask = 0;    //moga pojawic sie pary dla powtarzajacych sie pozycji - mozna tylko raz pozycje usunac
                foreach (var rowel in rowels.Where(x => x.Value.Count==2))
                {
                    if ((rowel.Key & removedmask) != 0) continue;
                    for (int k = 0; k < _n; k++)
                    {
                        if (rowel.Value.Contains(k))
                            d.Data[i, k] = rowel.Key;
                        else
                            TryReduceSingleElement(d, proc, i, k, rowel.Key ^ -1);
                    }
                    removedmask |= rowel.Key;
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
                //if (!ReduceRowsCols(d, proc)) return false;
                //proc.Clear();
                //SetRowsColsWherePossibleDoubles(d, proc);
            }
            return true;
        }
    }
}
