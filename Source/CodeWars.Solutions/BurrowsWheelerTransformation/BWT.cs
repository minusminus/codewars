using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.Solutions.BurrowsWheelerTransformation
{
    /// <summary>
    /// Encode:
    /// 1. Generuje liste indeksów pierwszego znaku obroconej wartosci wejściowego stringa
    /// 2. Sortuje listę indeksów jako listę obróconych stringów
    /// 3. Na podstawie listy indeksów i ciągu wejściowego generuje ostatni znak w każdym wierszu i wyznacza pozycję wejściowego ciągu w macierzy
    /// 
    /// Decode:
    /// 1. Generuje pierwszą kolumnę macierzy sortując ostatnią kolumnę (wejściowy ciąg) z zachowaniem kolejności identycznych elementów z wejściowego ciągu
    /// 2. Generuje kolejne znaki oryginalnego ciągu na podstawie tablicy przejść
    /// </summary>
    public static class BWT
    {
        private class BWTStringComparer : IComparer<int>
        {
            private readonly string _baseString;

            public BWTStringComparer(string baseString)
            {
                this._baseString = baseString;
            }

            public int Compare(int x, int y)
            {
                for (int i = 0; i < _baseString.Length; i++)
                {
                    if (_baseString[(x + i) % _baseString.Length] != _baseString[(y + i) % _baseString.Length])
                        return (_baseString[(x + i) % _baseString.Length] < _baseString[(y + i) % _baseString.Length]) ? -1 : 1;
                }
                return 0;
            }
        }

        public static Tuple<string, int> Encode(string s) =>
            GetRotationStartIndexes(s)
                .SortSuffixes(s)
                .GetLastColumnAndOriginalTextIndex(s);

        private static IEnumerable<int> GetRotationStartIndexes(string s) =>
            Enumerable
                .Range(0, s.Length);

        private static IEnumerable<int> SortSuffixes(this IEnumerable<int> rotationStartIndexes, string originalString) =>
            rotationStartIndexes
                .OrderBy(x => x, new BWTStringComparer(originalString));

        private static Tuple<string, int> GetLastColumnAndOriginalTextIndex(this IEnumerable<int> sortedRotationStartIndexes, string originalString)
        {
            int foundIndex = -1;

            char CheckIndexAndGetLastCharInRow(int rotationStartIndex, int currentIndex)
            {
                if (rotationStartIndex == 0) foundIndex = currentIndex;
                return originalString[(rotationStartIndex - 1 + originalString.Length) % originalString.Length];
            }

            return new Tuple<string, int>(
                string.Join("", sortedRotationStartIndexes.Select((rotationStartIndex, i) => CheckIndexAndGetLastCharInRow(rotationStartIndex, i))), 
                foundIndex);
        }

        public static string Decode(string s, int i)
        {
            int[] firstRowMapper = GenerateFirstRowIndexes(s);

            Console.WriteLine(s);
            Console.WriteLine(string.Join(",", firstRowMapper.Select(j => j.ToString())));
            return string.Empty;
        }

        private static int[] GenerateFirstRowIndexes(string lastRow)
        {
            return Enumerable
                .Range(0, lastRow.Length)
                .OrderBy(i => lastRow[i])
                .ToArray();
        }
    }
}
