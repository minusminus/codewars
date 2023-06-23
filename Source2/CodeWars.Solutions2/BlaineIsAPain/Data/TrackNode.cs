namespace CodeWars.Solutions2.BlaineIsAPain.Data;

/// <summary>
/// Track node.
/// </summary>
public class TrackNode
{
    public readonly TrackNodeKey TrackNodeKey;
    public readonly int TrackPosition;
    public readonly bool IsStation;
    public readonly bool IsCrossing;
    public int DistanceToNextNode;

    public TrackNode(TrackNodeKey trackNodeKey, int trackPosition, bool isStation, bool isCrossing)
    {
        TrackNodeKey = trackNodeKey;
        TrackPosition = trackPosition;
        IsStation = isStation;
        IsCrossing = isCrossing;
        DistanceToNextNode = -1;
    }
}
