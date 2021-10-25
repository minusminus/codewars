using System;
using NUnit.Framework;
using Shouldly;
using Skyscrapers;

namespace NSkyscrapers
{
    [TestFixture]
    public class NSkyscraperData
    {
        private const int N = 4;

        private readonly int[,] _testdata = new int[N, N]
        {
            {1, 1, 1, 1},
            {2, 2, 2, 2},
            {3, 3, 3, 3},
            {4, 4, 4, 4}
        };


        [Test]
        public void CreateSizesTest()
        {
            SkyscraperData obj = new SkyscraperData(N);
            obj.Data.Length.ShouldBe(N*N);
        }

        [Test]
        public void CreateFromTblTest()
        {
            SkyscraperData obj = new SkyscraperData(N, _testdata);
            obj.Data.Length.ShouldBe(N*N);
            obj.Data.ShouldBe(_testdata);
        }

        [Test]
        public void RotaetRightTest()
        {
            int[,] rotated1 = new int[N, N]
            {
                {4, 3, 2, 1},
                {4, 3, 2, 1},
                {4, 3, 2, 1},
                {4, 3, 2, 1}
            };
            int[,] rotated2 = new int[N, N]
            {
                {4, 4, 4, 4},
                {3, 3, 3, 3},
                {2, 2, 2, 2},
                {1, 1, 1, 1}
            };
            int[,] rotated3 = new int[N, N]
            {
                {1, 2, 3, 4},
                {1, 2, 3, 4},
                {1, 2, 3, 4},
                {1, 2, 3, 4}
            };

            SkyscraperData obj = new SkyscraperData(N, _testdata);
            obj.RotateRight();
            obj.Data.ShouldBe(rotated1);
            obj.RotateRight();
            obj.Data.ShouldBe(rotated2);
            obj.RotateRight();
            obj.Data.ShouldBe(rotated3);
            obj.RotateRight();
            obj.Data.ShouldBe(_testdata);
        }
    }
}
