using System;
using System.Linq;

namespace CodeWars.Solutions2.ConveysGameOfLifeUnlimited
{
    public class ConwayLife
    {
        /* Please note that the htmlize function for C# currently isn't working
            properly. I tested it on rextester.com and the code worked as expected,
            but for some reason on codewars it isn't. When I find a solution to
            the issue I will update the function. */
        public static int[,] GetGeneration(int[,] cells, int generation)
        {
            if (IsEmpty(cells))
                return new int[,] { };

            return new int[,] { };
        }

        private static bool IsEmpty(int[,] cells) => 
            (cells.GetLength(0) * cells.GetLength(1)) > 0;
    }
}