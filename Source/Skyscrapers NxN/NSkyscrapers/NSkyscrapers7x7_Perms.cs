using System;
using NUnit.Framework;
using Shouldly;
using Skyscrapers;

namespace NSkyscrapers
{
    [TestFixture]
    public class NSkyscrapers7x7_Perms
    {
        private readonly Skyscrapers.Skyscrapers7x7_Perms _pobj = new Skyscrapers.Skyscrapers7x7_Perms();
        private void SolveTestPuzzle(int[] clues, int[][] expected)
        {
            var actual = _pobj.SolvePuzzle(clues);
            actual.ShouldBe(expected);
        }

        [Test]
        public void Test_1_Medium()
        {
            var clues = new[]
            {
                7, 0, 0, 0, 2, 2, 3,
                0, 0, 3, 0, 0, 0, 0,
                3, 0, 3, 0, 0, 5, 0,
                0, 0, 0, 0, 5, 0, 4
            };
            var expected = new[]
            {
                new[] {1, 5, 6, 7, 4, 3, 2},
                new[] {2, 7, 4, 5, 3, 1, 6},
                new[] {3, 4, 5, 6, 7, 2, 1},
                new[] {4, 6, 3, 1, 2, 7, 5},
                new[] {5, 3, 1, 2, 6, 4, 7},
                new[] {6, 2, 7, 3, 1, 5, 4},
                new[] {7, 1, 2, 4, 5, 6, 3}
            };

            SolveTestPuzzle(clues, expected);
        }

        [Test]
        public void Test_2_VeryHard()
        {
            var clues = new[]
            {
                0, 2, 3, 0, 2, 0, 0,
                5, 0, 4, 5, 0, 4, 0,
                0, 4, 2, 0, 0, 0, 6,
                5, 2, 2, 2, 2, 4, 1
            };
            var expected = new[]
            {
                new[] {7, 6, 2, 1, 5, 4, 3},
                new[] {1, 3, 5, 4, 2, 7, 6},
                new[] {6, 5, 4, 7, 3, 2, 1},
                new[] {5, 1, 7, 6, 4, 3, 2},
                new[] {4, 2, 1, 3, 7, 6, 5},
                new[] {3, 7, 6, 2, 1, 5, 4},
                new[] {2, 4, 3, 5, 6, 1, 7}
            };

            SolveTestPuzzle(clues, expected);
        }

        [Test]
        public void Test_Attempt_1_Hard()
        {
            var clues = new[]
            {
                6, 4, 0, 2, 0, 0, 3, 0, 3, 3, 3, 0, 0, 4, 0, 5, 0, 5, 0, 2, 0, 0, 0, 0, 4, 0, 0, 3
            };
            var expected = new[]
            {
                new[] {2, 1, 6, 4, 3, 7, 5},
                new[] {3, 2, 5, 7, 4, 6, 1},
                new[] {4, 6, 7, 5, 1, 2, 3},
                new[] {1, 3, 2, 6, 7, 5, 4},
                new[] {5, 7, 1, 3, 2, 4, 6},
                new[] {6, 4, 3, 2, 5, 1, 7},
                new[] {7, 5, 4, 1, 6, 3, 2}
            };
            SolveTestPuzzle(clues, expected);
        }

        [Test]
        public void Test_Attempt_2_Hard()
        {
            var clues = new[]
            {
                0, 0, 0, 5, 0, 0, 3, 0, 6, 3, 4, 0, 0, 0, 3, 0, 0, 0, 2, 4, 0, 2, 6, 2, 2, 2, 0, 0
            };
            var expected = new[]
            {
                new[] {3, 5, 6, 1, 7, 2, 4},
                new[] {7, 6, 5, 2, 4, 3, 1},
                new[] {2, 7, 1, 3, 6, 4, 5},
                new[] {4, 3, 7, 6, 1, 5, 2},
                new[] {6, 4, 2, 5, 3, 1, 7},
                new[] {1, 2, 3, 4, 5, 7, 6},
                new[] {5, 1, 4, 7, 2, 6, 3}
            };
            SolveTestPuzzle(clues, expected);
        }

        [Test]
        public void Test_Attempt_3_VeryHard()
        {
            var clues = new[]
            {
                0, 0, 5, 0, 0, 0, 6, 4, 0, 0, 2, 0, 2, 0, 0, 5, 2, 0, 0, 0, 5, 0, 3, 0, 5, 0, 0, 3
            };
            var expected = new[]
            {
                new[] {3, 4, 1, 7, 6, 5, 2},
                new[] {7, 1, 2, 5, 4, 6, 3},
                new[] {6, 3, 5, 2, 1, 7, 4},
                new[] {1, 2, 3, 6, 7, 4, 5},
                new[] {5, 7, 6, 4, 2, 3, 1},
                new[] {4, 5, 7, 1, 3, 2, 6},
                new[] {2, 6, 4, 3, 5, 1, 7}
            };
            SolveTestPuzzle(clues, expected);
        }
    }
}
