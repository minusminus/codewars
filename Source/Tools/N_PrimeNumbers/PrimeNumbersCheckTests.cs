using System;
using NumberTheory;
using NUnit.Framework;
using Shouldly;

namespace N_NumberTheory
{
    [TestFixture]
    public class PrimeNumbersCheckTests
    {
        private NumberTheory.PrimeNumbersCheck _pobj = new NumberTheory.PrimeNumbersCheck();

        [Test]
        public void TestPrimesMillerRabinCheck()
        {
            _pobj.IsPrimeMRTest(2).ShouldBeTrue();
            _pobj.IsPrimeMRTest(3).ShouldBeTrue();
            _pobj.IsPrimeMRTest(5).ShouldBeTrue();
            _pobj.IsPrimeMRTest(7).ShouldBeTrue();

            _pobj.IsPrimeMRTest(4).ShouldBeFalse();

            _pobj.IsPrimeMRTest(2000000000).ShouldBeFalse();
            _pobj.IsPrimeMRTest(2000000011).ShouldBeTrue();

            //piewsze 3 liczby Carmichaela
            _pobj.IsPrimeMRTest(561).ShouldBeFalse();
            _pobj.IsPrimeMRTest(1105).ShouldBeFalse();
            _pobj.IsPrimeMRTest(1729).ShouldBeFalse();
        }
    }
}
