using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscrapers
{
    /// <summary>
    /// Generator permutacji podanej tablicy w kolejnosci antyleksykograficznej
    /// Na podstawie "Algorytmika praktyczna" rozdział 4.1
    /// </summary>
    public class PermutationGen
    {
        private int[] _tbl;
        private List<int[]> _resList;

        private void SwapInTbl(int x, int y)
        {
            int t = _tbl[x];
            _tbl[x] = _tbl[y];
            _tbl[y] = t;
        }

        private void AddCurrentTblToResult()
        {
            int[] item = new int[_tbl.Length];
            Array.Copy(_tbl, 0, item, 0, _tbl.Length);
            _resList.Add(item);
        }

        private void Perm(int m)
        {
            if (m == 0)
                AddCurrentTblToResult();
            else
            {
                for (int i = 0; i <= m; i++)
                {
                    Perm(m - 1);
                    if (i < m)
                    {
                        SwapInTbl(i, m);
                        Array.Reverse(_tbl, 0, m);
                    }
                }
            }
        }

        public void Gen(int[] tbl, List<int[]> resList)
        {
            _tbl = tbl;
            _resList = resList;
            Perm(_tbl.Length - 1);
        }
    }
}
