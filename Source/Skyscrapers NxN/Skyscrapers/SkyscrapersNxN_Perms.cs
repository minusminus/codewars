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
            _lists = new List<int[]>[4 * _n];  //tablica list zainicjalizowana nulami

            //zainicjalizowanie list dla wszystkich ograniczen
            for (int i = 0; i < _lists.Length; i++)
                if (constraints[i] != 0)
                    _lists[i] = _precalc.GetList(constraints[i]);

            //redukcja list
            ReduceLists();

            //wygenerowanie wynikowej tabeli
            SkyscraperData_Perms data = new SkyscraperData_Perms(_n);
            PopulateResultsData(data);

            //konwersja do wynikowego formatu
            int[][] res = new int[_n][];
            for (int i = 0; i < _n; i++)
            {
                res[i] = new int[_n];
                for (int j = 0; j < _n; j++)
                    res[i][j] = Array.IndexOf(SkyscraperData.Masks, data.Data[i, j]);
            }

            return res;
        }

        private void ReduceLists()
        {
            while (true)
            {
                //redukcja w poziomie i pionie wszystkich list
                while (ReduceListsHorizVert() > 0) ;
                //redukcje par przeciwleglych
                if (ReduceListsOpposite() == 0) return;
            }
        }

        private List<int>[] GetAllowedItems(List<int[]> list)
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
                if (((allowedTop[_n - i - 1] != null) && (!allowedTop[_n - i - 1][pos].Contains(tbl[i])))
                    || ((allowedBottom[i] != null) && (!allowedBottom[i][_n - pos - 1].Contains(tbl[i]))))
                    return true;
            return false;
        }

        private List<int>[][] CreateAllowedItemsList(int nPart)
        {
            List<int>[][] res = new List<int>[_n][];
            for (int i = 0; i < _n; i++)
                if (_lists[nPart * _n + i] != null)
                    res[i] = GetAllowedItems(_lists[nPart * _n + i]);
            return res;
        }

        private int ReduceListsDir(int nPart, List<int>[][] allowedTop, List<int>[][] allowedBottom)
        {
            int deleted = 0;
            for (int i = 0; i < _n; i++)
                if (_lists[nPart * _n + i] != null)
                    deleted += _lists[nPart * _n + i].RemoveAll(tbl => CheckIfPermToRemove(tbl, i, allowedTop, allowedBottom));
            return deleted;
        }

        private int ReduceListsHorizVert()
        {
            //reduce horizontal
            //z list gornych i dolnych wyznaczane dopuszczalne wartosci na pozycjach i na tej podstawie filtrowane listy prawe i lewe
            List<int>[][] allowedTop = CreateAllowedItemsList(0);   //top
            List<int>[][] allowedBottom = CreateAllowedItemsList(2);    //bottom

            int deleted = ReduceListsDir(1, allowedTop, allowedBottom);
            deleted += ReduceListsDir(3, allowedBottom, allowedTop);

            //reduce vertical
            //analogicznie, na podstawie prawych i lewych filtrowane listy gorne i dolne
            allowedTop = CreateAllowedItemsList(1); //right
            allowedBottom = CreateAllowedItemsList(3);  //left

            deleted += ReduceListsDir(0, allowedBottom, allowedTop);
            deleted += ReduceListsDir(2, allowedTop, allowedBottom);

            return deleted;
        }

        private int ReduceListsOpposite()
        {
            int deleted = 0;
            //redukcja list przeciwleglych
            //dla listy 1 generowane listy dopuszczalnych wartosci i lista 2 filtrowana na odpowiadajacych pozycjach
            //pionowe
            for (int i = 0; i < _n; i++)
                if ((_lists[i] != null) && (_lists[2 * _n + (_n - i - 1)] != null))
                    deleted += ReduceListOppositePair(i, 2 * _n + (_n - i - 1));
            //poziome
            for (int i = 0; i < _n; i++)
                if ((_lists[_n + i] != null) && (_lists[3 * _n + (_n - i - 1)] != null))
                    deleted += ReduceListOppositePair(_n + i, 3 * _n + (_n - i - 1));
            return deleted;
        }

        private int ReduceListOppositePair(int i1, int i2)
        {
            int deleted = ReduceListOppositePairSingle(i1, i2);

            int currdeleted = 1;
            while (currdeleted > 0)
            {
                int t = i1;
                i1 = i2;
                i2 = t;
                currdeleted = ReduceListOppositePairSingle(i1, i2);
                deleted += currdeleted;
            }

            return deleted;
        }

        private int ReduceListOppositePairSingle(int i1, int i2)
        {
            int deleted = 0;
            List<int>[] allowed = GetAllowedItems(_lists[i1]);
            for (int i = 0; i < _n; i++)
                deleted += _lists[i2].RemoveAll(tbl => (!allowed[i].Contains(tbl[_n - i - 1])));
            return deleted;
        }

        private Tuple<int, int> GetDataRowCol(int listNum, int listIndex)
        {
            int row = 0, col = 0;

            switch (listNum / _n)
            {
                case 0:
                    row = listIndex;
                    col = listNum % _n;
                    break;
                case 1:
                    row = listNum % _n;
                    col = _n - listIndex - 1;
                    break;
                case 2:
                    row = _n - listIndex - 1;
                    col = _n - listNum % _n - 1;
                    break;
                case 3:
                    row = _n - listNum % _n - 1;
                    col = listIndex;
                    break;
            }

            return new Tuple<int, int>(row, col);
        }

        private void PopulateResultsData(SkyscraperData_Perms data)
        {
            //ustawienie elementow ze zredukowanych list
            for (int i = 0; i < _lists.Length; i++)
                if (_lists[i] != null)
                    for (int j = 0; j < _n; j++)
                        data.SetSingleElement(GetDataRowCol(i, j), _lists[i][0][j]);

            //lista nieustawionych elementow
            List<Tuple<int, int>> notSet = new List<Tuple<int, int>>();
            for (int row = 0; row < _n; row++)
                for (int col = 0; col < _n; col++)
                    if (data.Data[row, col] == 0)
                        notSet.Add(new Tuple<int, int>(row, col));

            //ustawienie brakujacych elementow
            while (notSet.Count > 0)
            {
                int p = 0;
                while (p < notSet.Count)
                {
                    int x = (data.SetInRow[notSet[p].Item1] | data.SetInCol[notSet[p].Item2]) ^ SkyscraperData_Perms.InitialValues[_n];
                    if (data.CountBits(x) == 1)
                    {
                        data.SetSingleElementMask(notSet[p].Item1, notSet[p].Item2, x);
                        notSet.RemoveAt(p);
                    }
                    else p++;
                }
            }
        }
    }
}
