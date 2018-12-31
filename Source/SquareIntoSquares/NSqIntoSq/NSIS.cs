using System;
using NUnit.Framework;
using Shouldly;
using SqIntoSq;

namespace NSqIntoSq
{
    [TestFixture]
    public class NSIS
    {
        private readonly SIS _testObj = new SIS();

        [Test]
        public void TestSampleKata()
        {
            _testObj.Decompose(11).ShouldBe("1 2 4 10");
        }

        [Test]
        public void Test2()
        {
            _testObj.Decompose(50).ShouldBe("1 3 5 8 49");
        }

        [Test]
        public void TestsFromDiscourse()
        {
            _testObj.Decompose(18351).ShouldBe("1, 3, 4, 5, 13, 191, 18350");
            _testObj.Decompose(38477).ShouldBe("1, 2, 3, 4, 5, 13, 277, 38476");
        }

        [Test]
        public void TestsFromDiscourse_LargeNGap()
        {
            _testObj.Decompose(100000).ShouldBe("1 2 4 13 447 99999");
        }
    }
}