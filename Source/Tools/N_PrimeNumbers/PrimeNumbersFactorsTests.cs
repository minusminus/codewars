using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NumberTheory;
using NUnit.Framework;
using Shouldly;

namespace N_NumberTheory
{
    [TestFixture]
    public class PrimeNumbersFactorsTests
    {
        private PrimeNumbersFactors _pobj = new PrimeNumbersFactors();

        private void CheckPollardRhoFactorsList(long n, long startx, long c, List<long> correct )
        {
            //Console.WriteLine($"++ n = {n} ++");
            List<long> res = _pobj.GetPollardRhoFactorsList(n, startx, c);
            res.Sort();
            res.ShouldBe(correct);
        }

        [Test]
        public void TestPollardRhoFactorsList()
        {
            //na podstawie http://www.geeksforgeeks.org/pollards-rho-algorithm-prime-factorization/
            CheckPollardRhoFactorsList(187, 2, 1, new List<long>() { 11 });
            CheckPollardRhoFactorsList(187, 110, 183, new List<long>() { 17 });
            CheckPollardRhoFactorsList(187, 147, 67, new List<long>());

            //na podstawie https://en.wikipedia.org/wiki/Pollard%27s_rho_algorithm
            CheckPollardRhoFactorsList(8051, 5, 1, new List<long>() { 83, 97 });
            CheckPollardRhoFactorsList(10403, 2, 1, new List<long>() { 101, 103 });

            //na podstawie https://www.cs.colorado.edu/~srirams/courses/csci2824-spr14/pollardsRho.html
            CheckPollardRhoFactorsList(55, 2, 2, new List<long>());
            CheckPollardRhoFactorsList(55, 2, 1, new List<long>() {5, 11});
        }

        [Test]
        public void TestPollardRhoFactorsList2()
        {
            CheckPollardRhoFactorsList(16, 13, 6, new List<long>() { 8 });
        }

        //[Test]
        //public void TestPollardRhoFactorsListBig()
        //{
        //    CheckPollardRhoFactorsList(123456789987654320, 2558860614600199, 112075433868738794, new List<long>() { 8, 16, 80 });
        //}

        [Test]
        public void TestPollardRhoPrimeFactors()
        {
            //http://www.virtuescience.com/prime-factor-calculator.html

            _pobj.PollardRhoPrimeFactors(55).OrderBy(x => x.Key).ShouldBe(new Dictionary<long, long>() { { 5, 1 }, { 11, 1 } });
            _pobj.PollardRhoPrimeFactors(144).OrderBy(x => x.Key).ShouldBe(new Dictionary<long, long>() { { 2, 4 }, { 3, 2 } });
            _pobj.PollardRhoPrimeFactors(1000000).OrderBy(x => x.Key).ShouldBe(new Dictionary<long, long>() { { 2, 6 }, { 5, 6 } });
            _pobj.PollardRhoPrimeFactors(1000001).OrderBy(x => x.Key).ShouldBe(new Dictionary<long, long>() { { 101, 1 }, { 9901, 1 } });
            _pobj.PollardRhoPrimeFactors(1000000000).OrderBy(x => x.Key).ShouldBe(new Dictionary<long, long>() { { 2, 9 }, { 5, 9 } });
            _pobj.PollardRhoPrimeFactors(1000000001).OrderBy(x => x.Key).ShouldBe(new Dictionary<long, long>() { { 7, 1 }, { 11, 1 }, {13, 1}, {19, 1}, { 52579, 1} });
            _pobj.PollardRhoPrimeFactors(123456789).OrderBy(x => x.Key).ShouldBe(new Dictionary<long, long>() { { 3, 2 }, { 3607, 1 }, {3803, 1} });

            _pobj.PollardRhoPrimeFactors(2147483646).OrderBy(x => x.Key).ShouldBe(new Dictionary<long, long>() { { 2, 1 }, { 3, 2 }, { 7, 1 }, {11, 1}, {31, 1}, {151, 1}, {331, 1} });
        }

        //[Test]
        //public void TestPollardRhoPrimeFactorsBig()
        //{
        //    _pobj.PollardRhoPrimeFactors(123456789987654320).OrderBy(x => x.Key).ShouldBe(new Dictionary<long, long>() { { 2, 4 }, { 5, 1 }, { 1543209874845679, 1 } });
        //}
    }
}
