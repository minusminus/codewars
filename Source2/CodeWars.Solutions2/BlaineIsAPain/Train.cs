namespace CodeWars.Solutions2.BlaineIsAPain;

internal enum TrainDirection { Clockwise, Counterclockwise };

/// <summary>
/// Train data.
/// </summary>
internal class Train
{
    public readonly string Definition;
    public readonly int StartingPosition;
    public readonly TrainDirection Direction;
    public readonly int Length;
    public readonly int CarsCount;
    public int CurrentPosition;
    public TrackNode? LastNode;

    public Train(string definition, int startingPosition)
    {
        Definition = definition;
        StartingPosition = startingPosition;
        Direction = char.IsUpper(definition[0]) ? TrainDirection.Counterclockwise : TrainDirection.Clockwise;
        Length = definition.Length;
        CarsCount = definition.Length - 1;
        CurrentPosition = startingPosition;
        LastNode = null;
    }
}
