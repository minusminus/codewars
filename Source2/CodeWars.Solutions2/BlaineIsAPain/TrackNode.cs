namespace CodeWars.Solutions2.BlaineIsAPain;

/// <summary>
/// Track node.
/// </summary>
internal class TrackNode
{
    public readonly TrackNodeKey TrackNodeKey;
    public int DistanceToNextNode;

    public TrackNode(TrackNodeKey trackNodeKey)
    {
        TrackNodeKey = trackNodeKey;
        DistanceToNextNode = -1;
    }
}
