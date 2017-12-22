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
        private const int MaxN = 9;
        private static readonly int[] Masks = new int[MaxN + 1] { 0, 1, 1 << 1, 1 << 2, 1 << 3, 1 << 4, 1 << 5, 1 << 6, 1 << 7, 1 << 8 };
        private static readonly int[] MasksRev = new int[MaxN + 1] { 0, Masks[1] ^ -1, Masks[2] ^ -1, Masks[3] ^ -1, Masks[4] ^ -1, Masks[5] ^ -1, Masks[6] ^ -1, Masks[7] ^ -1, Masks[8] ^ -1, Masks[9] ^ -1 };

        private readonly int _initValue;
        private readonly int _n;

        public SkyscrapersNxN(int N)
        {
            _n = N;
            _initValue = GenerateInitValue(N);
        }

        private int GenerateInitValue(int N)
        {
            int res = 0;
            for (int i = 0; i < N; i++)
                res |= Masks[i+1];
            return res;
        }

        /// <summary>
        /// Zwraca ilosc ustawionych bitow metodą Briana Kernighana, ponieważ ustawionych bitów może być max _n
        /// https://stackoverflow.com/a/12171691/5912466
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int CountBits(int value)
        {
            int cnt = 0;
            while (value != 0)
            {
                cnt++;
                value &= value - 1;
            }
            return cnt;
        }

        private int SetElement(int val, int num)
        {
            return val | Masks[num];
        }

        private int RemoveElement(int val, int num)
        {
            return val & MasksRev[num];
        }

        private SkyscraperData CreateInitialData()
        {
            SkyscraperData d = new SkyscraperData(_n);
            for(int i=0; i<_n; i++)
                for (int j = 0; j < _n; j++)
                    d.Data[i, j] = _initValue;
            return d;
        }

        private bool CheckDataReduced(SkyscraperData d)
        {
            for (int i = 0; i < _n; i++)
                for (int j = 0; j < _n; j++)
                    if (CountBits(d.Data[i, j]) != 1) return false;
            return true;
        }

        private void ApplyConstraints(SkyscraperData d, int[] constraints)
        {
            for(int i=0; i<constraints.Length; i++)
                if (constraints[i] > 0)
                {
                    int section = i/4;
                    //narazie dla sekcji 3 (od lewej do prawej)
                    int row = i%4;
                    if (constraints[i] == _n)
                    {
                        for (int k = 0; k < _n; k++)
                            d.Data[row, k] = Masks[_n - k];
                    }
                    else if (constraints[i] == 1)
                    {
                        d.Data[row, _n - 1] = Masks[_n];
                    }
                    else
                    {
                        
                    }
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
