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
            //var clues = new[]
            //{
            //    2, 2, 1, 3,
            //    2, 2, 3, 1,
            //    1, 2, 2, 3,
            //    3, 2, 1, 3
            //};
            var clues = new[]
            {
                0, 0, 1, 2,
                0, 2, 0, 0,
                0, 3, 0, 0,
                0, 1, 0, 0
            };



            //testowe wywolanie
            const int CALLSCOUNT = 20;

            Skyscrapers4x4 testobj = new Skyscrapers4x4();
            for(int i=0; i<CALLSCOUNT; i++)
            {
                testobj.SolvePuzzle(clues);
            }
        }
    }
}
