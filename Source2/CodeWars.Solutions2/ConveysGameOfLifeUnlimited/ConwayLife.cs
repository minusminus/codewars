using System;

namespace CodeWars.Solutions2.ConveysGameOfLifeUnlimited
{
    public class ConwayLife
    {
        private static readonly int[,] EmptyResult = new int[,] { };
        private const int Dead = 0;
        private const int Alive = 1;

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
            int height = cells.GetLength(0);
            int width = cells.GetLength(1);
            int leftResize = CheckWidthResize(cells, height, 0);
            int rightResize = CheckWidthResize(cells, height, width - 1);
            int topResize = CheckHeightResize(cells, width, 0);
            int bottomResize = CheckHeightResize(cells, width, height - 1);

            int[,] result = new int[height + topResize + bottomResize, width + leftResize + rightResize];

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    int aliveNeighbours = CalculateNeighbours(cells, width, height, x, y);
                    result[y + topResize, x + leftResize] = LiveDieOrBeBorn(cells[y, x], aliveNeighbours);
                }
            if (leftResize > 0)
                for (int i = 1; i < height - 1; i++)
                {
                    int aliveNeighbours = CalculateNeighbours(cells, width, height, 0, i);
                    result[0, i + topResize] = DieOrBeBorn(aliveNeighbours);
                }

            return result;
        }

        private static int CheckHeightResize(in int[,] cells, int width, int row)
        {
            for (int i = 1; i < width - 1; i++)
                if (cells[row, i - 1] + cells[row, i] + cells[row, i + 1] == 3)
                    return 1;
            return 0;
        }

        private static int CheckWidthResize(in int[,] cells, int height, int column)
        {
            for (int i = 1; i < height - 1; i++)
                if (cells[i - 1, column] + cells[i, column] + cells[i + 1, column] == 3)
                    return 1;
            return 0;
        }

        private static int CalculateNeighbours(in int[,] cells, int width, int height, int positionX, int positionY)
        {
            int result = 0;

            for (int y = Math.Max(positionY - 1, 0); y <= Math.Min(positionY + 1, height - 1); y++)
                for (int x = Math.Max(positionX - 1, 0); x <= Math.Min(positionX + 1, width - 1); x++)
                    result += cells[y, x];

            if ((positionX >= 0) && (positionX < width) && (positionY >= 0) && (positionY < height))
                result -= cells[positionY, positionX];

            return result;
        }

        private static int LiveDieOrBeBorn(int currentState, int aliveNeighbours) => 
            currentState == Alive
                ? DieOrStayAlive(aliveNeighbours)
                : DieOrBeBorn(aliveNeighbours);

        private static int DieOrStayAlive(int aliveNeighbours) =>
            ((aliveNeighbours == 2) || (aliveNeighbours == 3)) ? Alive : Dead;

        private static int DieOrBeBorn(int aliveNeighbours) =>
            (aliveNeighbours == 3) ? Alive : Dead;
    }
}