using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscrapers
{
    public class Skyscrapers7x7_Perms
    {
        private static readonly SkyscrapersPrecalcData precalcData = new SkyscrapersPrecalcData(7);
        private readonly SkyscrapersNxN_Perms _solver = new SkyscrapersNxN_Perms(7, precalcData);

        public int[][] SolvePuzzle(int[] clues)
        {
            return _solver.Solve(clues);
        }
    }
}
