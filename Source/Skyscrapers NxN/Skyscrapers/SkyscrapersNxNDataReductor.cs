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

        private bool TryReduceSingleElement(SkyscraperData d, int row, int col, int mask, out bool singleset)
        {
            singleset = false;
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
                    if (!ReduceRowCol(d, row, col)) return false;
                    singleset = true;
                }
            }
            //else if (d.Data[row, col] == (mask ^ -1)) return false;
            return true;
        }

        private bool ReduceRowCol(SkyscraperData d, int row, int col)
        {
            SkyscrapersCounters.ReduceRCReductions++;
            int mask = d.Data[row, col] ^ -1;
            int i = 0;
            while (i < d.Rows[row].Count)
            {
                bool singleset = false;
                if (col != d.Rows[row][i])
                    if (!TryReduceSingleElement(d, row, d.Rows[row][i], mask, out singleset)) return false;
                if (!singleset) i++;
            }
            i = 0;
            while (i < d.Cols[col].Count)
            {
                bool singleset = false;
                if (row != d.Cols[col][i])
                    if (!TryReduceSingleElement(d, d.Cols[col][i], col, mask, out singleset)) return false;
                if (!singleset) i++;
            }
            return true;
        }

        private bool ReduceRowsCols(SkyscraperData d, List<Tuple<int, int>> proc)
        {
            int iproc = 0;
            while (iproc < proc.Count)
            {
                if (!ReduceRowCol(d, proc[iproc].Item1, proc[iproc].Item2)) return false;
                iproc++;
            }
            return true;
        }

        private bool TrySetSingleElement(SkyscraperData d, int row, int col, int mask)
        {
            //int v = (mask ^ -1) & d.Data[row, col]; //zostaja jedynki tam gdzie w masce sa 0
            int v = (mask ^ -1) & (d.Data[row, col] & ((d.SetInRow[row] | d.SetInCol[col]) ^ -1));
            if (d.CountBits(v) == 1)
                //if (((v & d.SetInRow[row]) == 0) && ((v & d.SetInCol[col]) == 0))
            {
                d.SetSingleElementMask(row, col, v);
                if (!ReduceRowCol(d, row, col)) return false;
                return true;
            }
            //if (d.CountBits(v) > 1)
            //{
            //    d.SetMultiElementMask(row, col, v);
            //}
            return false;
        }

        private bool SetRowsColsWherePossible(SkyscraperData d)
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
            //{
            //    int j = 0;
            //    while (j < d.Rows[i].Count)
            //    {
            //        int mask = 0, mask2 = 0;

            //        for (int k = 0; k < d.Rows[i].Count; k++)
            //            if (k != j) mask |= d.Data[i, d.Rows[i][k]];

            //        int col = d.Rows[i][j];
            //        for (int k = 0; k < d.Cols[col].Count; k++)
            //            if (d.Cols[col][k] != i) mask2 |= d.Data[d.Cols[col][k], col];

            //        bool b = TrySetSingleElement(d, i, d.Rows[i][j], mask);
            //        if (!b) b = TrySetSingleElement(d, i, d.Rows[i][j], mask2);
            //        if (!b) j++;
            //    }
            //}

            for (int i = 0; i < _n; i++)
            {
                int j = 0;
                while (j < d.Rows[i].Count)
                {
                    int mask = 0;
                    for (int k = 0; k < d.Rows[i].Count; k++)
                        if (k != j) mask |= d.Data[i, d.Rows[i][k]];
                    if (!TrySetSingleElement(d, i, d.Rows[i][j], mask)) j++;
                }
            }
            for (int i = 0; i < _n; i++)
            {
                int j = 0;
                while (j < d.Cols[i].Count)
                {
                    int mask = 0;
                    for (int k = 0; k < d.Cols[i].Count; k++)
                        if (k != j) mask |= d.Data[d.Cols[i][k], i];
                    if (!TrySetSingleElement(d, d.Cols[i][j], i, mask)) j++;
                }
            }

            return true;
        }

        private void SetRowsColsWherePossibleDoubles(SkyscraperData d, List<Tuple<int, int>> proc)
        {
            //wierszami
            //for (int i = 0; i < _n; i++)
            //{
            //    //listy indeksow elementow dla masek
            //    Dictionary<int, List<int>> rowels = new Dictionary<int, List<int>>();
            //    for (int j = 0; j < _n; j++)
            //    {
            //        //rozbicie elementu (wybranie ustawionych bitow)
            //        List<int> el = new List<int>();
            //        for (int m = 1; m <= _n; m++)
            //            if ((d.Data[i, j] & SkyscraperData.Masks[m]) != 0) el.Add(SkyscraperData.Masks[m]);
            //        //wyznaczenie list z calego wiersza dla wszystkich par danego elementu 
            //        for (int f = 0; f < el.Count - 1; f++)
            //            for (int n = f + 1; n < el.Count; n++)
            //            {
            //                int p = el[f] | el[n];
            //                if (rowels.ContainsKey(p)) continue;
            //                List<int> tl = new List<int>();
            //                for (int k = 0; k < _n; k++)
            //                    if ((d.Data[i, k] & p) == p) tl.Add(k);
            //                rowels.Add(p, tl);
            //            }
            //    }
            //    //analiza par
            //    int removedmask = 0;    //moga pojawic sie pary dla powtarzajacych sie pozycji - mozna tylko raz pozycje usunac
            //    foreach (var rowel in rowels.Where(x => x.Value.Count==2))
            //    {
            //        if ((rowel.Key & removedmask) != 0) continue;
            //        for (int k = 0; k < _n; k++)
            //        {
            //            if (rowel.Value.Contains(k))
            //                d.Data[i, k] = rowel.Key;
            //            else
            //                TryReduceSingleElement(d, proc, i, k, rowel.Key ^ -1);
            //        }
            //        removedmask |= rowel.Key;
            //    }
            //}
        }

        public bool ReduceData(SkyscraperData d, List<Tuple<int, int>> proc)
        {
            if (!ReduceRowsCols(d, proc)) return false;
            if (!SetRowsColsWherePossible(d)) return false;
            //if (!ReduceRowsCols(d, proc)) return false;
            //proc.Clear();
            //SetRowsColsWherePossibleDoubles(d, proc);
            return true;
        }
    }
}
