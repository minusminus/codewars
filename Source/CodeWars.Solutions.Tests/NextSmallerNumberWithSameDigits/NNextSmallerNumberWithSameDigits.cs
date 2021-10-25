using NUnit.Framework;
using Shouldly;

namespace NNextSmallerNumberWithSameDigits
{
    [TestFixture]
    public class NNextSmallerNumberWithSameDigits
    {
        private readonly NextSmallerNumberWithSameDigits.NextSmallerNumberWithSameDigits _pobj =
            new NextSmallerNumberWithSameDigits.NextSmallerNumberWithSameDigits();

        [TestCase(21, ExpectedResult = 12)]
        [TestCase(907, ExpectedResult = 790)]
        [TestCase(531, ExpectedResult = 513)]
        [TestCase(1027, ExpectedResult = -1)]
        [TestCase(441, ExpectedResult = 414)]
        [TestCase(123456798, ExpectedResult = 123456789)]
        public long BasicKataTests(long n)
        {
            return _pobj.NextSmaller(n);
        }

        [Test]
        public void TestsRecurence()
        {
            _pobj.NextSmaller(315).ShouldBe(153);
        }

        [Test]
        public void NoLeadingZero()
        {
            _pobj.NextSmaller(1027).ShouldBe(-1);
        }

        [Test]
        public void SomeTest()
        {
            //Expected: 51226262 627551
            //But was:  51226262 576521
            //Expected: 51226262 627551
            //But was:  51226262 565127
            _pobj.NextSmaller(51226262651257).ShouldBe(51226262627551);
            _pobj.NextSmaller(66554433222).ShouldBe(66554432322);
        }
    }
}