using CodeWars.Solutions.RomanNumerals;
using NUnit.Framework;
using Shouldly;

namespace CodeWars.Solutions.Tests.RomanNumerals
{
    [TestFixture]
    public class RomanNumeralsHelperTests
    {
        [TestCase(2017, "MMXVII")]
        [TestCase(1954, "MCMLIV")]
        [TestCase(1990, "MCMXC")]
        [TestCase(990, "CMXC")]
        [TestCase(1, "I")]
        [TestCase(0, "")]
        [TestCase(1666, "MDCLXVI")]
        public void ToRoman__ReturnsCorrectly(int n, string expected)
        {
            RomanNumeralsHelper.ToRoman(n).ShouldBe(expected);
        }

        [TestCase("MMXVII", 2017)]
        [TestCase("MCMLIV", 1954)]
        [TestCase("MCMXC", 1990)]
        [TestCase("CMXC", 990)]
        [TestCase("MDCLXVI", 1666)]
        [TestCase("I", 1)]
        [TestCase("II", 2)]
        [TestCase("III", 3)]
        [TestCase("IV", 4)]
        public void FromRoman__ReturnsCorrectly(string roman, int expected)
        {
            RomanNumeralsHelper.FromRoman(roman).ShouldBe(expected);
        }

        [TestCase("abcd")]
        public void FromRoman_IncorrectValue__ReturnsZero(string value)
        {
            RomanNumeralsHelper.FromRoman(value).ShouldBe(0);
        }
    }
}
