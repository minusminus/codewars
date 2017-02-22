using System;
using NUnit.Framework;
using Shouldly;

namespace N_RomanNumerals
{
    [TestFixture]
    public class RomanNumeralsTests
    {
        private readonly RomanNumerals.RomanNumerals _testNumerals = new RomanNumerals.RomanNumerals();

        //[SetUp]
        //public void SetUp()
        //{
        //}


        [Test]
        public void TestEncoding()
        {
            _testNumerals.Encode(2017).ShouldBe("MMXVII");
            _testNumerals.Encode(1954).ShouldBe("MCMLIV");
            _testNumerals.Encode(1990).ShouldBe("MCMXC");
            _testNumerals.Encode(990).ShouldBe("CMXC");
            _testNumerals.Encode(1).ShouldBe("I");
            _testNumerals.Encode(0).ShouldBe("");
        }
    }
}