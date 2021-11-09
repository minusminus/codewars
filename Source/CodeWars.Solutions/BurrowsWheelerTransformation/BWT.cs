using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars.Solutions.BurrowsWheelerTransformation
{
    /// <summary>
    /// Encode:
    /// 1. Generuje liste indeksów pierwszego znaku obroconej wartosci wejściowego stringa
    /// 2. Sortuje listę indeksów jako listę obróconych stringów
    /// 3. Na podstawie listy indeksów i ciągu wejściowego generuje ostatni znak w każdym wierszu i wyznacza pozycję wejściowego ciągu w macierzy
    /// 
    /// Decode:
    /// 1. Generuje tablicę ilości porzedzających identycznych znaków i słownik ilości znaków poprzedzających bieżący.
    /// 2. Generuje kolejne znaki oryginalnego ciągu na podstawie obu tablic przejść
    /// 
    /// Oparte w większości na:
    /// https://michaeldipperstein.github.io/bwt.html
    /// </summary>
    public static class BWT
    {
        private class BWTStringComparer : IComparer<int>
        {
            private readonly string _baseString;

            public BWTStringComparer(string baseString)
            {
                _baseString = baseString;
            }

            public int Compare(int x, int y)
            {
                for (int i = 0; i < _baseString.Length; i++)
                    if (GetBaseChar(x, i) != GetBaseChar(y, i))
                        return (GetBaseChar(x, i) < GetBaseChar(y, i)) ? -1 : 1;
                return 0;
            }

            private char GetBaseChar(int startIndex, int position) =>
                _baseString[(startIndex + position) % _baseString.Length];
        }

        public static Tuple<string, int> Encode(string s) =>
            string.IsNullOrEmpty(s)
            ? new Tuple<string, int>(string.Empty, 0)
            : GetRotationStartIndexes(s)
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

        public static string Decode(string s, int i) => 
            string.IsNullOrEmpty(s)
                ? string.Empty
                : DecodeString(s, i,
                        GetPrecedingSymbolsCount(s, out ConcurrentDictionary<char, int> symbolsCount),
                        GetSymbolsLessThanCurrent(symbolsCount))
                    .ReverseAndBuildString();

        private static int[] GetPrecedingSymbolsCount(string input, out ConcurrentDictionary<char, int> symbolsCount)
        {
            ConcurrentDictionary<char, int> counts = new ConcurrentDictionary<char, int>();
            symbolsCount = counts;
            return input
                .Select(c =>
                {
                    counts.AddOrUpdate(c, 1, (key, value) => value + 1);
                    return counts[c] - 1;
                })
                .ToArray();
        }

        private static Dictionary<char, int> GetSymbolsLessThanCurrent(ConcurrentDictionary<char, int> symbolsCount)
        {
            var orderedSymbolsCount = symbolsCount
                .OrderBy(x => x.Key)
                .ToArray();

            int currentSum = 0;
            Dictionary<char, int> symbolsLessThanCurrent = new Dictionary<char, int>() { { orderedSymbolsCount[0].Key, currentSum } };
            for (int i = 1; i < orderedSymbolsCount.Length; i++)
            {
                currentSum += orderedSymbolsCount[i - 1].Value;
                symbolsLessThanCurrent[orderedSymbolsCount[i].Key] = currentSum;
            }
            return symbolsLessThanCurrent;
        }

        private static IEnumerable<char> DecodeString(string encoded, int originalStringIndex, int[] precedingSymbolsCount, Dictionary<char, int> symbolsLessThanCurrent)
        {
            int currentIndex = originalStringIndex;
            for(int i=0; i < encoded.Length; i++)
            {
                yield return encoded[currentIndex];
                currentIndex = precedingSymbolsCount[currentIndex] + symbolsLessThanCurrent[encoded[currentIndex]];
            }
        }

        private static string ReverseAndBuildString(this IEnumerable<char> reversedChars) =>
            string.Join("", reversedChars.Reverse());
    }
}
