using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using Skyscrapers;

namespace NSkyscrapers
{
    [TestFixture]
    public class NPermutationGen
    {
        private void CheckGeneratedList(List<int[]> check, List<int[]> expected)
        {
            check.Count.ShouldBe(expected.Count, "rozne dlugosci");
            for (int i = 0; i < check.Count; i++)
            {
                check[i].Length.ShouldBe(expected[i].Length, $"rozne dlugosci tablic {i}");
                for (int j = 0; j < check[i].Length; j++)
                    check[i][j].ShouldBe(expected[i][j], $"niezgodne elementy {i},{j}");
            }
        }

        [Test]
        public void FourElemsPermTest()
        {
            PermutationGen obj = new PermutationGen();

            int[] testdata = new int[4] {1, 2, 3, 4};
            List<int[]> resList = new List<int[]>();
            List<int[]> expectedList = new List<int[]>()
            {
                new int[]{1, 2, 3, 4}, new int[]{2, 1, 3, 4}, new int[]{1, 3, 2, 4}, new int[]{3, 1, 2, 4}, new int[]{2, 3, 1, 4}, new int[]{3, 2, 1, 4},
                new int[]{1, 2, 4, 3}, new int[]{2, 1, 4, 3}, new int[]{1, 4, 2, 3}, new int[]{4, 1, 2, 3}, new int[]{2, 4, 1, 3}, new int[]{4, 2, 1, 3},
                new int[]{1, 3, 4, 2}, new int[]{3, 1, 4, 2}, new int[]{1, 4, 3, 2}, new int[]{4, 1, 3, 2}, new int[]{3, 4, 1, 2}, new int[]{4, 3, 1, 2},
                new int[]{2, 3, 4, 1}, new int[]{3, 2, 4, 1}, new int[]{2, 4, 3, 1}, new int[]{4, 2, 3, 1}, new int[]{3, 4, 2, 1}, new int[]{4, 3, 2, 1}
            };

            obj.Gen(testdata, resList);
            CheckGeneratedList(resList, expectedList);
        }
    }
}
