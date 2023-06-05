namespace CodeWars.Solutions2.BlaineIsAPain.Data;

/// <summary>
/// Simulated system state.
/// </summary>
public class State
{
    public readonly Train TrainShorter;
    public readonly Train TrainLonger;
    public readonly IReadOnlyList<TrackNode> TrackNodes;
    public readonly IReadOnlyDictionary<TrackNodeKey, TrackCrossingInfo> TrackCrossings;
    public readonly int TrackLength;
    public int TimePassed;

    public State(Train train1, Train train2, int trackLength, List<TrackNode> trackNodes, Dictionary<TrackNodeKey, TrackCrossingInfo>? trackCrossings = null)
    {
        if (train1.Length <= train2.Length)
        {
            TrainShorter = train1;
            TrainLonger = train2;
        }
        else
        {
            TrainShorter = train2;
            TrainLonger = train1;
        }
        TrackLength = trackLength;
        TrackNodes = trackNodes.AsReadOnly();
        TrackCrossings = trackCrossings ?? new Dictionary<TrackNodeKey, TrackCrossingInfo>();
        TimePassed = 0;
    }
}
