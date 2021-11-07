using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.Solutions.BurrowsWheelerTransformation
{
    /// <summary>
    /// 
    /// </summary>
    public static class BWT
    {
        private class BWTStringComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                if (x == y) return 0;
                if (x.StartsWith(y)) return -1;
                if (y.StartsWith(x)) return 1;

                return StringComparer.Ordinal.Compare(x, y);
            }
        }

        public static Tuple<string, int> Encode(string s) => 
            GetSuffixes(s)
                .SortSuffixes()
                .GetLastColumnAndOriginalTextIndex(s);

        private static IEnumerable<string> GetSuffixes(string s) => 
            Enumerable
                .Range(0, s.Length)
                .Select(i => s.Substring(i, s.Length - i));

        private static IEnumerable<string> SortSuffixes(this IEnumerable<string> suffixes) =>
            suffixes
                .OrderBy(x => x, new BWTStringComparer());

        private static Tuple<string, int> GetLastColumnAndOriginalTextIndex(this IEnumerable<string> sortedSuffixes, string originalString)
        {
            int foundIndex = -1;

            char CheckIndexAndGetLastCharInRow(string suffix, int currentIndex)
            {
                if (suffix.Length == originalString.Length) foundIndex = currentIndex;
                return originalString[(originalString.Length - suffix.Length - 1 + originalString.Length) % originalString.Length];
            }

            return new Tuple<string, int>(
                string.Join("", sortedSuffixes.Select((suffix, i) => CheckIndexAndGetLastCharInRow(suffix, i))), 
                foundIndex);
        }

        public static string Decode(string s, int i)
        {
            return string.Empty;
        }
    }
}
