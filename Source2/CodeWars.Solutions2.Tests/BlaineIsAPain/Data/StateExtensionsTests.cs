using CodeWars.Solutions2.BlaineIsAPain.Data;

namespace CodeWars.Solutions2.Tests.BlaineIsAPain.Data;

[TestFixture]
internal class StateExtensionsTests
{
    private readonly State TestState = new(new Train("Aaaaa", 1), new Train("Bbbbb", 10), 100,
        new List<TrackNode>()
        {
            new TrackNode(new TrackNodeKey(1, 10), 10, true, false),
            new TrackNode(new TrackNodeKey(1, 30), 30, true, false),
            new TrackNode(new TrackNodeKey(1, 80), 80, true, false),
        });

    [TestCase(0, 1)]
    [TestCase(1, 2)]
    [TestCase(2, 0)]
    public void NextNodeIndexClockwise__ReturnsCorrectly(int currentNodeIndex, int expectedNodeIndex)
    {
        TestState.NextNodeIndexClockwise(currentNodeIndex).ShouldBe(expectedNodeIndex);
    }

    [TestCase(0, 2)]
    [TestCase(1, 0)]
    [TestCase(2, 1)]
    public void NextNodeIndexCounterclockwise__ReturnsCorrectly(int currentNodeIndex, int expectedNodeIndex)
    {
        TestState.NextNodeIndexCounterclockwise(currentNodeIndex).ShouldBe(expectedNodeIndex);
    }

    [TestCase(20, 0)]
    [TestCase(40, 1)]
    [TestCase(90, 2)]
    [TestCase(5, 2)]
    public void FindNodeBeforePositionClockwise__ReturnsCorrectly(int position, int expectedNodeIndex)
    {
        TestState.FindNodeBeforePositionClockwise(position).ShouldBe(expectedNodeIndex);
    }

    [TestCase(20, 1)]
    [TestCase(40, 2)]
    [TestCase(90, 0)]
    [TestCase(5, 0)]
    public void FindNodeBeforePositionCounterclockwise__ReturnsCorrectly(int position, int expectedNodeIndex)
    {
        TestState.FindNodeBeforePositionCounterclockwise(position).ShouldBe(expectedNodeIndex);
    }
}
