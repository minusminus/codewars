using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscrapers
{
    /// <summary>
    /// rozwiazanie na podstawie: http://norvig.com/sudoku.html
    /// 
    /// </summary>
    public class Skyscrapers4x4
    {
        private readonly SkyscrapersNxN _solver = new SkyscrapersNxN(4);

        public int[][] SolvePuzzle(int[] clues)
        {
            return _solver.Solve(clues);
        }
    }
}
