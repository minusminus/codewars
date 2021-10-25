using NUnit.Framework;
using Shouldly;

namespace NGTZ
{
    [TestFixture]
    public class NGTZ
    {
        private readonly GTZ.GTZ _testObj = new GTZ.GTZ();

        [Test]
        public void TestBasic()
        {
            _testObj.going(4).ShouldBe(1.375);
        }

        [Test]
        public void TestBasicFor1()
        {
            _testObj.going(1).ShouldBe(1);
        }

        [Test]
        public void Test01()
        {
            _testObj.going(5).ShouldBe(1.275);
        }

        [Test]
        public void Test02()
        {
            _testObj.going(6).ShouldBe(1.2125);
        }

        [Test]
        public void Test03()
        {
            _testObj.going(7).ShouldBe(1.173214);
        }
    }
}
