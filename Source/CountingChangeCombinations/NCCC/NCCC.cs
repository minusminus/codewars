using System;
using NUnit.Framework;
using Shouldly;

namespace NCCC
{
    [TestFixture]
    public class NCCC
    {
        private readonly CCC.CCC _pobj = new CCC.CCC();

        [Test]
        public void KataBaseTests()
        {
            _pobj.CountCombinations(4, new[] { 1, 2 }).ShouldBe(3); // => 3
            _pobj.CountCombinations(10, new[] { 5, 2, 3 }).ShouldBe(4); // => 4
            _pobj.CountCombinations(11, new[] { 5, 7 }).ShouldBe(0); //  => 0
        }
    }
}