using System;
using System.Collections;
using System.Collections.Generic;
using NumberTheory;
using NUnit.Framework;
using Shouldly;

namespace N_NumberTheory
{
    [TestFixture]
    public class PrimeNumbersFactorsTests
    {
        private PrimeNumbersFactors _pobj = new PrimeNumbersFactors();

        [Test]
        public void TestPollardRhoFactorsList()
        {
            //na podstawie http://www.geeksforgeeks.org/pollards-rho-algorithm-prime-factorization/
            _pobj.GetPollardRhoFactorsList(187, 2, 1).ShouldBe(new List<long>() { 11 });
            _pobj.GetPollardRhoFactorsList(187, 110, 183).ShouldBe(new List<long>() { 17 });
            _pobj.GetPollardRhoFactorsList(187, 147, 67).ShouldBe(new List<long>());

            //na podstawie https://en.wikipedia.org/wiki/Pollard%27s_rho_algorithm
            _pobj.GetPollardRhoFactorsList(8051, 5, 1).ShouldBe(new List<long>() { 97, 83 });
            _pobj.GetPollardRhoFactorsList(10403, 2, 1).ShouldBe(new List<long>() { 101, 103 });

            //na podstawie https://www.cs.colorado.edu/~srirams/courses/csci2824-spr14/pollardsRho.html
            _pobj.GetPollardRhoFactorsList(55, 2, 2).ShouldBe(new List<long>());
            _pobj.GetPollardRhoFactorsList(55, 2, 1).ShouldBe(new List<long>() {11, 5});
        }
    }
}
