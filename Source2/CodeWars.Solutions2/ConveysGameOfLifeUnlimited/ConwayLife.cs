namespace CodeWars.Solutions2.ConveysGameOfLifeUnlimited;

/// <summary>
/// CodeWars kata: Conway's Game of Life - Unlimited Edition
/// 
/// 
/// </summary>
public class ConwayLife
{
    private static readonly int[,] EmptyResult = { };
    private const int Dead = 0;
    private const int Alive = 1;

    public static int[,] GetGeneration(int[,] cells, int generation)
    {
        if (IsEmpty(cells)) return EmptyResult;

        int[,] nextCells = cells;
        for (int i = 0; i < generation; i++)
        {
            nextCells = CropGeneration(CalculateNextGeneration(nextCells));
            if (IsEmpty(nextCells)) return EmptyResult;
        }
        return nextCells;
    }

    private static bool IsEmpty(in int[,] cells) =>
        (cells.GetLength(0) * cells.GetLength(1)) == 0;

    private static int[,] CalculateNextGeneration(in int[,] cells)
    {
        int height = cells.GetLength(0);
        int width = cells.GetLength(1);
        int leftResize = GetWidthResize(cells, height, 0);
        int rightResize = GetWidthResize(cells, height, width - 1);
        int topResize = GetHeightResize(cells, width, 0);
        int bottomResize = GetHeightResize(cells, width, height - 1);

        int[,] result = new int[height + topResize + bottomResize, width + leftResize + rightResize];

        for (int y = 0 - topResize; y < height + bottomResize; y++)
            for (int x = 0 - leftResize; x < width + rightResize; x++)
            {
                int aliveNeighbors = CalculateNeighbors(cells, width, height, x, y);
                result[y + topResize, x + leftResize] = LiveDieOrBeBorn(GetCellsValue(cells, width, height, x, y), aliveNeighbors);
            }

        return result;
    }

    private static int GetHeightResize(in int[,] cells, int width, int row)
    {
        for (int i = 1; i < width - 1; i++)
            if (cells[row, i - 1] + cells[row, i] + cells[row, i + 1] == 3)
                return 1;
        return 0;
    }

    private static int GetWidthResize(in int[,] cells, int height, int column)
    {
        for (int i = 1; i < height - 1; i++)
            if (cells[i - 1, column] + cells[i, column] + cells[i + 1, column] == 3)
                return 1;
        return 0;
    }

    private static int CalculateNeighbors(in int[,] cells, int width, int height, int positionX, int positionY)
    {
        int result = 0;

        for (int y = Math.Max(positionY - 1, 0); y <= Math.Min(positionY + 1, height - 1); y++)
            for (int x = Math.Max(positionX - 1, 0); x <= Math.Min(positionX + 1, width - 1); x++)
                result += cells[y, x];

        return result - GetCellsValue(cells, width, height, positionX, positionY);
    }

    private static int GetCellsValue(in int[,] cells, int width, int height, int x, int y) =>
        (x >= 0) && (x < width) && (y >= 0) && (y < height)
            ? cells[y, x]
            : Dead;

    private static int LiveDieOrBeBorn(int currentState, int aliveNeighbors) =>
        currentState == Alive
            ? DieOrStayAlive(aliveNeighbors)
            : DieOrBeBorn(aliveNeighbors);

    private static int DieOrStayAlive(int aliveNeighbors) =>
        (aliveNeighbors is 2 or 3) ? Alive : Dead;

    private static int DieOrBeBorn(int aliveNeighbors) =>
        (aliveNeighbors == 3) ? Alive : Dead;

    private static int[,] CropGeneration(in int[,] cells)
    {
        int height = cells.GetLength(0);
        int width = cells.GetLength(1);
        int leftCrop = GetColumnsToCrop(cells, width, height, 0, 1);
        int rightCrop = GetColumnsToCrop(cells, width, height, width - 1, -1);
        int topCrop = GetRowsToCrop(cells, width, height, 0, 1);
        int bottomCrop = GetRowsToCrop(cells, width, height, height - 1, -1);

        if ((leftCrop == 0) && (rightCrop == 0) && (topCrop == 0) && (bottomCrop == 0)) return cells;

        int newHeight = Math.Max(height - topCrop - bottomCrop, 0);
        int newWidth = Math.Max(width - leftCrop - rightCrop, 0);
        int[,] croppedCells = new int[newHeight, newWidth];

        for (int y = 0; y < newHeight; y++)
            for (int x = 0; x < newWidth; x++)
                croppedCells[y, x] = cells[y + topCrop, x + leftCrop];

        return croppedCells;
    }

    private static int GetRowsToCrop(in int[,] cells, int width, int height, int startRow, int step) =>
        GetEmptyToCrop(IsRowEmpty, cells, height, width, startRow, step);

    private static int GetColumnsToCrop(in int[,] cells, int width, int height, int startColumn, int step) =>
        GetEmptyToCrop(IsColumnEmpty, cells, width, height, startColumn, step);

    private static int GetEmptyToCrop(Func<int[,], int, int, bool> isEmpty, in int[,] cells, int indexUpperBound, int countedElementUpperBound, int startIndex, int step)
    {
        int result = 0;
        int index = startIndex;
        do
        {
            if (!isEmpty(cells, countedElementUpperBound, index)) break;
            result++;
            index += step;
        } while ((index >= 0) && (index < indexUpperBound));
        return result;
    }

    private static bool IsRowEmpty(int[,] cells, int width, int row)
    {
        for (int i = 0; i < width; i++)
            if (cells[row, i] == Alive) return false;
        return true;
    }

    private static bool IsColumnEmpty(int[,] cells, int height, int column)
    {
        for (int i = 0; i < height; i++)
            if (cells[i, column] == Alive) return false;
        return true;
    }
}