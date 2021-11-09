using NUnit.Framework;
using Shouldly;
using CodeWars.Solutions.BurrowsWheelerTransformation;

namespace CodeWars.Solutions.Tests.BurrowsWheelerTransformation
{
    [TestFixture]
    public class BWTTests
    {
        [TestCase("banana", "nnbaaa", 3)]
        [TestCase("banaca", "cnbaaa", 3)]
        [TestCase("banac", "nbcaa", 2)]
        [TestCase("ban ana", "nnb aaa", 4)]
        [TestCase("bAnanA", "nbnAaA", 3)]
        [TestCase("bananabar", "nnbbraaaa", 4)]
        [TestCase("Humble Bundle", "e emnllbduuHB", 2)]
        [TestCase("Mellow Yellow", "ww MYeelllloo", 1)]
        [TestCase("", "", 0)]
        public void Encode__EncodesCorrectly(string input, string expectedEncoded, int expectedIndex)
        {
            var result = BWT.Encode(input);
            result.Item1.ShouldBe(expectedEncoded);
            result.Item2.ShouldBe(expectedIndex);
        }

        [TestCase("nnbaaa", 3, "banana")]
        [TestCase("cnbaaa", 3, "banaca")]
        [TestCase("nbcaa", 2, "banac")]
        [TestCase("nnbbraaaa", 4, "bananabar")]
        [TestCase("e emnllbduuHB", 2, "Humble Bundle")]
        [TestCase("ww MYeelllloo", 1, "Mellow Yellow")]
        [TestCase("xxxxxxxxxx", 0, "xxxxxxxxxx")]
        [TestCase("", 3, "")]
        public void Decode__DecodesCorrectly(string encoded, int index, string expected)
        {
            BWT.Decode(encoded, index).ShouldBe(expected);
        }
    }
}
