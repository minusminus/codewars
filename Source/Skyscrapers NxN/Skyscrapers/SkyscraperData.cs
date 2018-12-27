using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscrapers
{
    public class SkyscraperData
    {
        private const int MaxN = 8;
        public static readonly int[] Masks = new int[MaxN + 1] { 0, 1, 1 << 1, 1 << 2, 1 << 3, 1 << 4, 1 << 5, 1 << 6, 1 << 7 };
        public static readonly int[] MasksRev = new int[MaxN + 1] { 0, Masks[1] ^ -1, Masks[2] ^ -1, Masks[3] ^ -1, Masks[4] ^ -1, Masks[5] ^ -1, Masks[6] ^ -1, Masks[7] ^ -1, Masks[8] ^ -1 };
        public static readonly int[] InitialValues = new int[MaxN + 1] { 0, 1, (Masks[2] - 1) | Masks[2], (Masks[3] - 1) | Masks[3], (Masks[4] - 1) | Masks[4], (Masks[5] - 1) | Masks[5], (Masks[6] - 1) | Masks[6], (Masks[7] - 1) | Masks[7], (Masks[8] - 1) | Masks[8] };
        public static readonly int[] ByteBitCount = new int[256]
        {
            0, 1, 1, 2, 1, 2, 2, 3, 1, 2,
            2, 3, 2, 3, 3, 4, 1, 2, 2, 3,
            2, 3, 3, 4, 2, 3, 3, 4, 3, 4,
            4, 5, 1, 2, 2, 3, 2, 3, 3, 4,
            2, 3, 3, 4, 3, 4, 4, 5, 2, 3,
            3, 4, 3, 4, 4, 5, 3, 4, 4, 5,
            4, 5, 5, 6, 1, 2, 2, 3, 2, 3,
            3, 4, 2, 3, 3, 4, 3, 4, 4, 5,
            2, 3, 3, 4, 3, 4, 4, 5, 3, 4,
            4, 5, 4, 5, 5, 6, 2, 3, 3, 4,
            3, 4, 4, 5, 3, 4, 4, 5, 4, 5,
            5, 6, 3, 4, 4, 5, 4, 5, 5, 6,
            4, 5, 5, 6, 5, 6, 6, 7, 1, 2,
            2, 3, 2, 3, 3, 4, 2, 3, 3, 4,
            3, 4, 4, 5, 2, 3, 3, 4, 3, 4,
            4, 5, 3, 4, 4, 5, 4, 5, 5, 6,
            2, 3, 3, 4, 3, 4, 4, 5, 3, 4,
            4, 5, 4, 5, 5, 6, 3, 4, 4, 5,
            4, 5, 5, 6, 4, 5, 5, 6, 5, 6,
            6, 7, 2, 3, 3, 4, 3, 4, 4, 5,
            3, 4, 4, 5, 4, 5, 5, 6, 3, 4,
            4, 5, 4, 5, 5, 6, 4, 5, 5, 6,
            5, 6, 6, 7, 3, 4, 4, 5, 4, 5,
            5, 6, 4, 5, 5, 6, 5, 6, 6, 7,
            4, 5, 5, 6, 5, 6, 6, 7, 5, 6,
            6, 7, 6, 7, 7, 8
        };

        private readonly int _size;
        public readonly int[,] Data;
        public readonly int[] SetInRow;
        public readonly int[] SetInCol;
        public readonly List<int>[] Rows;   //lista elementow w wierszu (kolumn)
        public readonly List<int>[] Cols;   //lista elementow w kolumnie (wierszy)

        public SkyscraperData(int N)
        {
            _size = N;
            Data = new int[N, N];
            SetInRow = new int[N];
            SetInCol = new int[N];
            Rows = new List<int>[N];
            Cols = new List<int>[N];
            for (int i = 0; i < N; i++)
            {
                Rows[i] = new List<int>();
                Cols[i] = new List<int>();
            }
        }

        public SkyscraperData(SkyscraperData obj)
        {
            _size = obj._size;
            Data = (int[,]) obj.Data.Clone();
            SetInRow = (int[]) obj.SetInRow.Clone();
            SetInCol = (int[])obj.SetInCol.Clone();
            Rows = new List<int>[_size];
            Cols = new List<int>[_size];
            for (int i = 0; i < _size; i++)
            {
                Rows[i] = obj.Rows[i].ToList();
                Cols[i] = obj.Cols[i].ToList();
            }
        }

        public SkyscraperData(int N, int[,] tbl)
        {
            _size = N;
            Data = (int[,])tbl.Clone();
            SetInRow = new int[N];
            SetInCol = new int[N];
            Rows = new List<int>[N];
            Cols = new List<int>[N];
            for (int i = 0; i < N; i++)
            {
                Rows[i] = new List<int>();
                Cols[i] = new List<int>();
            }
        }

        public void RotateRight()
        {
            int[,] tmp = new int[_size, _size];
            for(int i = 0; i < _size; i++)
                for (int j = 0; j < _size; j++)
                    tmp[j, _size - i - 1] = Data[i, j];
            Array.Copy(tmp, Data, tmp.Length);
        }

        ///// <summary>
        ///// Zwraca ilosc ustawionych bitow metodą Briana Kernighana, ponieważ ustawionych bitów może być max _n
        ///// https://stackoverflow.com/a/12171691/5912466
        ///// </summary>
        //public int CountBits(int row, int col)
        //{
        //    int value = Data[row, col];
        //    int cnt = 0;
        //    while (value != 0)
        //    {
        //        cnt++;
        //        value &= value - 1;
        //    }
        //    return cnt;
        //}

        public int CountBits(int row, int col)
        {
            return CountBits(Data[row, col]);
        }

        public int CountBits(int val)
        {
            SkyscrapersCounters.CountBits++;
            return ByteBitCount[val];
        }

        public void SetElement(int row, int col, int num)
        {
            Data[row, col] |= SkyscraperData.Masks[num];
        }

        public void RemoveElement(int row, int col, int num)
        {
            RemoveElementMask(row, col, SkyscraperData.MasksRev[num]);
        }
        public void RemoveElementMask(int row, int col, int mask)
        {
            Data[row, col] &= mask;
        }

        public void SetSingleElement(int row, int col, int num)
        {
            SetSingleElementMask(row, col, SkyscraperData.Masks[num]);
        }

        public void SetSingleElementMask(int row, int col, int mask)
        {
            Data[row, col] = mask;
            SetInRow[row] |= mask;
            SetInCol[col] |= mask;
            Rows[row].Remove(col);
            Cols[col].Remove(row);
        }

        public void SetMultiElementMask(int row, int col, int mask)
        {
            Data[row, col] = mask;
        }

    }
}
