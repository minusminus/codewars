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
        private Action<int[]> _permProcessor;

        private void SwapInTbl(int x, int y)
        {
            int t = _tbl[x];
            _tbl[x] = _tbl[y];
            _tbl[y] = t;
        }

        private void Perm(int m)
        {
            if (m == 0)
                _permProcessor(_tbl);
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

        public void Gen(int[] tbl, Action<int[]> permProcessor)
        {
            _tbl = tbl;
            _permProcessor = permProcessor;
            Perm(_tbl.Length - 1);
        }
    }
}
