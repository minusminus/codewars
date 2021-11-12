/*
 * Hero Suffix Quest solution
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars.Katas.HeroSuffixQuest
{
    public static class Hero
    {
        private class SuffixStringComparer : IComparer<int>
        {
            private readonly string _baseString;

            public SuffixStringComparer(string baseString)
            {
                _baseString = baseString;
            }

            public int Compare(int x, int y) => 
                string.Compare(_baseString, x, _baseString, y, _baseString.Length - Math.Min(x, y), StringComparison.Ordinal);
        }

        public static string FindPassword(string engravedText, int numberOnTheDoor) => 
            string.IsNullOrEmpty(engravedText) || (numberOnTheDoor < 0) || (engravedText.Length <= numberOnTheDoor)
                ? string.Empty
                : GetSuffixesStartIndexes(engravedText)
                    .SortSuffixes(engravedText)
                    .GetNthOrderedSuffix(numberOnTheDoor, engravedText);

        private static IEnumerable<int> GetSuffixesStartIndexes(string s) =>
            Enumerable.Range(0, s.Length);

        private static IEnumerable<int> SortSuffixes(this IEnumerable<int> suffixesStartIndexes, string originalString) =>
            suffixesStartIndexes.OrderBy(x => x, new SuffixStringComparer(originalString));

        private static string GetNthOrderedSuffix(this IEnumerable<int> suffixesStartIndexes, int n, string originalString) =>
            originalString.Substring(suffixesStartIndexes.Skip(n).First());

    }
}
