using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using Skyscrapers;

namespace NSkyscrapers
{
    [TestFixture]
    public class NSkyscrapersPrecalcData
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
        public void FourElemsTest()
        {
            SkyscrapersPrecalcData obj = new SkyscrapersPrecalcData(4);

            CheckGeneratedList(obj.GetList(1), new List<int[]>() { new int[] { 4, 1, 2, 3 }, new int[] { 4, 2, 1, 3 }, new int[] { 4, 1, 3, 2 }, new int[] { 4, 3, 1, 2 }, new int[] { 4, 2, 3, 1 }, new int[] { 4, 3, 2, 1 } });

            CheckGeneratedList(obj.GetList(2), new List<int[]>() { new int[] { 3, 1, 2, 4 }, new int[] { 3, 2, 1, 4 }, new int[] { 2, 1, 4, 3 }, new int[] { 1, 4, 2, 3 }, new int[] { 2, 4, 1, 3 }, new int[] { 3, 1, 4, 2 }, new int[] { 1, 4, 3, 2 }, new int[] { 3, 4, 1, 2 }, new int[] { 3, 2, 4, 1 }, new int[] { 2, 4, 3, 1 }, new int[] { 3, 4, 2, 1 }, });

            CheckGeneratedList(obj.GetList(3), new List<int[]>() { new int[] { 2, 1, 3, 4 }, new int[] { 1, 3, 2, 4 }, new int[] { 2, 3, 1, 4 }, new int[] { 1, 2, 4, 3 }, new int[] { 1, 3, 4, 2 }, new int[] { 2, 3, 4, 1 }, });

            CheckGeneratedList(obj.GetList(4), new List<int[]>() { new int[] { 1, 2, 3, 4 }, });
        }

        [Test]
        public void TwoGetListGivesSameObjects()
        {
            SkyscrapersPrecalcData obj = new SkyscrapersPrecalcData(4);

            List<int[]> l1 = obj.GetList(1);
            List<int[]> l2 = obj.GetList(1);

            (l1 == l2).ShouldBeTrue();
        }
    }
}
