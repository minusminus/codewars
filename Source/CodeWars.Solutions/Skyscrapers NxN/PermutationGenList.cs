using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscrapers
{
    /// <summary>
    /// klasa obudowujaca generator permutacji, zwracajaca wszystkie permutacje w jednej liscie
    /// </summary>
    public class PermutationGenList
    {
        private List<int[]> _resList;

        private void ProcessElement(int[] perm)
        {
            int[] item = new int[perm.Length];
            Array.Copy(perm, 0, item, 0, perm.Length);
            _resList.Add(item);
        }

        public void Gen(int[] tbl, List<int[]> resList)
        {
            _resList = resList;
            PermutationGen pg = new PermutationGen();
            pg.Gen(tbl, ProcessElement);
        }
    }
}
