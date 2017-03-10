using System;
using NumberTheory;
using NUnit.Framework;
using Shouldly;

namespace N_NumberTheory
{
    [TestFixture]
    public class NumberTheoryTests
    {
        //private NumberTheory.NumbersTheory _obj = new NumberTheory.NumbersTheory();

        [Test]
        public void TestGCDBinary()
        {
            NumbersTheory.GCDBinary(0, 0).ShouldBe(0);
            NumbersTheory.GCDBinary(10, 0).ShouldBe(10);
            NumbersTheory.GCDBinary(0, 10).ShouldBe(10);

            NumbersTheory.GCDBinary(1, 10).ShouldBe(1);
            NumbersTheory.GCDBinary(1, 100000000000).ShouldBe(1);
            NumbersTheory.GCDBinary(100000000000, 1).ShouldBe(1);

            NumbersTheory.GCDBinary(48, 180).ShouldBe(12);
            NumbersTheory.GCDBinary(180, 48).ShouldBe(12);
        }

        [Test]
        public void TestLCM()
        {
            NumbersTheory.LCM(0, 56).ShouldBe(0);
            NumbersTheory.LCM(1, 56).ShouldBe(56);

            NumbersTheory.LCM(42, 56).ShouldBe(168);
            NumbersTheory.LCM(192, 348).ShouldBe(5568);
        }

        [Test]
        public void TestExpMod()
        {
            NumbersTheory.ExpMod(2, 0, 10).ShouldBe(1);

            NumbersTheory.ExpMod(1, 10, 10).ShouldBe(1);
            NumbersTheory.ExpMod(2, 100, 10).ShouldBe(6);
            NumbersTheory.ExpMod(3, 17, 123).ShouldBe(3);
            NumbersTheory.ExpMod(123456789, 2, 321).ShouldBe(90);
            NumbersTheory.ExpMod(1 << 32, (1 << 32) - 1, 999).ShouldBe(1);

            NumbersTheory.ExpMod(2, 32, 10).ShouldBe(6);
            NumbersTheory.ExpMod(2, 32, 2).ShouldBe(0);
        }
    }
}
