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
            _lists = new List<int[]>[4*_n];  //tablica list zainicjalizowana nulami

            //zainicjalizowanie list dla wszystkich ograniczen
            for (int i = 0; i < _lists.Length; i++)
                if (constraints[i] != 0)
                    _lists[i] = _precalc.GetList(constraints[i]);

            //redukcja w poziomie i pionie wszystkich list
            int deleted = 1;
            while (deleted > 0)
            {
                deleted = ReduceHorizontal();
                deleted += ReduceVertical();
            }

            //wygenerowanie wynikowej tabeli

            _lists = null;
            return new int[_n][];
        }


        private List<int>[] GetAllowedItems(List<int[]> list, bool backwards)
        {
            bool[][] existing = new bool[_n][];
            for (int i = 0; i < _n; i++)
                existing[i] = new bool[_n];

            foreach (int[] tbl in list)
                for (int i = 0; i < _n; i++)
                    existing[i][tbl[i] - 1] = true;

            List<int>[] res = new List<int>[_n];
            for (int i = 0; i < _n; i++)
            {
                List<int> l = new List<int>();
                if (backwards)
                    res[_n - i - 1] = l;
                else
                    res[i] = l;
                for (int j = 0; j < _n; j++)
                    if (existing[i][j])
                        l.Add(j + 1);
            }
            return res;
        }

        private bool CheckIfPermToRemove(int[] tbl, int pos, List<int>[][] allowedTop, List<int>[][] allowedBottom)
        {
            for (int i = 0; i < _n; i++)
                if (((allowedTop[i] != null) && (!allowedTop[i][pos].Contains(tbl[i])))
                    || ((allowedBottom[i] != null) && (!allowedBottom[i][pos].Contains(tbl[i]))))
                    return true;
            return false;
        }

        private int ReduceHorizontal()
        {
            //redukcja list poziomych (z prawej i lewej strony)

            //dla list gornych wyznaczamy macierz dopuszczalnych elementow na kazdej pozycji
            //dla dolnych analogicznie tylko z odwroconym indeksem
            List<int>[][] allowedTop = new List<int>[_n][];
            List<int>[][] allowedBottom = new List<int>[_n][];
            for (int i = 0; i < _n; i++)
                if (_lists[i] != null)
                    allowedTop[i] = GetAllowedItems(_lists[i], false);
            for (int i = 0; i < _n; i++)
                if (_lists[2 * _n + i] != null)
                    allowedBottom[i] = GetAllowedItems(_lists[2 * _n + i], true);

            //potem filtrujemy listy boczne za pomoca tych macierzy
            int deleted = 0;
            for (int i = 0; i < _n; i++)
                if (_lists[_n + i] != null)
                    deleted += _lists[_n + i].RemoveAll(tbl => CheckIfPermToRemove(tbl, i, allowedTop, allowedBottom));
            for (int i = 0; i < _n; i++)
                if (_lists[3 * _n + i] != null)
                    deleted += _lists[3 * _n + i].RemoveAll(tbl => CheckIfPermToRemove(tbl, _n - i - 1, allowedTop, allowedBottom));
            return deleted;
        }

        private int ReduceVertical()
        {
            //redukcja list pionowych (gornej i dolnej)

            List<int>[][] allowedLeft = new List<int>[_n][];
            List<int>[][] allowedRight = new List<int>[_n][];
            for (int i = 0; i < _n; i++)
                if (_lists[3 * _n + i] != null)
                    allowedLeft[i] = GetAllowedItems(_lists[3 * _n + i], false);
            for (int i = 0; i < _n; i++)
                if (_lists[_n + i] != null)
                    allowedRight[i] = GetAllowedItems(_lists[_n + i], true);

            int deleted = 0;
            for (int i = 0; i < _n; i++)
                if (_lists[i] != null)
                    deleted += _lists[i].RemoveAll(tbl => CheckIfPermToRemove(tbl, i, allowedLeft, allowedRight));
            for (int i = 0; i < _n; i++)
                if (_lists[2 * _n + i] != null)
                    deleted += _lists[2 * _n + i].RemoveAll(tbl => CheckIfPermToRemove(tbl, _n - i - 1, allowedLeft, allowedRight));
            return deleted;
        }

    }
}
