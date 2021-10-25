using System;
using NUnit.Framework;
using Shouldly;

namespace NBE
{
    [TestFixture]
    public class GenerateCoefsTests
    {
        private readonly BE.BE _testObj = new BE.BE();

        [Test]
        public void Test1()
        {
            Int64[] expected = { 1, 1 };
            _testObj.GenerateCoefs(1).ShouldBe(expected);
        }

        [Test]
        public void Test2()
        {
            Int64[] expected = { 1, 2, 1 };
            _testObj.GenerateCoefs(2).ShouldBe(expected);
        }

        [Test]
        public void Test3()
        {
            Int64[] expected = { 1, 3, 3, 1 };
            _testObj.GenerateCoefs(3).ShouldBe(expected);
        }

        [Test]
        public void Test5()
        {
            Int64[] expected = { 1, 5, 10, 10, 5, 1 };
            _testObj.GenerateCoefs(5).ShouldBe(expected);
        }

        [Test]
        public void Test10()
        {
            Int64[] expected = { 1, 10, 45, 120, 210, 252, 210, 120, 45, 10, 1 };
            _testObj.GenerateCoefs(10).ShouldBe(expected);
        }
    }
}
