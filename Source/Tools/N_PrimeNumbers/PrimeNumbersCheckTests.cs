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
            _pobj.IsPrimeMRTest(5).ShouldBeTrue();
            _pobj.IsPrimeMRTest(7).ShouldBeTrue();

            _pobj.IsPrimeMRTest(4).ShouldBeFalse();

            _pobj.IsPrimeMRTest(2000000000).ShouldBeFalse();
            _pobj.IsPrimeMRTest(2000000011).ShouldBeTrue();

            //piewsze 3 liczby Carmichaela
            _pobj.IsPrimeMRTest(561).ShouldBeFalse();
            _pobj.IsPrimeMRTest(1105).ShouldBeFalse();
            _pobj.IsPrimeMRTest(1729).ShouldBeFalse();

            _pobj.IsPrimeMRTest(2147483647).ShouldBeTrue(); //2^31 - 1
        }

        //[Test]
        //public void TestPrimesMillerRabinCheckBig()
        //{
        //    //_pobj.IsPrimeMRTest(123456789987654320).ShouldBeFalse();
        //    //_pobj.IsPrimeMRTest(1543209874845679).ShouldBeTrue();
        //    //_pobj.IsPrimeMRTest(7716049374228395).ShouldBeFalse();
        //}

        [Test]
        public void SpecialTest()
        {
            _pobj.IsPrimeMRTest(5).ShouldBeTrue();
            //_pobj.IsPrimeMRTest(561).ShouldBeTrue();
        }

        private void PrimesMRCheckToN(int n)
        {
            PrimeNumbersList gen = new PrimeNumbersList();
            BitArray nums = gen.EratosthenesSieve(n);
            int errors = 0;
            for (int i = 2; i < nums.Count; i++)
            {
                bool b = _pobj.IsPrimeMRTest((long)i);
                if (!nums[i] != b)
                {
                    Console.WriteLine($"{i}: {!nums[i]} != {b}");
                    errors++;
                }
            }
            errors.ShouldBe(0);
        }

        [Test]
        public void TestAllPrimesMRCheckTo1000()
        {
            PrimesMRCheckToN(1000);
        }

        [Test]
        public void TestAllPrimesMRCheckTo1mln()
        {
            PrimesMRCheckToN(1000000);
        }
    }
}
