using NUnit.Framework;
using Shouldly;

namespace NConsecutiveKPrimes
{
    [TestFixture]
    public class NConsecutiveKPrimes
    {
        private ConsecutiveKPrimes.ConsecutiveKPrimes _pobj = new ConsecutiveKPrimes.ConsecutiveKPrimes();

        [Test]
        public void BasicKataTest()
        {
            _pobj.ConsecKprimes(4, new long[] { 10005, 10030, 10026, 10008, 10016, 10028, 10004 }).ShouldBe(3);
            _pobj.ConsecKprimes(4, new long[] { 10175, 10185, 10180, 10197 }).ShouldBe(3);


            _pobj.ConsecKprimes(6, new long[] { 10098 }).ShouldBe(0);
            _pobj.ConsecKprimes(6, new long[] { 10176, 10164 }).ShouldBe(0);
            _pobj.ConsecKprimes(5, new long[] { 10116, 10108, 10092, 10104, 10100, 10096, 10088 }).ShouldBe(6);
            _pobj.ConsecKprimes(5, new long[] { 10188, 10192, 10212, 10184, 10200, 10208 }).ShouldBe(1);
            _pobj.ConsecKprimes(4, new long[] { 10175, 10185, 10180, 10197 }).ShouldBe(3);
        }
    }
}