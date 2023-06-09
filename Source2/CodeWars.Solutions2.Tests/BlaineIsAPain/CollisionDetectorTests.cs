using CodeWars.Solutions2.BlaineIsAPain;

namespace CodeWars.Solutions2.Tests.BlaineIsAPain;

[TestFixture]
internal class CollisionDetectorTests
{
    [TestCase("Aaaaa", 5, "Bbbb", 20)]
    [TestCase("Aaaaa", 5, "Bbbb", 90)]
    [TestCase("Aaaaa", 5, "Bbbb", 10)]
    [TestCase("Aaaaa", 5, "Bbbb", 1)]
    [TestCase("aaaaA", 20, "Bbbb", 25)]
    [TestCase("aaaaA", 20, "Bbbb", 21)]
    [TestCase("aaaaA", 20, "Bbbb", 12)]
    public void Detect_Track0_DoesNotCollideInCurrentState__ReturnsFalse(string train1, int position1, string train2, int position2)
    {
        CollisionDetector.Detect(PredefinedTracks.CreateState(0, train1, position1, train2, position2)).ShouldBeFalse();
    }

    [TestCase("Aaaaa", 5, "Bbbb", 6)]
    [TestCase("Aaaaa", 21, "Bbbb", 20)]
    [TestCase("Aaaaa", 5, "Bbbb", 2)]
    [TestCase("Aaaaa", 5, "Bbbb", 9)]
    [TestCase("Aaaaa", 98, "Bbbb", 0)]
    [TestCase("Aaaaa", 98, "Bbbb", 2)]
    [TestCase("Aaaaa", 98, "Bbbb", 95)]
    [TestCase("aaaaA", 20, "Bbbb", 19)]
    [TestCase("aaaaA", 20, "Bbbb", 13)]
    public void Detect_Track0_CollideInCurrentState__ReturnsTrue(string train1, int position1, string train2, int position2)
    {
        CollisionDetector.Detect(PredefinedTracks.CreateState(0, train1, position1, train2, position2)).ShouldBeTrue();
    }

    //straight
    [TestCase("Aaaaa", 5, "Bbbb", 20)]
    [TestCase("Aaaaa", 15, "Bbbb", 20)]
    [TestCase("Aaaaa", 24, "Bbbb", 20)]
    [TestCase("Aaaaa", 97, "Bbbb", 2)]
    [TestCase("aaaaA", 20, "Bbbb", 25)]
    [TestCase("aaaaA", 20, "Bbbb", 21)]
    [TestCase("aaaaA", 20, "Bbbb", 12)]
    //through crossing
    [TestCase("Aaaaa", 20, "Bbbb", 35)]
    [TestCase("Aaaaa", 25, "Bbbb", 30)]
    [TestCase("Aaaaa", 30, "Bbbb", 26)]
    [TestCase("Aaaaa", 31, "Bbbb", 70)]
    [TestCase("Aaaaa", 30, "Bbbb", 71)]
    [TestCase("aaaaA", 29, "Bbbb", 31)]
    [TestCase("aaaaA", 30, "Bbbb", 31)]
    [TestCase("aaaaA", 29, "Bbbb", 30)]
    [TestCase("aaaaA", 34, "Bbbb", 26)]
    public void Detect_Track1_DoesNotCollideInCurrentState__ReturnsFalse(string train1, int position1, string train2, int position2)
    {
        CollisionDetector.Detect(PredefinedTracks.CreateState(1, train1, position1, train2, position2)).ShouldBeFalse();
    }

    //straight collision
    [TestCase("Aaaaa", 5, "Bbbb", 6)]
    [TestCase("Aaaaa", 21, "Bbbb", 20)]
    [TestCase("Aaaaa", 5, "Bbbb", 2)]
    [TestCase("Aaaaa", 5, "Bbbb", 9)]
    [TestCase("Aaaaa", 98, "Bbbb", 0)]
    [TestCase("Aaaaa", 98, "Bbbb", 2)]
    [TestCase("Aaaaa", 98, "Bbbb", 95)]
    [TestCase("aaaaA", 20, "Bbbb", 19)]
    [TestCase("aaaaA", 20, "Bbbb", 13)]
    //through crossing
    [TestCase("Aaaaa", 30, "Bbbb", 70)]
    [TestCase("Aaaaa", 26, "Bbbb", 70)]
    [TestCase("Aaaaa", 26, "Bbbb", 69)]
    [TestCase("aaaaA", 30, "Bbbb", 70)]
    [TestCase("aaaaA", 31, "Bbbb", 70)]
    [TestCase("aaaaA", 30, "Bbbb", 69)]
    public void Detect_Track1_CollideInCurrentState__ReturnsTrue(string train1, int position1, string train2, int position2)
    {
        CollisionDetector.Detect(PredefinedTracks.CreateState(1, train1, position1, train2, position2)).ShouldBeTrue();
    }
}
