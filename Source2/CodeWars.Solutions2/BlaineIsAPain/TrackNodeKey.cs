namespace CodeWars.Solutions2.BlaineIsAPain;

/// <summary>
/// Key of track node.
/// </summary>
internal readonly struct TrackNodeKey
{
    public readonly int Row;
    public readonly int Column;

    public TrackNodeKey(int row, int column) : this()
    {
        Row = row;
        Column = column;
    }
}
