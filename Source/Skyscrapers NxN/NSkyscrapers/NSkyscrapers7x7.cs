﻿using System;
using NUnit.Framework;
using Shouldly;
using Skyscrapers;

namespace NSkyscrapers
{
    [TestFixture]
    public class NSkyscrapers7x7
    {
        private readonly Skyscrapers.Skyscrapers7x7 _pobj = new Skyscrapers.Skyscrapers7x7();

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
    }
}
