using System;
using NUnit.Framework;
using Shouldly;

namespace N_TwiceLinear
{
    [TestFixture]
    public class TwiceLinearTests
    {
        private readonly TwiceLinear.TwiceLinear _test = new TwiceLinear.TwiceLinear();

        private void doTest(int n, int expected)
        {
            _test.DblLinear(n).ShouldBe(expected);
        }

        [Test]
        public void TestKata()
        {
            doTest(10, 27);
            doTest(20, 57);
            doTest(30, 91);
            doTest(40, 175);
        }
    }
}