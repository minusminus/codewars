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
            _testNumerals.Encode(1666).ShouldBe("MDCLXVI");
        }

        [Test]
        public void TestDecoding()
        {
            _testNumerals.Decode("MMXVII").ShouldBe(2017, "MMXVII");
            _testNumerals.Decode("MCMLIV").ShouldBe(1954, "MCMLIV");
            _testNumerals.Decode("MCMXC").ShouldBe(1990, "MCMXC");
            _testNumerals.Decode("CMXC").ShouldBe(990, "CMXC");
            _testNumerals.Decode("I").ShouldBe(1, "I");
            _testNumerals.Decode("MDCLXVI").ShouldBe(1666, "MDCLXVI");
        }
    }
}