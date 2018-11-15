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

            //1. usunac z nowej listy wszystkie niepasujace do biezacych list
            PrepareList(index);
            //2. usunac z biezacych to co nie pasuje do nowej listy
            //3. usuwac z pionowych to co nie pasuje do poziomych i z poziomych to co nie pasuje do pionowych do momentu az nic nie zostanie usuniete
        }

        private List<int> GetAllItemsAtPosition(List<int[]> list, int pos )
        {
            bool[] existing = new bool[_n];
            for (int i = 0; i < _n; i++) existing[i] = false;

            foreach (int[] tbl in list)
                existing[tbl[pos] - 1] = true;

            List<int> res = new List<int>();
            for (int i = 0; i < _n; i++)
                if (existing[i])
                    res.Add(i + 1);
            return res;
        }

        private void PrepareList(int index)
        {
            List<int[]> newList = _lists[index];
            int side = index/_n;
            int sidepos = index%_n;

            //gorne listy pomijane
            if (side == 0) return;

            //na poczatek zakladamy ze:
            //nowa lista jest z prawej strony, analizujemy listy z gory
            int startrange = 0;
            //if ((side & 1) == 0) startrange = _n;
            //dla kazdej listy z gory wyznaczamy na pozycji okreslonej przez nowa liste, liste dopuszczalnych elementow (wystepujacych na pozycji w gornej liscie)
            //i filtrujemy nimi nowa liste na pozycji okreslonej przez indeks gornej listy
            //od razu analogicznie dla dolnych list z odwroconym indeksem
            for (int i = startrange; i < startrange + _n; i++)
            {
                List<int> allowedTop = null;
                List<int> allowedBottom = null;
                if (_lists[i] != null)
                    allowedTop = GetAllItemsAtPosition(_lists[i], sidepos);
                if (_lists[i + 2*_n] != null)
                    allowedBottom = GetAllItemsAtPosition(_lists[i + 2*_n], _n - sidepos - 1);
                if ((allowedTop == null) && (allowedBottom == null)) continue;
                newList.RemoveAll(tbl => ((allowedTop != null) && (!allowedTop.Contains(tbl[i - startrange]))) 
                                        || ((allowedBottom != null) && (!allowedBottom.Contains(tbl[i - startrange]))));
            }
        }

    }
}
