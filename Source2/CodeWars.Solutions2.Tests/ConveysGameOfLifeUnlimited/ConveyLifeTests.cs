﻿using CodeWars.Solutions2.ConveysGameOfLifeUnlimited;

namespace CodeWars.Solutions2.Tests.ConveysGameOfLifeUnlimited;

[TestFixture]
public class ConveyLifeTests
{
    [Test]
    public void SampleKataTest()
    {
        int[,] testCase = new int[,] { { 1, 0, 0 }, { 0, 1, 1 }, { 1, 1, 0 } };
        int[,] expected = new int[,] { { 0, 1, 0 }, { 0, 0, 1 }, { 1, 1, 1 } };

        ConwayLife.GetGeneration(testCase, 1).ShouldBe(expected);
    }

    [Test]
    public void TopLineWithOnes__ResultResized()
    {
        int[,] testCase = new int[,] { { 1, 1, 1 }, { 0, 1, 1 }, { 1, 1, 0 } };
        int[,] expected = new int[,] { { 0, 1, 0 }, { 1, 0, 1 }, { 0, 0, 0 }, { 1, 1, 1 } };

        ConwayLife.GetGeneration(testCase, 1).ShouldBe(expected);
    }

    [Test]
    public void BottomLineWithOnes__ResultResized()
    {
        int[,] testCase = new int[,] { { 1, 0, 0 }, { 0, 1, 1 }, { 1, 1, 1 } };
        int[,] expected = new int[,] { { 0, 1, 0 }, { 0, 0, 1 }, { 1, 0, 1 }, { 0, 1, 0 } };

        ConwayLife.GetGeneration(testCase, 1).ShouldBe(expected);
    }

    [Test]
    public void LeftColumnWithOnes__ResultResized()
    {
        int[,] testCase = new int[,] { { 1, 0, 0 }, { 1, 1, 1 }, { 1, 1, 0 } };
        int[,] expected = new int[,] { { 0, 1, 0, 0 }, { 1, 0, 0, 1 }, { 0, 1, 0, 1 } };

        ConwayLife.GetGeneration(testCase, 1).ShouldBe(expected);
    }

    [Test]
    public void RightColumnWithOnes__ResultResized()
    {
        int[,] testCase = new int[,] { { 1, 0, 1 }, { 0, 1, 1 }, { 1, 0, 1 } };
        int[,] expected = new int[,] { { 0, 0, 1, 0 }, { 1, 0, 1, 1 }, { 0, 0, 1, 0 } };

        ConwayLife.GetGeneration(testCase, 1).ShouldBe(expected);
    }

    [Test]
    public void FourWayResize__ResultResized()
    {
        int[,] testCase = new int[,] { { 1, 1, 1 }, { 1, 0, 1 }, { 1, 1, 1 } };
        int[,] expected = new int[,] { { 0, 0, 1, 0, 0 }, { 0, 1, 0, 1, 0 }, { 1, 0, 0, 0, 1 }, { 0, 1, 0, 1, 0 }, { 0, 0, 1, 0, 0 } };

        ConwayLife.GetGeneration(testCase, 1).ShouldBe(expected);
    }

    [Test]
    public void TopToCrop__ResultCropped()
    {
        int[,] testCase = new int[,] { { 0, 1, 0 }, { 0, 0, 1 }, { 1, 1, 0 } };
        int[,] expected = new int[,] { { 1, 0, 1 }, { 0, 1, 0 } };

        ConwayLife.GetGeneration(testCase, 1).ShouldBe(expected);
    }

    [Test]
    public void BottomToCrop__ResultCropped()
    {
        int[,] testCase = new int[,] { { 1, 1, 0 }, { 0, 0, 1 }, { 0, 1, 0 } };
        int[,] expected = new int[,] { { 0, 1, 0 }, { 1, 0, 1 } };

        ConwayLife.GetGeneration(testCase, 1).ShouldBe(expected);
    }

    [Test]
    public void LeftToCrop__ResultCropped()
    {
        int[,] testCase = new int[,] { { 0, 0, 1 }, { 1, 0, 1 }, { 0, 1, 0 } };
        int[,] expected = new int[,] { { 1, 0 }, { 0, 1 }, { 1, 0 } };

        ConwayLife.GetGeneration(testCase, 1).ShouldBe(expected);
    }

    [Test]
    public void RightToCrop__ResultCropped()
    {
        int[,] testCase = new int[,] { { 1, 0, 0 }, { 1, 0, 1 }, { 0, 1, 0 } };
        int[,] expected = new int[,] { { 0, 1 }, { 1, 0 }, { 0, 1 } };

        ConwayLife.GetGeneration(testCase, 1).ShouldBe(expected);
    }

    [Test]
    public void EmptyInput__ReturnsEmpty()
    {
        int[,] emptyArray = new int[,] { };

        ConwayLife.GetGeneration(emptyArray, 1).ShouldBe(emptyArray);
    }

    [Test]
    public void CroppedToEmpty__ReturnsEmpty()
    {
        int[,] testCase = new int[,] { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };
        int[,] expected = new int[,] { };

        ConwayLife.GetGeneration(testCase, 1).ShouldBe(expected);
    }
}
