using CodeWars.Solutions2.BlaineIsAPain.Data;

namespace CodeWars.Solutions2.Tests.BlaineIsAPain.Data;

[TestFixture]
internal class TrainExtensionsTests
{
    [TestCase("aaaA", 1)]
    [TestCase("Aaaa", -1)]
    public void DirectionCoeff__ReturnsCorrectly(string trainDefinition, int expectedCoeff)
    {
        new Train(trainDefinition, 10).DirectionCoeff().ShouldBe(expectedCoeff);
    }

    [TestCase("aaaA", 10, 5, 20, 15)]
    [TestCase("aaaA", 10, 15, 20, 5)]
    [TestCase("Aaaa", 10, 5, 20, 5)]
    [TestCase("Aaaa", 10, 15, 20, 15)]
    [TestCase("Aaaa", 1, 1, 20, 0)]
    [TestCase("Aaaa", 1, 2, 20, 19)]
    [TestCase("aaaA", 19, 1, 20, 0)]
    [TestCase("aaaA", 19, 2, 20, 1)]
    public void MoveForward__ReturnsCorrectly(string trainDefinition, int startingPosition, int units, int trackLength, int expectedPosition)
    {
        new Train(trainDefinition, startingPosition).MoveForward(units, trackLength).ShouldBe(expectedPosition);
    }

    [TestCase("aaaA", 10, 5, 20, 5)]
    [TestCase("aaaA", 10, 15, 20, 15)]
    [TestCase("Aaaa", 10, 5, 20, 15)]
    [TestCase("Aaaa", 10, 15, 20, 5)]
    public void MoveBackward__ReturnsCorrectly(string trainDefinition, int startingPosition, int units, int trackLength, int expectedPosition)
    {
        new Train(trainDefinition, startingPosition).MoveBackward(units, trackLength).ShouldBe(expectedPosition);
    }

    [TestCase("aaaaA", 10, 20, 10, 6)]
    [TestCase("aaaaA", 2, 20, 2, 18)]
    [TestCase("Aaaaa", 10, 20, 14, 10)]
    [TestCase("Aaaaa", 18, 20, 2, 18)]
    public void PositionOnTrack__ReturnsCorrectly(string trainDefinition, int position, int trackLength, int expectedStart, int expectedEnd)
    {
        var result = new Train(trainDefinition, position).PositionOnTrack(trackLength);

        result.Start.ShouldBe(expectedStart);
        result.End.ShouldBe(expectedEnd);
    }
}
