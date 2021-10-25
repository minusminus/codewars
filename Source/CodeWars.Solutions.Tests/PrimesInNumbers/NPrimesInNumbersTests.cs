using NUnit.Framework;
using Shouldly;

namespace N_PrimesInNumbers
{
    [TestFixture]
    public class NPrimesInNumbersTests
    {
        private PrimesInNumbers.PrimesInNumbers _pobj = new PrimesInNumbers.PrimesInNumbers();

        [Test]
        public void KataBasicTest()
        {
            _pobj.factors(86240).ShouldBe("(2**5)(5)(7**2)(11)");
            _pobj.factors(7775460).ShouldBe("(2**2)(3**3)(5)(7)(11**2)(17)");
        }

        [Test]
        public void TestForPrimes()
        {
            _pobj.factors(5).ShouldBe("(5)");
            _pobj.factors(7919).ShouldBe("(7919)");
        }
    }
}