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
    /// ograniczenie N do 8
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
            for (int i = 0; i < _n; i++)
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
                if ((mask != SkyscraperData.InitialValues[_n]) || (mask2 != SkyscraperData.InitialValues[_n]))
                    return false;
            }
            return true;
        }

        private void CheckDataConstaintsSingleCheck(ref int v, ref int highest, int dataval)
        {
            if (dataval > highest)
            {
                v++;
                highest = dataval;
            }
        }

        private bool CheckDataConstraints(SkyscraperData d, int[] constraints)
        {
            int v;
            int highest;
            for (int i = 0; i < _n; i++)
            {
                //poziomo
                if (constraints[4*_n - 1 - i] != 0)
                {
                    v = 1;
                    highest = d.Data[i, 0];
                    for (int j = 0; j < _n; j++)
                        CheckDataConstaintsSingleCheck(ref v, ref highest, d.Data[i, j]);
                    if (constraints[4*_n - 1 - i] != v) return false;
                }
                if (constraints[_n + i] != 0)
                {
                    v = 1;
                    highest = d.Data[i, _n - 1];
                    for (int j = _n - 1; j >= 0; j--)
                        CheckDataConstaintsSingleCheck(ref v, ref highest, d.Data[i, j]);
                    if (constraints[_n + i] != v) return false;
                }
                //pionowo
                if (constraints[i] != 0)
                {
                    v = 1;
                    highest = d.Data[0, i];
                    for (int j = 0; j < _n; j++)
                        CheckDataConstaintsSingleCheck(ref v, ref highest, d.Data[j, i]);
                    if (constraints[i] != v) return false;
                }
                if (constraints[3*_n - 1 - i] != 0)
                {
                    v = 1;
                    highest = d.Data[_n - 1, i];
                    for (int j = _n - 1; j >= 0; j--)
                        CheckDataConstaintsSingleCheck(ref v, ref highest, d.Data[j, i]);
                    if (constraints[3*_n - 1 - i] != v) return false;
                }
            }
            return true;
        }

        private bool CheckDataCorrect(SkyscraperData d, int[] constraints)
        {
            if (!CheckDataReduced(d)) return false;
            if (!CheckDataElements(d)) return false;
            return CheckDataConstraints(d, constraints);
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
                    if (i == col) continue;
                    TryReduceSingleElement(d, proc, row, i, mask);
                }
                for (int i = 0; i < _n; i++)
                {
                    if (i == row) continue;
                    TryReduceSingleElement(d, proc, i, col, mask);
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
                        TrySetSingleElement(d, proc, i, j, mask);
                }
        }

        private void ReduceData(SkyscraperData d, List<Tuple<int, int>> proc)
        {
            while (proc.Count > 0)
            {
                ReduceRowsCols(d, proc);
                proc.Clear();
                SetRowsColsWherePossible(d, proc);
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
            ReduceData(d, proc);
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

            for (int i = 0; i < cntList.Count; i++)
            {
                int row = cntList[i].Item2.Item1;
                int col = cntList[i].Item2.Item2;
                int el = d.Data[row, col];
                for (int m = 1; m <= _n; m++)
                    if ((el & SkyscraperData.Masks[m]) != 0)
                    {
                        SkyscrapersCounters.NewData++;
                        SkyscraperData newd = new SkyscraperData(d);
                        newd.Data[row, col] = SkyscraperData.Masks[m];
                        List<Tuple<int, int>> proc = new List<Tuple<int, int>>() {new Tuple<int, int>(row, col)};
                        ReduceData(newd, proc);
                        SkyscraperData nextd = FindSolution(newd, constraints);
                        if (nextd != null) return nextd;
                    }
            }

            return null;
        }

        public int[][] Solve(int[] constraints)
        {
            Console.WriteLine("***");
            SkyscrapersCounters.Clear();
            SkyscraperData d = CreateInitialData();
            ApplyConstraints(d, constraints);

            SkyscraperData dres = FindSolution(d, constraints);

            Console.WriteLine($"NewData: {SkyscrapersCounters.NewData}");
            Console.WriteLine($"ReduceRCIters: {SkyscrapersCounters.ReduceRCIters}");
            Console.WriteLine($"ReduceRCLoops: {SkyscrapersCounters.ReduceRCLoops}");
            Console.WriteLine($"ReduceRCLoopsRemoves: {SkyscrapersCounters.ReduceRCLoopsRemoves}");
            Console.WriteLine($"ReduceRCReductions: {SkyscrapersCounters.ReduceRCReductions}");
            Console.WriteLine($"CheckDataCorrectCalls: {SkyscrapersCounters.CheckDataCorrectCalls}");
            Console.WriteLine($"CorrectData: {SkyscrapersCounters.CorrectData}");
            Console.WriteLine($"FirstCorrectDataInCall: {SkyscrapersCounters.FirstCorrectDataInCall}");

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
