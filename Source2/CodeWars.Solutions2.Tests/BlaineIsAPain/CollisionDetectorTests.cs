using CodeWars.Solutions2.BlaineIsAPain;

namespace CodeWars.Solutions2.Tests.BlaineIsAPain;

internal class CollisionDetectorTests
{
    /* Track 0:
     * length: 100
     * S(10)-S(30)-S(80)
     * 
     * Track 1:
     * length: 100
     * S(10)    S(60)---
     * |    \   /       |
     * |     X(30, 70)  |
     * |    /   \       |
     * S(90)    S(50)---
     * 
     * 
     */
    private readonly List<TrackNode>[] _trackNodes =
    {
        new List<TrackNode>()
        {
            new TrackNode(new TrackNodeKey(1, 10), 10, true, false),
            new TrackNode(new TrackNodeKey(1, 30), 30, true, false),
            new TrackNode(new TrackNodeKey(1, 80), 80, true, false),
        },
        new List<TrackNode>()
        {
            new TrackNode(new TrackNodeKey(1, 10), 10, true, false),
            new TrackNode(new TrackNodeKey(2, 30), 30, false, true),
            new TrackNode(new TrackNodeKey(3, 50), 50, true, false),
            new TrackNode(new TrackNodeKey(1, 60), 60, true, false),
            new TrackNode(new TrackNodeKey(2, 30), 70, false, true),
            new TrackNode(new TrackNodeKey(3, 90), 90, true, false),
        },
    };
    private readonly Dictionary<TrackNodeKey, TrackCrossingInfo>[] _trackCrossings =
    {
        new Dictionary<TrackNodeKey, TrackCrossingInfo>(),
        new Dictionary<TrackNodeKey, TrackCrossingInfo>()
        {
            { new TrackNodeKey(2, 30), new TrackCrossingInfo(new List<int>(){30, 70}) }
        },
    };
    private readonly int[] _trackLengths =
    {
        100,
        100,
    };


    [TestCase("Aaaaa", 5, "Bbbb", 20)]
    [TestCase("Aaaaa", 5, "Bbbb", 90)]
    [TestCase("Aaaaa", 5, "Bbbb", 10)]
    [TestCase("Aaaaa", 5, "Bbbb", 1)]
    public void Detect_Track0_DoesNotCollideInCurrentState__ReturnsFalseAndZero(string train1, int position1, string train2, int position2)
    {
        State state = CreateState(0, train1, position1, train2, position2);

        bool result = CollisionDetector.Detect(state, out int collisionAfter);

        result.ShouldBeFalse();
        collisionAfter.ShouldBe(0);
    }

    [TestCase("Aaaaa", 5, "Bbbb", 6)]
    [TestCase("Aaaaa", 21, "Bbbb", 20)]
    [TestCase("Aaaaa", 5, "Bbbb", 2)]
    [TestCase("Aaaaa", 5, "Bbbb", 9)]
    [TestCase("Aaaaa", 98, "Bbbb", 0)]
    [TestCase("Aaaaa", 98, "Bbbb", 2)]
    [TestCase("Aaaaa", 98, "Bbbb", 95)]
    public void Detect_Track0_CollideInCurrentState__ReturnsTrueAndZero(string train1, int position1, string train2, int position2)
    {
        State state = CreateState(0, train1, position1, train2, position2);

        bool result = CollisionDetector.Detect(state, out int collisionAfter);

        result.ShouldBeTrue();
        collisionAfter.ShouldBe(0);
    }

    //straight
    [TestCase("Aaaaa", 5, "Bbbb", 20)]
    [TestCase("Aaaaa", 15, "Bbbb", 20)]
    [TestCase("Aaaaa", 24, "Bbbb", 20)]
    [TestCase("Aaaaa", 97, "Bbbb", 2)]
    //through crossing
    public void Detect_Track1_DoesNotCollideInCurrentState__ReturnsFalseAndZero(string train1, int position1, string train2, int position2)
    {
        State state = CreateState(1, train1, position1, train2, position2);

        bool result = CollisionDetector.Detect(state, out int collisionAfter);

        result.ShouldBeFalse();
        collisionAfter.ShouldBe(0);
    }

    //straight collision
    [TestCase("Aaaaa", 5, "Bbbb", 6)]
    [TestCase("Aaaaa", 21, "Bbbb", 20)]
    [TestCase("Aaaaa", 5, "Bbbb", 2)]
    [TestCase("Aaaaa", 5, "Bbbb", 9)]
    [TestCase("Aaaaa", 98, "Bbbb", 0)]
    [TestCase("Aaaaa", 98, "Bbbb", 2)]
    [TestCase("Aaaaa", 98, "Bbbb", 95)]
    //through crossing
    [TestCase("Aaaaa", 30, "Bbbb", 70)]
    [TestCase("Aaaaa", 31, "Bbbb", 70)]
    [TestCase("Aaaaa", 30, "Bbbb", 71)]
    [TestCase("Aaaaa", 31, "Bbbb", 71)]
    [TestCase("Aaaaa", 34, "Bbbb", 70)]
    [TestCase("Aaaaa", 34, "Bbbb", 71)]
    public void Detect_Track1_CollideInCurrentState__ReturnsTrueAndZero(string train1, int position1, string train2, int position2)
    {
        State state = CreateState(0, train1, position1, train2, position2);

        bool result = CollisionDetector.Detect(state, out int collisionAfter);

        result.ShouldBeTrue();
        collisionAfter.ShouldBe(0);
    }

    private State CreateState(int trackDefinition, string train1, int position1, string train2, int position2)
    {
        State state = new(new Train(train1, position1), new Train(train2, position2), _trackCrossings[trackDefinition]);
        state.TrackLength = _trackLengths[trackDefinition];
        state.TrackNodes.AddRange(_trackNodes[trackDefinition]);
        return state;
    }
}
