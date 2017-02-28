using System;
using System.Diagnostics;
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
            //_test.DblLinearBruteforce(n).ShouldBe(expected);
        }

        private void doTestSW(int n, int expected)
        {
            Stopwatch sw = Stopwatch.StartNew();
            _test.DblLinear(n).ShouldBe(expected);
            sw.Stop();
            Console.WriteLine($"elapsed: {sw.ElapsedMilliseconds} ms");
        }

        [Test]
        public void TestKata()
        {
            doTest(10, 22);
            doTest(20, 57);
            doTest(30, 91);
            doTest(50, 175);
        }

        [Test]
        public void TestFirst11()
        {
            doTest(0, 1);
            doTest(1, 3);
            doTest(2, 4);
            doTest(3, 7);
            doTest(4, 9);
            doTest(5, 10);
            doTest(6, 13);
            doTest(7, 15);
            doTest(8, 19);
            doTest(9, 21);
            doTest(10, 22);
            doTest(11, 27);
        }

        [Test]
        public void TestLargeNumbers()
        {
            //doTestSW(1000, 8488);
            //doTestSW(10000, 157654);
            doTestSW(20000, 377625);
        }
    }
}