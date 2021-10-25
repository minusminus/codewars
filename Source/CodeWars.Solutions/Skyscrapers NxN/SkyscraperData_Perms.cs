using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscrapers
{
    public class SkyscraperData_Perms
    {
        private const int MaxN = 8;
        public static readonly int[] Masks = new int[MaxN + 1] { 0, 1, 1 << 1, 1 << 2, 1 << 3, 1 << 4, 1 << 5, 1 << 6, 1 << 7 };
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

        public readonly int[,] Data;
        public readonly int[] SetInRow;
        public readonly int[] SetInCol;

        public SkyscraperData_Perms(int N)
        {
            Data = new int[N, N];
            SetInRow = new int[N];
            SetInCol = new int[N];
        }

        public int CountBits(int val)
        {
            return ByteBitCount[val];
        }

        public void SetSingleElement(Tuple<int, int> pos, int num)
        {
            SetSingleElementMask(pos.Item1, pos.Item2, SkyscraperData_Perms.Masks[num]);
        }

        public void SetSingleElementMask(int row, int col, int mask)
        {
            Data[row, col] = mask;
            SetInRow[row] |= mask;
            SetInCol[col] |= mask;
        }
    }
}
