using NUnit.Framework;
using Shouldly;
using CodeWars.Solutions.SumStringsAsNumbers;

namespace CodeWars.Solutions.Tests.SumStringsAsNumbers
{
    [TestFixture]
    public class SumStringsTests
    {
        [TestCase("1", "2", "3")]
        [TestCase("2", "1", "3")]
        [TestCase("10", "20", "30")]
        [TestCase("19", "2", "21")]
        [TestCase("2", "19", "21")]
        [TestCase("9", "9", "18")]
        [TestCase("19", "9", "28")]
        [TestCase("8797", "45", "8842")]
        [TestCase("800", "9567", "10367")]
        [TestCase("00103", "08567", "8670")]
        [TestCase("712569312664357328695151392", "8100824045303269669937", "712577413488402631964821329")]
        [TestCase("50095301248058391139327916261", "81055900096023504197206408605", "131151201344081895336534324866")]
        public void SumString__SumsCorrectly(string s1, string s2, string expected)
        {
            SumStrings.sumStrings(s1, s2).ShouldBe(expected);
        }
    }
}
