using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skyscrapers;

namespace SkyscrapersProfiling
{
    class Program
    {
        static void Main(string[] args)
        {
            var clues = new[]{ 3, 2, 2, 3, 2, 1,
                           1, 2, 3, 3, 2, 2,
                           5, 1, 2, 2, 4, 3,
                           3, 2, 1, 2, 2, 4};


            //testowe wywolanie
            const int CALLSCOUNT = 20;

            Skyscrapers6x6 testobj = new Skyscrapers6x6();
            for(int i=0; i<CALLSCOUNT; i++)
            {
                testobj.SolvePuzzle(clues);
            }
        }
    }
}
