namespace CodeWars.Solutions2.BlaineIsAPain.Data;

/// <summary>
/// Track crossing info.
/// Contains list of indexes of TrackNode in TrackNodes.
/// </summary>
public class TrackCrossingInfo
{
    public readonly IReadOnlyList<int> TrackPositions;

    public TrackCrossingInfo(List<int>? trackPositions = null)
    {
        TrackPositions = (trackPositions ?? new List<int>()).AsReadOnly();
    }
}
