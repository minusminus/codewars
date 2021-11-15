using NUnit.Framework;
using Shouldly;
using CodeWars.Solutions.BecomeImmortal;
using System;

namespace CodeWars.Solutions.Tests.BecomeImmortal
{
    [TestFixture]
    public class BecomeImmortalTests
    {
        const long NoT = 1000000000L;

        [TestCase(2L, 2L, 0L, NoT, 2L)]
        [TestCase(4L, 4L, 0L, NoT, 24L)]
        [TestCase(8L, 8L, 0L, NoT, 224L)]
        public void ElderAge_FullSquaresOnly__ReturnsCorrectly(long m, long n, long l, long t, long expected)
        {
            CodeWars.Solutions.BecomeImmortal.BecomeImmortal.ElderAge(m, n, l, t).ShouldBe(expected);
        }

        [TestCase(2L, 2L, 1L, NoT, 0L)]
        [TestCase(4L, 4L, 1L, NoT, 12L)]
        [TestCase(8L, 8L, 1L, NoT, 168L)]
        [TestCase(2L, 2L, 2L, NoT, 0L)]
        [TestCase(4L, 4L, 2L, NoT, 4L)]
        [TestCase(8L, 8L, 2L, NoT, 120L)]
        public void ElderAge_FullSquares_WithL__ReturnsCorrectly(long m, long n, long l, long t, long expected)
        {
            CodeWars.Solutions.BecomeImmortal.BecomeImmortal.ElderAge(m, n, l, t).ShouldBe(expected);
        }

        [TestCase(10L, 8L, 0L, NoT, 224L + 184L)]
        [TestCase(15L, 8L, 0L, NoT, 224L + 644L)]
        [TestCase(10L, 8L, 1L, NoT, 168L + 168L)]
        [TestCase(15L, 8L, 1L, NoT, 168L + 588L)]
        [TestCase(5L, 4L, 0L, NoT, 24L + 22L)]
        [TestCase(5L, 4L, 1L, NoT, 12L + 18L)]
        public void ElderAge_WithPartOnRight__ReturnsCorrectly(long m, long n, long l, long t, long expected)
        {
            CodeWars.Solutions.BecomeImmortal.BecomeImmortal.ElderAge(m, n, l, t).ShouldBe(expected);
        }

        [TestCase(8L, 10L, 0L, NoT, 224L + 184L)]
        [TestCase(8L, 15L, 0L, NoT, 224L + 644L)]
        [TestCase(8L, 10L, 1L, NoT, 168L + 168L)]
        [TestCase(8L, 15L, 1L, NoT, 168L + 588L)]
        [TestCase(4L, 5L, 0L, NoT, 24L + 22L)]
        [TestCase(4L, 5L, 1L, NoT, 12L + 18L)]
        public void ElderAge_WithPartBelow__ReturnsCorrectly(long m, long n, long l, long t, long expected)
        {
            CodeWars.Solutions.BecomeImmortal.BecomeImmortal.ElderAge(m, n, l, t).ShouldBe(expected);
        }

        [TestCase(10L, 10L, 0L, NoT, 224L + 184L + 184L + 2L)]
        [TestCase(15L, 15L, 0L, NoT, 224L + 644L + 644L + 168L)]
        [TestCase(5L, 45L, 3L, NoT, 4323L)]
        [TestCase(545L, 435L, 342L, NoT, 11808528L)]
        public void ElderAge_OnRightAndBelow__ReturnsCorrectly(long m, long n, long l, long t, long expected)
        {
            CodeWars.Solutions.BecomeImmortal.BecomeImmortal.ElderAge(m, n, l, t).ShouldBe(expected);
        }

        //[Ignore("tymczasowo wyłączone")]
        [TestCase(8L, 5L, 1L, 100L, 5L)]
        [TestCase(8L, 8L, 0L, 100007L, 224L)]
        [TestCase(25L, 31L, 0L, 100007L, 11925L)]
        [TestCase(5L, 45L, 3L, 1000007L, 4323L)]
        [TestCase(31L, 39L, 7L, 2345L, 1586L)]
        [TestCase(545L, 435L, 342L, 1000007L, 808451L)]
        //[TestCase(28827050410L, 35165045587L, 7109602L, 13719506L, 5456283L)]
        public void ElderAge__ReturnsCorrectly(long m, long n, long l, long t, long expected)
        {
            CodeWars.Solutions.BecomeImmortal.BecomeImmortal.ElderAge(m, n, l, t).ShouldBe(expected);
        }

        [TestCase(8L, 8L, 0L, NoT)]
        [TestCase(545L, 435L, 342L, NoT)]
        [TestCase(545L, 435L, 342L, 1000007L)]
        //[TestCase(28827050410L, 35165045587L, 7109602L, 13719506L)]
        public void ElderAge_CompareToBruteForce__ReturnsCorrectly(long m, long n, long l, long t)
        {
            long orig = CodeWars.Solutions.BecomeImmortal.BecomeImmortal.ElderAge(m, n, l, t);
            long bruteforce = CodeWars.Solutions.BecomeImmortal.BecomeImmortalBF.ElderAge(m, n, l, t);
            orig.ShouldBe(bruteforce);
        }

        [TestCase(28827050410L, 35165045587L, 7109602L, 13719506L, 5456283L)]
        [Timeout(1000)]
        public void ElderAge__RequiredSpeedTest(long m, long n, long l, long t, long expected)
        {
            CodeWars.Solutions.BecomeImmortal.BecomeImmortal.ElderAge(m, n, l, t).ShouldBe(expected);
        }
    }
}
