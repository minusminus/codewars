using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscrapers
{
    /// <summary>
    /// prekalkulowane dane do obliczen
    /// </summary>
    public class SkyscrapersPrecalcData
    {
        private readonly List<int[]>[] _visibilityLists;
        private readonly LineVisibilityChecker _lvc = new LineVisibilityChecker();

        public SkyscrapersPrecalcData(int n)
        {
            _visibilityLists = new List<int[]>[n];
            for (int i = 0; i < n; i++)
                _visibilityLists[i] = new List<int[]>();
            PrepareData(n);
        }

        public List<int[]> GetList(int visibility)
        {
            //return _visibilityLists[visibility - 1].ToList();
            return _visibilityLists[visibility - 1];
        }

        private void ElementProcessor(int[] perm)
        {
            int visibility = _lvc.FromLeft(perm);

            int[] item = new int[perm.Length];
            Array.Copy(perm, 0, item, 0, perm.Length);
            _visibilityLists[visibility - 1].Add(item);
        }

        private void PrepareData(int n)
        {
            PermutationGen pg = new PermutationGen();

            int[] tbl = Enumerable.Range(1, n).ToArray();
            pg.Gen(tbl, ElementProcessor);
        }
    }
}
