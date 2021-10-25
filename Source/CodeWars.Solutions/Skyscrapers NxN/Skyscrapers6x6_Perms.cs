using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscrapers
{
    public class Skyscrapers6x6_Perms
    {
        private static readonly SkyscrapersPrecalcData precalcData = new SkyscrapersPrecalcData(6);
        private readonly SkyscrapersNxN_Perms _solver = new SkyscrapersNxN_Perms(6, precalcData);

        public int[][] SolvePuzzle(int[] clues)
        {
            return _solver.Solve(clues);
        }
    }
}
