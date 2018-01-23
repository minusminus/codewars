using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using Skyscrapers;

namespace NSkyscrapers
{
    [TestFixture]
    public class NSkyscrapersNxNApplyConstraints
    {
        private readonly Skyscrapers.SkyscrapersNxN _pobj = new Skyscrapers.SkyscrapersNxN(4);

        private const int m1 = 1;
        private const int m2 = 2;
        private const int m3 = 4;
        private const int m4 = 8;
        private const int mAll = m1 | m2 | m3 | m4;

        [Test]
        public void ApplyConstraintsTest()
        {
            var clues = new int[]
            {
                2, 2, 1, 3,
                2, 2, 3, 1,
                1, 2, 2, 3,
                3, 2, 1, 3
            };
            var expected = new int[,]
            {
                {m1|m2, m3, m4, m1|m2},
                {m4, m1|m2, m1|m2, m3},
                {m3, m4, m1|m2, m1|m2},
                {m1|m2, m1|m2, m3, m4}
            };

            SkyscraperData data = _pobj.CreateInitialData();
            _pobj.ApplyConstraints(data, clues);
            data.Data.ShouldBe(expected, "first test failed");
            data.Rows[0].ShouldBe(new List<int>() {0, 3});
            data.Rows[1].ShouldBe(new List<int>() {1, 2});
            data.Rows[2].ShouldBe(new List<int>() {2, 3});
            data.Rows[3].ShouldBe(new List<int>() {0, 1});
            data.Cols[0].ShouldBe(new List<int>() {0, 3});
            data.Cols[1].ShouldBe(new List<int>() {1, 3});
            data.Cols[2].ShouldBe(new List<int>() {1, 2});
            data.Cols[3].ShouldBe(new List<int>() {0, 2});

            clues = new[]
            {
                0, 0, 1, 2,
                0, 2, 0, 0,
                0, 3, 0, 0,
                0, 1, 0, 0
            };
            expected = new int[,]
            {
                {m1|m2|m3, m1|m2|m3, m4, m1|m2|m3},
                {m1|m2|m3, m4, m1|m2|m3, m1|m2|m3},
                {m4, m1|m2|m3, m1|m2|m3, m1|m2|m3},
                {m1|m2|m3, m1|m2|m3, m1|m2, m4}
            };
            data = _pobj.CreateInitialData();
            _pobj.ApplyConstraints(data, clues);
            data.Data.ShouldBe(expected, "second test failed");
            data.Rows[0].ShouldBe(new List<int>() { 0, 1, 3 });
            data.Rows[1].ShouldBe(new List<int>() { 0, 2, 3 });
            data.Rows[2].ShouldBe(new List<int>() { 1, 2, 3 });
            data.Rows[3].ShouldBe(new List<int>() { 0, 1, 2 });
            data.Cols[0].ShouldBe(new List<int>() { 0, 1, 3 });
            data.Cols[1].ShouldBe(new List<int>() { 0, 2, 3 });
            data.Cols[2].ShouldBe(new List<int>() { 1, 2, 3 });
            data.Cols[3].ShouldBe(new List<int>() { 0, 1, 2 });
        }
    }
}
