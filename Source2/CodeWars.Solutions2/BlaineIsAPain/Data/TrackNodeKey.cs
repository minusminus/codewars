namespace CodeWars.Solutions2.BlaineIsAPain.Data;

/// <summary>
/// Key of track node.
/// </summary>
public readonly struct TrackNodeKey
{
    public readonly int Row;
    public readonly int Column;

    public TrackNodeKey(int row, int column) : this()
    {
        Row = row;
        Column = column;
    }
}
