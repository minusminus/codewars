using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscrapers
{
    /// <summary>
    /// Mechanizm wyznaczający ilość widocznych budynków
    /// </summary>
    public class LineVisibilityChecker
    {
        public int FromLeft(int[] line)
        {
            int res = 1;
            int highest = line[0];
            for (int i = 1; i < line.Length; i++)
                if (line[i] > highest)
                {
                    highest = line[i];
                    res++;
                }
            return res;
        }
    }
}
