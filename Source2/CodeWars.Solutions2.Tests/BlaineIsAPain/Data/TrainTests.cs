using CodeWars.Solutions2.BlaineIsAPain.Data;

namespace CodeWars.Solutions2.Tests.BlaineIsAPain.Data;

internal class TrainTests
{
    [TestCase("Aaaaa", 10, TrainDirection.Counterclockwise, false)]
    [TestCase("aaaaA", 20, TrainDirection.Clockwise, false)]
    [TestCase("Xxxxx", 30, TrainDirection.Counterclockwise, true)]
    [TestCase("xxxxX", 40, TrainDirection.Clockwise, true)]
    public void Create__CreatesCorrectly(string definition, int startingPosition,
        TrainDirection expectedDirection, bool expectedExpress)
    {
        var testObj = new Train(definition, startingPosition);

        testObj.Definition.ShouldBe(definition);
        testObj.StartingPosition.ShouldBe(startingPosition);
        testObj.CurrentPosition.ShouldBe(startingPosition);
        testObj.LastNodeIndex.ShouldBe(-1);
        testObj.Direction.ShouldBe(expectedDirection);
        testObj.IsExpress.ShouldBe(expectedExpress);
        testObj.Length.ShouldBe(definition.Length);
        testObj.CarsCount.ShouldBe(definition.Length - 1);
    }
}
