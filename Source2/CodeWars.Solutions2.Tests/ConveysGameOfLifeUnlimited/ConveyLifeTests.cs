using System;
using CodeWars.Solutions2.ConveysGameOfLifeUnlimited;
using NUnit.Framework;
using Shouldly;

namespace CodeWars.Solutions2.Tests.ConveysGameOfLifeUnlimited
{
    [TestFixture]
    public class ConveyLifeTests
    {
        [Test]
        public void SampleKataTest()
        {
            int[,] testCase = new int[,] { { 1, 0, 0 }, { 0, 1, 1 }, { 1, 1, 0 } };
            int[,] expected = new int[,] { { 0, 1, 0 }, { 0, 0, 1 }, { 1, 1, 1 } };

            int[,] res = ConwayLife.GetGeneration(testCase, 1);

            res.ShouldBe(expected);
        }

        [Test]
        public void EmptyInput__ReturnsEmpty()
        {
            int[,] emptyArray = new int[,] { };

            ConwayLife.GetGeneration(emptyArray, 1).ShouldBe(emptyArray);
        }
    }
}
