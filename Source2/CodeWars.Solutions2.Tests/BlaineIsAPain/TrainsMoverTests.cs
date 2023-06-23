using CodeWars.Solutions2.BlaineIsAPain;

namespace CodeWars.Solutions2.Tests.BlaineIsAPain;

[TestFixture]
internal class TrainsMoverTests
{
    [TestCase("aaaaA", 20, "bbB", 40, 21, 41)]
    [TestCase("Aaaaa", 20, "Bbb", 40, 19, 39)]
    [TestCase("aaaaA", 20, "Bbb", 40, 21, 39)]
    public void Move_SimpleMove__MovesCorrectly(string trainLonger, int positionLonger, string trainShorter, int positionShorter, 
        int expectedPositionLonger, int expectedPositionShorter)
    {
        trainLonger.Length.ShouldBeGreaterThanOrEqualTo(trainShorter.Length);
        var state = PredefinedTracks.CreateState(0, trainLonger, positionLonger, trainShorter, positionShorter);

        TrainsMover.Move(state);

        state.TrainLonger.CurrentPosition.ShouldBe(expectedPositionLonger);
        state.TrainShorter.CurrentPosition.ShouldBe(expectedPositionShorter);
    }

    [TestCase("aaaaA", 29, "Bbb", 11, 30, 10, 4, 2)]
    [TestCase("xxxxX", 29, "Xxx", 11, 30, 10, 0, 0)]
    public void Move_EnterStation__SetsStationStopCorrectly(string trainLonger, int positionLonger, string trainShorter, int positionShorter,
        int expectedPositionLonger, int expectedPositionShorter,
        int expectedWaitLonger, int expectedWaitShorter)
    {
        trainLonger.Length.ShouldBeGreaterThanOrEqualTo(trainShorter.Length);
        var state = PredefinedTracks.CreateState(0, trainLonger, positionLonger, trainShorter, positionShorter);

        TrainsMover.Move(state);

        state.TrainLonger.CurrentPosition.ShouldBe(expectedPositionLonger);
        state.TrainLonger.WaitTimeOnStation.ShouldBe(expectedWaitLonger);
        state.TrainShorter.CurrentPosition.ShouldBe(expectedPositionShorter);
        state.TrainShorter.WaitTimeOnStation.ShouldBe(expectedWaitShorter);
    }

    [TestCase("aaaaA", 30, "Bbb", 10, 4, 2, 30, 10, 3, 1)]
    [TestCase("aaaaA", 30, "Bbb", 10, 1, 1, 30, 10, 0, 0)]
    [TestCase("aaaaA", 30, "Bbb", 10, 0, 0, 31, 9, 0, 0)]
    public void Move_WaitOnStation__WaitsCorrectly(string trainLonger, int positionLonger, string trainShorter, int positionShorter,
        int waitLonger, int waitShorter,
        int expectedPositionLonger, int expectedPositionShorter,
        int expectedWaitLonger, int expectedWaitShorter)
    {
        trainLonger.Length.ShouldBeGreaterThanOrEqualTo(trainShorter.Length);
        var state = PredefinedTracks.CreateState(0, trainLonger, positionLonger, trainShorter, positionShorter);
        state.TrainLonger.WaitTimeOnStation = waitLonger;
        state.TrainShorter.WaitTimeOnStation = waitShorter;

        TrainsMover.Move(state);

        state.TrainLonger.CurrentPosition.ShouldBe(expectedPositionLonger);
        state.TrainLonger.WaitTimeOnStation.ShouldBe(expectedWaitLonger);
        state.TrainShorter.CurrentPosition.ShouldBe(expectedPositionShorter);
        state.TrainShorter.WaitTimeOnStation.ShouldBe(expectedWaitShorter);
    }
}
