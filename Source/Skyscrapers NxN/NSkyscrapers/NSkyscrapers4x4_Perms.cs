using System;
using NUnit.Framework;
using Shouldly;
using Skyscrapers;

namespace NSkyscrapers
{
    [TestFixture]
    public class NSkyscrapers4x4_Perms
    {
        private readonly Skyscrapers.Skyscrapers4x4_Perms _pobj = new Skyscrapers.Skyscrapers4x4_Perms();

        [Test]
        public void SingleTest()
        {
            var clues = new[]
            {
                0, 0, 1, 2,
                0, 2, 0, 0,
                0, 3, 0, 0,
                0, 1, 0, 0
            };
            var expected = new[]
            {
                new[] {2, 1, 4, 3},
                new[] {3, 4, 1, 2},
                new[] {4, 2, 3, 1},
                new[] {1, 3, 2, 4}
            };
            var actual = _pobj.SolvePuzzle(clues);
            actual.ShouldBe(expected);
        }

        [Test]
        public void BasicKataTests()
        {
            var clues = new[]
            {
                2, 2, 1, 3,
                2, 2, 3, 1,
                1, 2, 2, 3,
                3, 2, 1, 3
            };
            var expected = new[]
            {
                new[] {1, 3, 4, 2},
                new[] {4, 2, 1, 3},
                new[] {3, 4, 2, 1},
                new[] {2, 1, 3, 4}
            };
            var actual = _pobj.SolvePuzzle(clues);
            actual.ShouldBe(expected);

            clues = new[]
            {
                0, 0, 1, 2,
                0, 2, 0, 0,
                0, 3, 0, 0,
                0, 1, 0, 0
            };
            expected = new[]
            {
                new[] {2, 1, 4, 3},
                new[] {3, 4, 1, 2},
                new[] {4, 2, 3, 1},
                new[] {1, 3, 2, 4}
            };
            actual = _pobj.SolvePuzzle(clues);
            actual.ShouldBe(expected);
        }
    }
}
