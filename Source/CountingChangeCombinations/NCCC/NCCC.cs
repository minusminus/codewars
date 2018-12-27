using System;
using System.Diagnostics;
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

        [Test]
        public void Test1()
        {
            _pobj.CountCombinations(10, new[] {5, 2}).ShouldBe(2);
            _pobj.CountCombinations(11, new[] {5, 2}).ShouldBe(1);
            _pobj.CountCombinations(12, new[] {5, 2}).ShouldBe(2);
            _pobj.CountCombinations(13, new[] {5, 2}).ShouldBe(1);
            _pobj.CountCombinations(14, new[] {5, 2}).ShouldBe(2);
            _pobj.CountCombinations(15, new[] {5, 2}).ShouldBe(2);
            _pobj.CountCombinations(16, new[] {5, 2}).ShouldBe(2);
            _pobj.CountCombinations(22, new[] {5, 2}).ShouldBe(3);
        }

        [Test]
        public void SpeedTest()
        {
            int[] coins = {5, 3, 10, 47, 15, 21};
            const int money = 1000;
            const int expected = 4790284;

            _pobj.CountCombinations(money, coins ).ShouldBe(expected);

            const int cnt = 10000;
            int t = 0;
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 1; i < cnt; i++)
                t=_pobj.CountCombinations(money, coins);
            sw.Stop();
            Console.WriteLine($"elapsed: {sw.ElapsedMilliseconds} ms, avg: {((double)sw.ElapsedMilliseconds / (double)cnt).ToString("F4")}");
            t.ShouldBe(expected);
        }
    }
}