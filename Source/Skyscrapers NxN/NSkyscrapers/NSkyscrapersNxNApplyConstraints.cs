using System;
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
                {m1|m2, m1|m2|m3, m4, m1|m2},
                {m4, m1|m2|m3, m1|m2|m3, m1|m2|m3},
                {m1|m2|m3, mAll, m1|m2|m3, m1|m2},
                {m1|m2, m1|m2|m3, m1|m2|m3, m4}
            };

            SkyscraperData data = _pobj.CreateInitialData();
            _pobj.ApplyConstraints(data, clues);
            data.Data.ShouldBe(expected, "first test failed");

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
                {m1|m2|m3, mAll, m1|m2|m3, m1|m2|m3},
                {m4, m1|m2|m3, m1|m2|m3, m1|m2|m3},
                {m1|m2|m3, mAll, m1|m2, mAll}
            };
            data = _pobj.CreateInitialData();
            _pobj.ApplyConstraints(data, clues);
            data.Data.ShouldBe(expected, "second test failed");
        }
    }
}
