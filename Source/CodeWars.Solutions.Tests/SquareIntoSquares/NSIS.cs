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
            _testObj.Decompose(18351).ShouldBe("1 3 4 5 13 191 18350");
            _testObj.Decompose(38477).ShouldBe("1 2 3 4 5 13 277 38476");
        }

        [Test]
        public void TestsFromDiscourse_LargeNGap()
        {
            _testObj.Decompose(100000).ShouldBe("1 2 4 13 447 99999");
        }

        [Test]
        public void LargeNumber()
        {
            _testObj.Decompose(123456789000).ShouldBeNull();
            _testObj.Decompose(120120120120).ShouldBeNull();
            _testObj.Decompose(120120120).ShouldBe("1 4 14 145 15499 120120119");
        }
    }
}