using System;
using NumberTheory;
using NUnit.Framework;
using Shouldly;

namespace N_NumberTheory
{
    [TestFixture]
    public class NumberTheoryTests
    {
        private NumberTheory.NumbersTheory _obj = new NumberTheory.NumbersTheory();

        [Test]
        public void TestGCDBinary()
        {
            _obj.GCDBinary(0, 0).ShouldBe(0);
            _obj.GCDBinary(10, 0).ShouldBe(10);
            _obj.GCDBinary(0, 10).ShouldBe(10);

            _obj.GCDBinary(1, 10).ShouldBe(1);
            _obj.GCDBinary(1, 100000000000).ShouldBe(1);
            _obj.GCDBinary(100000000000, 1).ShouldBe(1);

            _obj.GCDBinary(48, 180).ShouldBe(12);
            _obj.GCDBinary(180, 48).ShouldBe(12);
        }

        [Test]
        public void TestLCM()
        {
            _obj.LCM(0, 56).ShouldBe(0);
            _obj.LCM(1, 56).ShouldBe(56);

            _obj.LCM(42, 56).ShouldBe(168);
            _obj.LCM(192, 348).ShouldBe(5568);
        }
    }
}
