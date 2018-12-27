using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscrapers
{
    public class Skyscrapers7x7
    {
        private readonly SkyscrapersNxN _solver = new SkyscrapersNxN(7);

        public int[][] SolvePuzzle(int[] clues)
        {
            return _solver.Solve(clues);
        }
    }
}
