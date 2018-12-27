using System;
using NUnit.Framework;
using Shouldly;

namespace NJosephusSurvivor
{
    [TestFixture]
    public class NJosephusSurvivor
    {
        private readonly JosephusSurvivor.JosephusSurvivor _pobj = new JosephusSurvivor.JosephusSurvivor();

        [Test]
        public void BasicKataTests()
        {
            _pobj.JosSurvivor(7, 3).ShouldBe(4);
            _pobj.JosSurvivor(11, 19).ShouldBe(10);
            _pobj.JosSurvivor(40, 3).ShouldBe(28);
            _pobj.JosSurvivor(14, 2).ShouldBe(13);
            _pobj.JosSurvivor(100, 1).ShouldBe(100);
            _pobj.JosSurvivor(1, 300).ShouldBe(1);
            _pobj.JosSurvivor(2, 300).ShouldBe(1);
            _pobj.JosSurvivor(5, 300).ShouldBe(1);
            _pobj.JosSurvivor(7, 300).ShouldBe(7);
            _pobj.JosSurvivor(300, 300).ShouldBe(265);
        }

        [Test]
        public void LargeNumTests()
        {
            _pobj.JosSurvivor(1000, 10).ShouldBe(63);
            _pobj.JosSurvivor(10000, 10).ShouldBe(9143);
            _pobj.JosSurvivor(100000, 10).ShouldBe(77328);
            _pobj.JosSurvivor(1000000, 10).ShouldBe(630538);
        }

        private void NonRecIntTest(int n, int k)
        {
            int x = _pobj.JosSurvivorRecursive(n, k);
            Console.WriteLine($"(n={n}, k={k}) should be {x}");
            _pobj.JosSurvivorNonRecursive(n, k).ShouldBe(x);
        }

        [Test]
        public void NonRecTest()
        {
            for (int i = 1; i <= 10; i++)
                NonRecIntTest(i, 3);
        }
    }
}