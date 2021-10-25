using NUnit.Framework;
using Shouldly;

namespace NBE
{
    [TestFixture]
    public class BETests
    {
        private readonly BE.BE _testObj = new BE.BE();

        [Test]
        public void TestBPositive()
        {
            _testObj.Expand("(x+1)^0").ShouldBe("1");
            _testObj.Expand("(x+1)^1").ShouldBe("x+1");
            _testObj.Expand("(x+1)^2").ShouldBe("x^2+2x+1");
        }

        [Test]
        public void TestBNegative()
        {
            _testObj.Expand("(x-1)^0").ShouldBe("1");
            _testObj.Expand("(x-1)^1").ShouldBe("x-1");
            _testObj.Expand("(x-1)^2").ShouldBe("x^2-2x+1");
        }

        [Test]
        public void TestAPositive()
        {
            _testObj.Expand("(5m+3)^4").ShouldBe("625m^4+1500m^3+1350m^2+540m+81");
            _testObj.Expand("(2x-3)^3").ShouldBe("8x^3-36x^2+54x-27");
            _testObj.Expand("(7x-7)^0").ShouldBe("1");
        }

        [Test]
        public void TestANegative()
        {
            _testObj.Expand("(-5m+3)^4").ShouldBe("625m^4-1500m^3+1350m^2-540m+81");
            _testObj.Expand("(-2k-3)^3").ShouldBe("-8k^3-36k^2-54k-27");
            _testObj.Expand("(-7x-7)^0").ShouldBe("1");
        }

        [Test]
        public void TestMinusA()
        {
            _testObj.Expand("(-m+3)^3").ShouldBe("-m^3+9m^2-27m+27");
        }

        [Test]
        public void TestsFromKata()
        {
            _testObj.Expand("(-15g+0)^5").ShouldBe("-759375g^5");

        }
    }
}