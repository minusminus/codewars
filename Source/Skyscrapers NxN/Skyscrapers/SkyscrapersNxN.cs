using System;
using System.Collections.Generic;
using System.Globalization;
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

        private bool CheckDataElements(SkyscraperData d)
        {
            for (int i = 0; i < _n; i++)
            {
                int mask = 0;
                int mask2 = 0;
                for (int j = 0; j < _n; j++)
                {
                    mask |= d.Data[i, j];
                    mask2 |= d.Data[j, i];
                }
                if (mask != SkyscraperData.InitialValues[_n]) return false;
                if (mask2 != SkyscraperData.InitialValues[_n]) return false;
            }
            return true;
        }

        private bool CheckDataConstraints(SkyscraperData d, int[] constraints)
        {
            for (int i = 0; i < _n; i++)
            {
                //poziomo
                int v = 1;
                int highest = d.Data[i, 0];
                for (int j = 0; j < _n; j++)
                    if (d.Data[i, j] > highest)
                    {
                        v++;
                        highest = d.Data[i, j];
                    }
                if ((constraints[4 * _n - 1 - i] != 0) && (constraints[4 * _n - 1 - i] != v)) return false;
                v = 1;
                highest = d.Data[i, _n - 1];
                for (int j = _n - 1; j >= 0; j--)
                    if (d.Data[i, j] > highest)
                    {
                        v++;
                        highest = d.Data[i, j];
                    }
                if ((constraints[_n + i] != 0) && (constraints[_n + i] != v)) return false;
                //pionowo
                v = 1;
                highest = d.Data[0, i];
                for (int j = 0; j < _n; j++)
                    if (d.Data[j, i] > highest)
                    {
                        v++;
                        highest = d.Data[j, i];
                    }
                if ((constraints[i] != 0) && (constraints[i] != v)) return false;
                v = 1;
                highest = d.Data[_n - 1, i];
                for (int j = _n - 1; j >= 0; j--)
                    if (d.Data[j, i] > highest)
                    {
                        v++;
                        highest = d.Data[j, i];
                    }
                if ((constraints[3 * _n - 1 - i] != 0) && (constraints[3 * _n - 1 - i] != v)) return false;
            }
            return true;
        }

        private bool CheckDataCorrect(SkyscraperData d, int[] constraints)
        {
            if (!CheckDataReduced(d)) return false;
            if (!CheckDataElements(d)) return false;
            return CheckDataConstraints(d, constraints);
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
                    if (d.CountBits(row, i) > 1)
                    {
                        d.RemoveElementMask(row, i, mask);
                        if ((d.CountBits(row, i) == 1) && (!proc.Any(x => (x.Item1 == row) && (x.Item2 == i))))
                            proc.Add(new Tuple<int, int>(row, i));
                    }
                }
                for (int i = 0; i < _n; i++)
                {
                    if (i == row) continue;
                    if (d.CountBits(i, col) > 1)
                    {
                        d.RemoveElementMask(i, col, mask);
                        if ((d.CountBits(i, col) == 1) && (!proc.Any(x => (x.Item1 == i) && (x.Item2 == col))))
                            proc.Add(new Tuple<int, int>(i, col));
                    }
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

        public SkyscraperData FindSolution(SkyscraperData d, int[] constraints)
        {
            if (CheckDataCorrect(d, constraints)) return d;

            List<Tuple<int, Tuple<int,int>>> cntList = new List<Tuple<int, Tuple<int, int>>>();
            for(int i=0; i<_n; i++)
                for (int j = 0; j < _n; j++)
                {
                    int bits = d.CountBits(i, j);
                    if (bits > 1)
                        cntList.Add(new Tuple<int, Tuple<int, int>>(bits, new Tuple<int, int>(i, j)));
                }
            cntList.Sort((x, y) => x.Item1.CompareTo(y.Item1));

            for (int i = 0; i < cntList.Count - 1; i++)
            {
                int row = cntList[i].Item2.Item1;
                int col = cntList[i].Item2.Item2;
                int el = d.Data[row, col];
                for (int m = 1; m <= _n; m++)
                    if ((el & SkyscraperData.Masks[m]) != 0)
                    {
                        SkyscraperData newd = new SkyscraperData(d);
                        newd.Data[row, col] = SkyscraperData.Masks[m];
                        List<Tuple<int, int>> proc = new List<Tuple<int, int>>() {new Tuple<int, int>(row, col)};
                        ReduceRowsCols(newd, proc);
                        //if (CheckDataCorrect(newd, constraints)) return newd;
                        newd = FindSolution(newd, constraints);
                        if (newd != null) return newd;
                    }
            }

            return null;
        }

        public int[][] Solve(int[] constraints)
        {
            SkyscraperData d = CreateInitialData();
            ApplyConstraints(d, constraints);

            SkyscraperData dres = FindSolution(d, constraints);
            if (dres == null) throw new Exception("dres == null");

            int[][] res = new int[_n][];
            for (int i = 0; i < _n; i++)
            {
                res[i]=new int[_n];
                for (int j = 0; j < _n; j++)
                    res[i][j] = Array.IndexOf(SkyscraperData.Masks, dres.Data[i, j]);
            }
            return res;
        }
    }
}
