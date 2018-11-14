using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscrapers
{
    /// <summary>
    /// rozwiazanie z wykorzystaniem list wszystkich permutacji N elementowego zbioru
    /// </summary>
    public class SkyscrapersNxN_Perms
    {
        private readonly int _n;
        private readonly SkyscrapersPrecalcData _precalc;
        List<int[]>[] _lists;  //tablica list

        public SkyscrapersNxN_Perms(int n, SkyscrapersPrecalcData precalc)
        {
            _n = n;
            _precalc = precalc;
        }

        public int[][] Solve(int[] constraints)
        {
            _lists = new List<int[]>[_n];  //tablica list zainicjalizowana nulami
            for (int i = 0; i < _lists.Length; i++) _lists[i] = null;

            //przetworzenie list dla wszystkich ograniczen
            for (int i = 0; i < _n; i++)
            {
                if (constraints[i] == 0) continue;
                AddList(i, constraints[i]);
            }


            _lists = null;
            return new int[_n][];
        }

        private void AddList(int index, int visibility)
        {
            List<int[]> l = _precalc.GetList(visibility);
            _lists[index] = l;

            //int removedcnt = l.RemoveAll(x => x[0] == 0);

            //1. usunac z nowej listy wszystkie niepasujace do biezacych list
            //2. usunac z biezacych to co nie pasuje do nowej listy
            //3. usuwac z pionowych to co nie pasuje do poziomych i z poziomych to co nie pasuje do pionowych do momentu az nic nie zostanie usuniete
        }

        private bool[] GetAllItemsAtPosition(List<int[]> list, int pos )
        {
            bool[] res = new bool[_n];
            for (int i = 0; i < _n; i++) res[i] = false;

            foreach (int[] tbl in list)
                res[tbl[pos] - 1] = true;

            return res;
        }

        private void PrepareList(int index)
        {
            List<int[]> l = _lists[index];
            int side = index/_n;
            int sidepos = index%_n;

            int startrange = 0;
            if ((side & 1) == 0) startrange = _n;
            for (int i = startrange; i < startrange + _n; i++)
            {
                if (_lists[i] == null) continue;
                bool[] toremove = GetAllItemsAtPosition(_lists[i], 0);
            }
        }

    }
}
