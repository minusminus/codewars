using CodeWars.Solutions2.Tools;

namespace CodeWars.Solutions2.Tests.Tools;

[TestFixture]
internal class IndexRounderTests
{
    [TestCase(10, 100, 11)]
    [TestCase(0, 100, 1)]
    [TestCase(99, 100, 0)]
    public void IncIndex__ReturnCorrectly(int currentIndex, int arrayLength, int expected)
    {
        currentIndex.IncIndex(arrayLength).ShouldBe(expected);
    }

    [TestCase(10, 100, 9)]
    [TestCase(0, 100, 99)]
    [TestCase(99, 100, 98)]
    public void DecIndex__ReturnCorrectly(int currentIndex, int arrayLength, int expected)
    {
        currentIndex.DecIndex(arrayLength).ShouldBe(expected);
    }
}
