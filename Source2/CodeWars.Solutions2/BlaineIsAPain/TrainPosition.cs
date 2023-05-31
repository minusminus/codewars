namespace CodeWars.Solutions2.BlaineIsAPain;

/// <summary>
/// Describes train position on track.
/// Start should be clockwise greater than End.
/// </summary>
public readonly struct TrainPosition
{
    public readonly int Start;
    public readonly int End;

    public TrainPosition(int start, int end) : this()
    {
        Start = start;
        End = end;
    }
}
