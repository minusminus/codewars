using System;

namespace CodeWars.Solutions2.ConveysGameOfLifeUnlimited
{
    public class ConwayLife
    {
        private static readonly int[,] EmptyResult = new int[,] { };
        private const int Dead = 0;
        private const int Alive = 1;

        /* Please note that the htmlize function for C# currently isn't working
            properly. I tested it on rextester.com and the code worked as expected,
            but for some reason on codewars it isn't. When I find a solution to
            the issue I will update the function. */
        public static int[,] GetGeneration(int[,] cells, int generation)
        {
            if (IsEmpty(cells))
                return EmptyResult;

            int[,] nextCells = cells;
            for (int i = 0; i < generation; i++)
            {
                nextCells = CalculateNextGeneration(nextCells);
                if (IsEmpty(nextCells))
                    return EmptyResult;
            }
            return nextCells;
        }

        private static bool IsEmpty(in int[,] cells) => 
            (cells.GetLength(0) * cells.GetLength(1)) == 0;

        private static int[,] CalculateNextGeneration(in int[,] cells)
        {
            int width = cells.GetLength(0);
            int height = cells.GetLength(1);
            int leftResize = CheckWidthResize(cells, height, 0);
            int rightResize = CheckWidthResize(cells, height, width - 1);
            int topResize = CheckHeightResize(cells, width, 0);
            int bottomResize = CheckHeightResize(cells, width, height - 1);

            int[,] result = new int[width + leftResize + rightResize, height + topResize + bottomResize];

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    int aliveNeighbours = CalculateNeighbours(cells, width, height, i, j);
                    result[i + leftResize, j + topResize] = DieLiveOrBeBorn(cells[i, j], aliveNeighbours);
                }
            if (leftResize > 0)
                for (int i = 1; i < height - 1; i++)
                {
                    int aliveNeighbours = CalculateNeighbours(cells, width, height, 0, i);
                    result[i + leftResize, 0] = DieOrBeBorn(aliveNeighbours);
                }

            return result;
        }

        private static int CheckHeightResize(in int[,] cells, int width, int row)
        {
            for (int i = 1; i < width - 1; i++)
                if (cells[i - 1, row] + cells[i, row] + cells[i + 1, row] == 3)
                    return 1;
            return 0;
        }

        private static int CheckWidthResize(in int[,] cells, int height, int column)
        {
            for (int i = 1; i < height - 1; i++)
                if (cells[column, i - 1] + cells[column, i] + cells[column, i + 1] == 3)
                    return 1;
            return 0;
        }

        private static int CalculateNeighbours(in int[,] cells, int width, int height, int positionX, int positionY)
        {
            //return
            //    ((positionX > 0) ? CalculateColumnNeighbours(cells, positionX - 1, positionY) : 0)
            //    + CalculateColumnNeighbours(cells, positionX, positionY) - cells[positionX, positionY]
            //    + ((positionX < width - 1) ? CalculateColumnNeighbours(cells, positionX + 1, positionY) : 0);

            //int CalculateColumnNeighbours(in int[,] cells, int x, int y) =>
            //    ((y > 0) ? cells[x, y - 1] : 0)
            //    + cells[x, y]
            //    + ((y < height - 1) ? cells[x, y + 1] : 0);

            int result = 0;
            for (int x = Math.Max(positionX - 1, 0); x <= Math.Min(positionX + 1, width - 1); x++)
                for (int y = Math.Max(positionY - 1, 0); y <= Math.Min(positionY + 1, height - 1); y++)
                    result += cells[x, y];
            if ((positionX >= 0) && (positionX < width) && (positionY >= 0) && (positionY < height))
                result -= cells[positionX, positionY];
            return result;
        }

        private static int DieLiveOrBeBorn(int currentState, int aliveNeighbours)
        {
            if (currentState == Alive)
                return ((aliveNeighbours == 2) || (aliveNeighbours == 3)) ? Alive : Dead;
            return DieOrBeBorn(aliveNeighbours);
        }

        private static int DieOrBeBorn(int aliveNeighbours) =>
            (aliveNeighbours == 3) ? Alive : Dead;
    }
}