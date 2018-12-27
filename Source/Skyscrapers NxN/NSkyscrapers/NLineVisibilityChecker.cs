using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using Skyscrapers;

namespace NSkyscrapers
{
    [TestFixture]
    public class NLineVisibilityChecker
    {
        private LineVisibilityChecker _obj = new LineVisibilityChecker();

        [Test]
        public void Tests()
        {
            _obj.FromLeft(new int[] { 1, 2, 3, 4 }).ShouldBe(4);
            _obj.FromLeft(new int[] { 4, 3, 2, 1 }).ShouldBe(1);
            _obj.FromLeft(new int[] { 3, 1, 2, 4 }).ShouldBe(2);
            _obj.FromLeft(new int[] { 3, 2, 1, 4 }).ShouldBe(2);
            _obj.FromLeft(new int[] { 2, 3, 1, 4 }).ShouldBe(3);
            _obj.FromLeft(new int[] { 1, 3, 2, 4 }).ShouldBe(3);
            _obj.FromLeft(new int[] { 1, 4, 2, 3 }).ShouldBe(2);
        }
    }
}
