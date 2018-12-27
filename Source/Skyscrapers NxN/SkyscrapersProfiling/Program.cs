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
            //7x7 medium
            //var clues = new[]
            //{
            //    7, 0, 0, 0, 2, 2, 3,
            //    0, 0, 3, 0, 0, 0, 0,
            //    3, 0, 3, 0, 0, 5, 0,
            //    0, 0, 0, 0, 5, 0, 4
            //};

            //7x7 hard
            var clues = new[]
            {
                0, 2, 3, 0, 2, 0, 0,
                5, 0, 4, 5, 0, 4, 0,
                0, 4, 2, 0, 0, 0, 6,
                5, 2, 2, 2, 2, 4, 1
            };


            //testowe wywolanie
            const int CALLSCOUNT = 1;

            Skyscrapers7x7 testobj = new Skyscrapers7x7();
            for(int i=0; i<CALLSCOUNT; i++)
            {
                testobj.SolvePuzzle(clues);
            }
        }
    }
}
