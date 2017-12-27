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
        private const int MaxN = 9;
        public static readonly int[] Masks = new int[MaxN + 1] { 0, 1, 1 << 1, 1 << 2, 1 << 3, 1 << 4, 1 << 5, 1 << 6, 1 << 7, 1 << 8 };
        public static readonly int[] MasksRev = new int[MaxN + 1] { 0, Masks[1] ^ -1, Masks[2] ^ -1, Masks[3] ^ -1, Masks[4] ^ -1, Masks[5] ^ -1, Masks[6] ^ -1, Masks[7] ^ -1, Masks[8] ^ -1, Masks[9] ^ -1 };
        public static readonly int[] InitialValues = new int[MaxN + 1] { 0, 1, (Masks[2] - 1) | Masks[2], (Masks[3] - 1) | Masks[3], (Masks[4] - 1) | Masks[4], (Masks[5] - 1) | Masks[5], (Masks[6] - 1) | Masks[6], (Masks[7] - 1) | Masks[7], (Masks[8] - 1) | Masks[8], (Masks[9] - 1) | Masks[9] };

        private readonly int _size;
        public readonly int[,] Data;

        public SkyscraperData(int N)
        {
            _size = N;
            Data = new int[N, N];
        }

        public SkyscraperData(SkyscraperData obj)
        {
            _size = obj._size;
            Data = (int[,]) obj.Data.Clone();
        }

        public SkyscraperData(int N, int[,] tbl)
        {
            _size = N;
            Data = (int[,])tbl.Clone();
        }

        public void RotateRight()
        {
            int[,] tmp = new int[_size, _size];
            for(int i = 0; i < _size; i++)
                for (int j = 0; j < _size; j++)
                    tmp[j, _size - i - 1] = Data[i, j];
            Array.Copy(tmp, Data, tmp.Length);
        }

        /// <summary>
        /// Zwraca ilosc ustawionych bitow metodą Briana Kernighana, ponieważ ustawionych bitów może być max _n
        /// https://stackoverflow.com/a/12171691/5912466
        /// </summary>
        public int CountBits(int row, int col)
        {
            int value = Data[row, col];
            int cnt = 0;
            while (value != 0)
            {
                cnt++;
                value &= value - 1;
            }
            return cnt;
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
    }
}
