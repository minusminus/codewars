using NUnit.Framework;
using Shouldly;
using CodeWars.Solutions.BecomeImmortal;

namespace CodeWars.Solutions.Tests.BecomeImmortal
{
    [TestFixture]
    public class BecomeImmortalTests
    {
        const uint NoT = 1000000000u;

        [TestCase(2u, 2u, 0u, NoT, 2u)]
        [TestCase(4u, 4u, 0u, NoT, 24u)]
        [TestCase(8u, 8u, 0u, NoT, 224u)]
        public void Find_FullSquaresOnly__ReturnsCorrectly(uint m, uint n, uint l, uint t, uint expected)
        {
            CodeWars.Solutions.BecomeImmortal.BecomeImmortal.Find(m, n, l, t).ShouldBe(expected);
        }

        [TestCase(2u, 2u, 1u, NoT, 0u)]
        [TestCase(4u, 4u, 1u, NoT, 12u)]
        [TestCase(8u, 8u, 1u, NoT, 168u)]
        [TestCase(2u, 2u, 2u, NoT, 0u)]
        [TestCase(4u, 4u, 2u, NoT, 0u)]
        [TestCase(8u, 8u, 2u, NoT, 120u)]
        public void Find_FullSquares_WithL__ReturnsCorrectly(uint m, uint n, uint l, uint t, uint expected)
        {
            CodeWars.Solutions.BecomeImmortal.BecomeImmortal.Find(m, n, l, t).ShouldBe(expected);
        }

        [TestCase(10u, 8u, 0u, NoT, 224u + 184u)]
        [TestCase(15u, 8u, 0u, NoT, 224u + 644u)]
        [TestCase(10u, 8u, 1u, NoT, 168u + 168u)]
        [TestCase(15u, 8u, 1u, NoT, 168u + 588u)]
        public void Find_WithPartOnRight__ReturnsCorrectly(uint m, uint n, uint l, uint t, uint expected)
        {
            CodeWars.Solutions.BecomeImmortal.BecomeImmortal.Find(m, n, l, t).ShouldBe(expected);
        }

        [TestCase(8u, 10u, 0u, NoT, 224u + 184u)]
        [TestCase(8u, 15u, 0u, NoT, 224u + 644u)]
        [TestCase(8u, 10u, 1u, NoT, 168u + 168u)]
        [TestCase(8u, 15u, 1u, NoT, 168u + 588u)]
        public void Find_WithPartBelow__ReturnsCorrectly(uint m, uint n, uint l, uint t, uint expected)
        {
            CodeWars.Solutions.BecomeImmortal.BecomeImmortal.Find(m, n, l, t).ShouldBe(expected);
        }

        [TestCase(10u, 10u, 0u, NoT, 224u + 184u + 184u + 2u)]
        [TestCase(15u, 15u, 0u, NoT, 224u + 644u + 644u + 168u)]
        public void Find_OnRightAndBelow__ReturnsCorrectly(uint m, uint n, uint l, uint t, uint expected)
        {
            CodeWars.Solutions.BecomeImmortal.BecomeImmortal.Find(m, n, l, t).ShouldBe(expected);
        }

        [TestCase(8u, 5u, 1u, 100u, 5u)]
        public void Find__ReturnsCorrectly(uint m, uint n, uint l, uint t, uint expected)
        {
            CodeWars.Solutions.BecomeImmortal.BecomeImmortal.Find(m, n, l, t).ShouldBe(expected);
        }
    }
}
