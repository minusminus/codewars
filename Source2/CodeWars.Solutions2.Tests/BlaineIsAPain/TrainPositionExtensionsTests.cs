using CodeWars.Solutions2.BlaineIsAPain;

namespace CodeWars.Solutions2.Tests.BlaineIsAPain;

internal class TrainPositionExtensionsTests
{
    //straight on track
    [TestCase(10, 5, 8, true)]
    [TestCase(10, 5, 10, true)]
    [TestCase(10, 5, 5, true)]
    [TestCase(10, 5, 4, false)]
    [TestCase(10, 5, 11, false)]
    //goes through start position
    [TestCase(5, 15, 4, true)]
    [TestCase(5, 15, 18, true)]
    [TestCase(5, 15, 5, true)]
    [TestCase(5, 15, 15, true)]
    [TestCase(5, 15, 6, false)]
    [TestCase(5, 15, 14, false)]
    public void PositionOnTrain__ReturnsCorrectly(int start, int end, int position, bool expected)
    {
        TrainPositionExtensions.PositionOnTrain(new TrainPosition(start, end), position).ShouldBe(expected);
    }
}
