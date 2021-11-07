using NUnit.Framework;
using Shouldly;
using CodeWars.Solutions.BurrowsWheelerTransformation;
using System.Linq;
using System;
using System.Collections.Generic;

namespace CodeWars.Solutions.Tests.BurrowsWheelerTransformation
{
    [TestFixture]
    public class BWTTests
    {
        [TestCase("banana", "bnnaaa", 3)]
        [TestCase("banaca", "nbcaaa", 3)]
        [TestCase("banac", "nbcaa", 2)]
        [TestCase("ban ana", "nb naaa", 4)]
        [TestCase("bAnanA", "bnnAaA", 3)]
        [TestCase("bananabar", "nnbbraaaa", 4)]
        [TestCase("Humble Bundle", "e emnllbduuHB", 2)]
        [TestCase("Mellow Yellow", "ww MYeelllloo", 1)]
        public void Encode__EncodesCorrectly(string input, string expectedEncoded, int expectedIndex)
        {
            var result = BWT.Encode(input);
            result.Item1.ShouldBe(expectedEncoded);
            result.Item2.ShouldBe(expectedIndex);
        }

        [TestCase("bnnaaa", 3, "banana")]
        [TestCase("nbcaaa", 3, "banaca")]
        [TestCase("nbcaa", 2, "banac")]
        [TestCase("nnbbraaaa", 4, "bananabar")]
        [TestCase("e emnllbduuHB", 2, "Humble Bundle")]
        [TestCase("ww MYeelllloo", 1, "Mellow Yellow")]
        [TestCase("xxxxxxxxxx", 0, "xxxxxxxxxx")]
        public void Decode__DecodesCorrectly(string encoded, int index, string expected)
        {
            BWT.Decode(encoded, index).ShouldBe(expected);
        }

        [TestCase("banana")]
        [TestCase("Humble Bundle")]
        [TestCase("Mellow Yellow")]
        public void CustomTest(string input)
        {
            var rotations = Enumerable
                .Range(0, input.Length)
                .Select(i => input.Substring(input.Length - i, i) + input.Substring(0, input.Length - i))
                .ToArray();
            Console.WriteLine("-= rotations:");
            foreach (var item in rotations)
                Console.WriteLine(item);

            Console.WriteLine("-= sorted:");
            foreach (var item in rotations.OrderBy(x => x, StringComparer.Ordinal))
                Console.WriteLine(item);

            Console.WriteLine("-= custom sorted:");
            foreach (var item in rotations.OrderBy(x => x, new CustomStringComparer()))
                Console.WriteLine(item);
        }

        private class CustomStringComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                if (x == y) return 0;
                if (x.StartsWith(y)) return 1;
                if (y.StartsWith(x)) return -1;

                return StringComparer.Ordinal.Compare(x, y);
            }
        }
    }
}
