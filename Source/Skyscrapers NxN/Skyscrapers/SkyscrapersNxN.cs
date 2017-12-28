﻿using System;
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
        private readonly SkyscrapersNxNDataValidator _dataValidator;
        private readonly SkyscrapersNxNDataReductor _dataReductor;

        public SkyscrapersNxN(int N)
        {
            _n = N;
            _dataValidator = new SkyscrapersNxNDataValidator(N);
            _dataReductor=new SkyscrapersNxNDataReductor(N);
        }

        public SkyscraperData CreateInitialData()
        {
            SkyscraperData d = new SkyscraperData(_n);
            for (int i = 0; i < _n; i++)
                for (int j = 0; j < _n; j++)
                    d.Data[i, j] = SkyscraperData.InitialValues[_n];
            return d;
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
            _dataReductor.ReduceData(d, proc);
        }

        public SkyscraperData FindSolution(SkyscraperData d, int[] constraints)
        {
            if (_dataValidator.CheckDataCorrect(d, constraints)) return d;

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
                        _dataReductor.ReduceData(newd, proc);
                        SkyscraperData nextd = FindSolution(newd, constraints);
                        if (nextd != null) return nextd;
                    }
            }

            return null;
        }

        public int[][] Solve(int[] constraints)
        {
            //Console.WriteLine("***");
            //SkyscrapersCounters.Clear();
            SkyscraperData d = CreateInitialData();
            ApplyConstraints(d, constraints);

            SkyscraperData dres = FindSolution(d, constraints);

            //Console.WriteLine($"NewData: {SkyscrapersCounters.NewData}");
            //Console.WriteLine($"ReduceRCIters: {SkyscrapersCounters.ReduceRCIters}");
            //Console.WriteLine($"ReduceRCLoops: {SkyscrapersCounters.ReduceRCLoops}");
            //Console.WriteLine($"ReduceRCLoopsRemoves: {SkyscrapersCounters.ReduceRCLoopsRemoves}");
            //Console.WriteLine($"ReduceRCReductions: {SkyscrapersCounters.ReduceRCReductions}");
            //Console.WriteLine($"CheckDataCorrectCalls: {SkyscrapersCounters.CheckDataCorrectCalls}");
            //Console.WriteLine($"CorrectData: {SkyscrapersCounters.CorrectData}");
            //Console.WriteLine($"FirstCorrectDataInCall: {SkyscrapersCounters.FirstCorrectDataInCall}");

            if (dres == null) return null;  //throw new Exception("dres == null");

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
