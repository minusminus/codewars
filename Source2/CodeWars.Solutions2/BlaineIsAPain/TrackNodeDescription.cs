namespace CodeWars.Solutions2.BlaineIsAPain;

/// <summary>
/// Track node description.
/// </summary>
internal class TrackNodeDescription
{
    public bool IsStation;
    public bool IsCrossing;
    public readonly List<int> TrackPositions = new();
}
