using System;
using NUnit.Framework;
using TakeANumberAndSumItsDigitsRaisedToconsecutivePowers;

namespace N_TakeANumberAndSumItsDigitsRaisedToconsecutivePowers
{
    [TestFixture]
    public class SumDigPowerTests
    {
        private static string Array2String(long[] list)
        {
            return "[" + string.Join(", ", list) + "]";
        }
        private static void testing(long a, long b, long[] res)
        {
            Assert.AreEqual(Array2String(res),
                            Array2String(SumDigPower.SumDigPow(a, b)));
        }

        [Test]
        public static void test1()
        {
            Console.WriteLine("Basic Tests SumDigPow");
            testing(1, 10, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            testing(1, 100, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 89 });
            testing(10, 100, new long[] { 89 });
            testing(90, 100, new long[] { });
            testing(90, 150, new long[] { 135 });
            testing(50, 150, new long[] { 89, 135 });
            testing(10, 150, new long[] { 89, 135 });
        }

        [Test]
        public static void test2()
        {
            Console.WriteLine("Second Tests SumDigPow");
            testing(89, 135, new long[] { 89, 135 });
            testing(90, 134, new long[] { });
            testing(89, 134, new long[] { 89 });
            testing(90, 135, new long[] { 135 });
        }

        [Test]
        public static void testLargeValues()
        {
            Console.WriteLine("Large Values Tests SumDigPow");
            testing(1, 1000000, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 89, 135, 175, 518, 598, 1306, 1676, 2427 });
            //testing(1, 1000000000, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 89, 135, 175, 518, 598, 1306, 1676, 2427, 2646798 });
        }
    }
}