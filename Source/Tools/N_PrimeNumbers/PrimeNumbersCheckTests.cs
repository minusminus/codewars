using System;
using System.Collections;
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
            //_pobj.IsPrimeMRTest(5).ShouldBeTrue();
            _pobj.IsPrimeMRTest(7).ShouldBeTrue();

            _pobj.IsPrimeMRTest(4).ShouldBeFalse();

            _pobj.IsPrimeMRTest(2000000000).ShouldBeFalse();
            _pobj.IsPrimeMRTest(2000000011).ShouldBeTrue();

            //piewsze 3 liczby Carmichaela
            _pobj.IsPrimeMRTest(561).ShouldBeFalse();
            _pobj.IsPrimeMRTest(1105).ShouldBeFalse();
            _pobj.IsPrimeMRTest(1729).ShouldBeFalse();
        }

        [Test]
        public void Special5Test()
        {
            //_pobj.IsPrimeMRTest(2).ShouldBeTrue();
            _pobj.IsPrimeMRTest(5).ShouldBeTrue();
            //bool b2 = _pobj.MillerRabinTest(2, 5);
            //bool b3 = _pobj.MillerRabinTest(3, 5);
            //bool b5 = _pobj.MillerRabinTest(5, 5);
            //bool b7 = _pobj.MillerRabinTest(7, 5);
            //(b2 && b3 && b5 && b7).ShouldBeFalse();
        }

        [Test]
        public void TestAllPrimesTo1000()
        {
            PrimeNumbersList gen = new PrimeNumbersList();
            BitArray nums = gen.EratosthenesSieve(1000);
            int errors = 0;
            for (int i = 2; i < nums.Count; i++)
            {
                bool b = _pobj.IsPrimeMRTest((long) i);
                //(nums[i] == b).ShouldBeTrue($"{i}: {nums[i]} != {b}");
                if (nums[i] == b)
                {
                    Console.WriteLine($"{i}: {!nums[i]} != {b}");
                    errors++;
                }
            }
            errors.ShouldBe(0);
        }
    }
}
