using System;
using System.Globalization;
using System.Linq;

namespace CodeWars.Solutions2.ConveysGameOfLifeUnlimited
{
    public class ConwayLife
    {
        private const int Dead = 0;
        private const int Alive = 1;

        /* Please note that the htmlize function for C# currently isn't working
            properly. I tested it on rextester.com and the code worked as expected,
            but for some reason on codewars it isn't. When I find a solution to
            the issue I will update the function. */
        public static int[,] GetGeneration(int[,] cells, int generation)
        {
            if (IsEmpty(cells))
                return new int[,] { };

            int[,] nextCells = cells;
            for (int i = 0; i < generation; i++)
            {
                nextCells = CalculateNextGeneration(nextCells);
                if (IsEmpty(nextCells))
                    return new int[,] { };
            }
            return nextCells;
        }

        private static bool IsEmpty(in int[,] cells) => 
            (cells.GetLength(0) * cells.GetLength(1)) == 0;

        private static int[,] CalculateNextGeneration(in int[,] cells)
        {
            int width = cells.GetLength(0);
            int height = cells.GetLength(1);

            int[,] result = new int[width, height];

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    int aliveNeighbours = CalculateNeighbours(cells, width, height, i, j);
                    result[i, j] = DieLiveOrBeBorn(cells[i, j], aliveNeighbours);
                }

            return result;
        }

        private static int CalculateNeighbours(in int[,] cells, int width, int height, int positionX, int positionY)
        {
            return
                ((positionX > 0) ? CalculateColumnNeighours(cells, positionX - 1, positionY) : 0)
                + CalculateColumnNeighours(cells, positionX, positionY) - cells[positionX, positionY]
                + ((positionX < width - 1) ? CalculateColumnNeighours(cells, positionX + 1, positionY) : 0);

            int CalculateColumnNeighours(in int[,] cells, int x, int y) =>
                ((y > 0) ? cells[x, y - 1] : 0)
                + cells[x, y]
                + ((y < height - 1) ? cells[x, y + 1] : 0);
        }

        private static int DieLiveOrBeBorn(int currentState, int aliveNeighbours)
        {
            if (currentState == Alive)
                return ((aliveNeighbours == 2) || (aliveNeighbours == 3)) ? Alive : Dead;
            return (aliveNeighbours == 3) ? Alive : Dead;
        }
    }
}