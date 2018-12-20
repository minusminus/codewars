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

        public SkyscrapersNxN_Perms(int n, SkyscrapersPrecalcData precalc)
        {
            _n = n;
            _precalc = precalc;
        }

        public int[][] Solve(int[] constraints)
        {
            //redukcja list
            SkyscraperNxNDataLists initData = new SkyscraperNxNDataLists(constraints, _n, _precalc);
            PrepareInitialData(initData);
            SkyscraperNxNDataLists resLists = ReduceLists(initData);

            if (resLists == null)
                throw new Exception("solution not found");

            //wygenerowanie wynikowej tabeli
            SkyscraperData_Perms data = new SkyscraperData_Perms(_n);
            PopulateResultsData(resLists, data);
            return ConvertToResultTable(data);
        }

        private int[][] ConvertToResultTable(SkyscraperData_Perms data)
        {
            int[][] res = new int[_n][];
            for (int i = 0; i < _n; i++)
            {
                res[i] = new int[_n];
                for (int j = 0; j < _n; j++)
                    res[i][j] = Array.IndexOf(SkyscraperData.Masks, data.Data[i, j]);
            }
            return res;
        }

        private SkyscraperNxNDataLists ReduceLists(SkyscraperNxNDataLists dataLists)
        {
            PerformListReduction(dataLists);
            int shortest;
            if (!CheckListsAndFindShortest(dataLists, out shortest)) return null;
            if (shortest == -1) return dataLists;
            for (int i = 0; i < dataLists.Lists[shortest].Idx.Count; i++)
            {
                SkyscraperNxNDataLists next = new SkyscraperNxNDataLists(dataLists);
                next.Lists[shortest].Idx = new List<int>() {dataLists.Lists[shortest].Idx[i]};
                ReduceListsSingleItemOnList(next, shortest);
                SkyscraperNxNDataLists nextres = ReduceLists(next);
                if (nextres != null) return nextres;
            }
            return null;
        }

        private void PrepareInitialData(SkyscraperNxNDataLists dataLists)
        {
            for (int i = 0; i < dataLists.Lists.Length; i++)
                if ((dataLists.Lists[i] != null) && (dataLists.Lists[i].Idx.Count == 1))
                    ReduceListsSingleItemOnList(dataLists, i);
        }

        private void PerformListReduction(SkyscraperNxNDataLists dataLists)
        {
            while (true)
            {
                //redukcja w poziomie i pionie wszystkich list
                while (ReduceListsHorizVert(dataLists) > 0) ;
                //redukcje par przeciwleglych
                if (ReduceListsOpposite(dataLists) == 0) return;
            }
        }

        private bool CheckListsAndFindShortest(SkyscraperNxNDataLists dataLists, out int shortestIndex)
        {
            shortestIndex = -1;
            int currLen = int.MaxValue;

            for(int i=0; i<dataLists.Lists.Length; i++)
            {
                if (dataLists.Lists[i] == null) continue;
                SkyscrapersNxNDataObject list = dataLists.Lists[i];
                if (list.Idx.Count == 0) return false;
                if ((list.Idx.Count > 1) && (list.Idx.Count < currLen))
                {
                    currLen = list.Idx.Count;
                    shortestIndex = i;
                }
            }

            return true;
        }
        
        private List<int>[] GetAllowedItems(SkyscrapersNxNDataObject data)
        {
            bool[][] existing = new bool[_n][];
            for (int i = 0; i < _n; i++)
                existing[i] = new bool[_n];

            foreach (int ix in data.Idx)
                for (int i = 0; i < _n; i++)
                    existing[i][data.PrecalcData[ix][i] - 1] = true;

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

        private List<int>[][] CreateAllowedItemsList(SkyscraperNxNDataLists dataLists, int nPart)
        {
            List<int>[][] res = new List<int>[_n][];
            for (int i = 0; i < _n; i++)
                if (dataLists.Lists[nPart * _n + i] != null)
                    res[i] = GetAllowedItems(dataLists.Lists[nPart * _n + i]);
            return res;
        }

        private int ReduceListsDir(SkyscraperNxNDataLists dataLists, int nPart, List<int>[][] allowedTop, List<int>[][] allowedBottom)
        {
            int deleted = 0;
            for (int i = 0; i < _n; i++)
                if ((dataLists.Lists[nPart*_n + i] != null) && (dataLists.Lists[nPart*_n + i].Idx.Count > 1))
                {
                    deleted += dataLists.Lists[nPart*_n + i].Idx.RemoveAll(ix => CheckIfPermToRemove(dataLists.Lists[nPart*_n + i].PrecalcData[ix], i, allowedTop, allowedBottom));
                    if (dataLists.Lists[nPart*_n + i].Idx.Count == 1)
                        ReduceListsSingleItemOnList(dataLists, nPart*_n + i);
                }
            return deleted;
        }

        private int ReduceListsHorizVert(SkyscraperNxNDataLists dataLists)
        {
            //reduce horizontal
            //z list gornych i dolnych wyznaczane dopuszczalne wartosci na pozycjach i na tej podstawie filtrowane listy prawe i lewe
            List<int>[][] allowedTop = CreateAllowedItemsList(dataLists, 0);   //top
            List<int>[][] allowedBottom = CreateAllowedItemsList(dataLists, 2);    //bottom

            int deleted = ReduceListsDir(dataLists, 1, allowedTop, allowedBottom);
            deleted += ReduceListsDir(dataLists, 3, allowedBottom, allowedTop);

            //reduce vertical
            //analogicznie, na podstawie prawych i lewych filtrowane listy gorne i dolne
            allowedTop = CreateAllowedItemsList(dataLists, 1); //right
            allowedBottom = CreateAllowedItemsList(dataLists, 3);  //left

            deleted += ReduceListsDir(dataLists, 0, allowedBottom, allowedTop);
            deleted += ReduceListsDir(dataLists, 2, allowedTop, allowedBottom);

            return deleted;
        }

        private int GetOppositeListIndex(int index)
        {
            return ((index/_n + 2)%4)*_n + (_n - (index%_n) - 1);
        }

        private int ReduceListsOpposite(SkyscraperNxNDataLists dataLists)
        {
            int deleted = 0;
            //redukcja list przeciwleglych
            //dla listy 1 generowane listy dopuszczalnych wartosci i lista 2 filtrowana na odpowiadajacych pozycjach
            //pionowe
            for (int i = 0; i < _n; i++)
                if ((dataLists.Lists[i] != null) && (dataLists.Lists[GetOppositeListIndex(i)] != null))
                    deleted += ReduceListOppositePair(dataLists, i, GetOppositeListIndex(i));
            //poziome
            for (int i = 0; i < _n; i++)
                if ((dataLists.Lists[_n + i] != null) && (dataLists.Lists[GetOppositeListIndex(_n + i)] != null))
                    deleted += ReduceListOppositePair(dataLists, _n + i, GetOppositeListIndex(_n + i));
            return deleted;
        }

        private int ReduceListOppositePair(SkyscraperNxNDataLists dataLists, int i1, int i2)
        {
            int deleted = ReduceListOppositePairSingle(dataLists, i1, i2);

            int currdeleted = 1;
            while (currdeleted > 0)
            {
                int t = i1;
                i1 = i2;
                i2 = t;
                currdeleted = ReduceListOppositePairSingle(dataLists, i1, i2);
                deleted += currdeleted;
            }

            return deleted;
        }

        private int ReduceListOppositePairSingle(SkyscraperNxNDataLists dataLists, int i1, int i2)
        {
            int deleted = 0;
            List<int>[] allowed = GetAllowedItems(dataLists.Lists[i1]);
            for (int i = 0; i < _n; i++)
                deleted += dataLists.Lists[i2].Idx.RemoveAll(ix => (!allowed[i].Contains(dataLists.Lists[i2].PrecalcData[ix][_n - i - 1])));
            if ((deleted > 0) && (dataLists.Lists[i2].Idx.Count == 1))
                ReduceListsSingleItemOnList(dataLists, i2);
            return deleted;
        }

        private int ReduceListsSingleItemOnList(SkyscraperNxNDataLists dataLists, int singleItemIndex)
        {
            int side = singleItemIndex/_n;   //strona kwadratu
            int lvl = singleItemIndex%_n;  //indeks na stronie kwadratu
            int orientation = side%2;   //wskazana lista jest pionowa (parzyste) czy pozioma (nieparzyste)
            int top = (side/2);   //gorna czy dolna (prawa czy lewa)

            //przeciwlegla lista jest juz niepotrzebna i mozna ja usunac
            //Console.WriteLine($"{singleItemIndex}, opposite: {GetOppositeListIndex(singleItemIndex)}");
            //dataLists.Lists[GetOppositeListIndex(singleItemIndex)] = null;

            //odpowiednie indeksy w kolejnosci stron: gorna, dolna, prawa, lewa
            int[][] sideindexes = new int[4][] {new int[] {3, 1, 0, 2}, new int[] {1, 3, 2, 0}, new int[] {0, 2, 1, 3}, new int[] {2, 0, 3, 1}};
            int[] sideix = sideindexes[orientation*2 + top];

            int[] row = dataLists.Lists[singleItemIndex].PrecalcData[dataLists.Lists[singleItemIndex].Idx[0]];
            int deleted = 0;
            for (int i = 0; i < _n; i++)
                deleted += ReduceListsSingleItemOnListSideReduction(dataLists, sideix[0], sideix[1], sideix[2], sideix[3], i, lvl, row);
            return deleted;
        }

        private int ReduceListsSingleItemOnListSideReduction(SkyscraperNxNDataLists dataLists, int ieq1, int ieq2,
            int inoneq1, int inoneq2, int i, int lvl, int[] row)
        {
            //prostopadle
            int deleted = ReduceListsSingleItemOnListOneNonEq(dataLists, ieq1*_n + (_n - i - 1), lvl, row[i]);
            deleted += ReduceListsSingleItemOnListOneNonEq(dataLists, ieq2*_n + i, _n - lvl - 1, row[i]);
            //rownolegle
            if (i != lvl)
                deleted += ReduceListsSingleItemOnListOneEq(dataLists, inoneq1*_n + i, row, false);
            if (_n - i - 1 != lvl)
                deleted += ReduceListsSingleItemOnListOneEq(dataLists, inoneq2*_n + i, row, true);
            return deleted;
        }

        private int ReduceListsSingleItemOnListOneNonEq(SkyscraperNxNDataLists dataLists, int listIndex, int lvlIndex,
            int rowValue)
        {
            if (dataLists.Lists[listIndex] == null) return 0;
            int deleted = dataLists.Lists[listIndex].Idx.RemoveAll(ix => dataLists.Lists[listIndex].PrecalcData[ix][lvlIndex] != rowValue);
            if ((deleted > 0) && (dataLists.Lists[listIndex].Idx.Count == 1))
                deleted += ReduceListsSingleItemOnList(dataLists, listIndex);
            return deleted;
        }

        private bool CheckIfSingleItemPermToRemove(int[] checkRow, int[] singleRow, bool backwards)
        {
            if (backwards)
            {
                for (int i = 0; i < _n; i++)
                    if (checkRow[_n - i - 1] == singleRow[i]) return true;
            }
            else
            {
                for (int i = 0; i < _n; i++)
                    if (checkRow[i] == singleRow[i]) return true;
            }
            return false;
        }

        private int ReduceListsSingleItemOnListOneEq(SkyscraperNxNDataLists dataLists, int listIndex, int[] singleRow, bool backwards)
        {
            if (dataLists.Lists[listIndex] == null) return 0;
            int deleted = dataLists.Lists[listIndex].Idx.RemoveAll(ix => CheckIfSingleItemPermToRemove(dataLists.Lists[listIndex].PrecalcData[ix], singleRow, backwards));
            if ((deleted > 0) && (dataLists.Lists[listIndex].Idx.Count == 1))
                deleted += ReduceListsSingleItemOnList(dataLists, listIndex);
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

        private void PopulateResultsData(SkyscraperNxNDataLists dataLists, SkyscraperData_Perms data)
        {
            //ustawienie elementow ze zredukowanych list
            for (int i = 0; i < dataLists.Lists.Length; i++)
                if (dataLists.Lists[i] != null)
                    for (int j = 0; j < _n; j++)
                        data.SetSingleElement(GetDataRowCol(i, j), dataLists.Lists[i].PrecalcData[dataLists.Lists[i].Idx[0]][j]);

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
