using CodeWars.Solutions2.BlaineIsAPain.Data;

namespace CodeWars.Solutions2.Tests.BlaineIsAPain;

internal static class PredefinedTracks
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
    private static readonly List<TrackNode>[] TrackNodes =
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
    private static readonly Dictionary<TrackNodeKey, TrackCrossingInfo>[] TrackCrossings =
    {
        new Dictionary<TrackNodeKey, TrackCrossingInfo>(),
        new Dictionary<TrackNodeKey, TrackCrossingInfo>()
        {
            { new TrackNodeKey(2, 30), new TrackCrossingInfo(new List<int>(){30, 70}) }
        },
    };
    private static readonly int[] TrackLengths =
    {
        100,
        100,
    };

    public static State CreateState(int trackDefinition, string train1, int position1, string train2, int position2) =>
        new(new Train(train1, position1), new Train(train2, position2),
            TrackLengths[trackDefinition], TrackNodes[trackDefinition], TrackCrossings[trackDefinition]);
}
