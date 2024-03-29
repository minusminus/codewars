﻿namespace CodeWars.Solutions2.BlaineIsAPain.Data;

/// <summary>
/// Describes train position on track.
/// Start is expected to be clockwise greater than End.
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
