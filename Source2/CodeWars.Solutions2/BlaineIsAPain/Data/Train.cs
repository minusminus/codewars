namespace CodeWars.Solutions2.BlaineIsAPain.Data;

public enum TrainDirection { Clockwise, Counterclockwise };

/// <summary>
/// Train data.
/// </summary>
public class Train
{
    public readonly string Definition;
    public readonly int StartingPosition;
    public readonly bool IsExpress;
    public readonly TrainDirection Direction;
    public readonly int Length;
    public readonly int CarsCount;
    public int CurrentPosition;
    public int LastNodeIndex;

    public Train(string definition, int startingPosition)
    {
        Definition = definition;
        StartingPosition = startingPosition;
        IsExpress = definition[0] is 'x' or 'X';
        Direction = char.IsUpper(definition[0]) ? TrainDirection.Counterclockwise : TrainDirection.Clockwise;
        Length = definition.Length;
        CarsCount = definition.Length - 1;
        CurrentPosition = startingPosition;
        LastNodeIndex = -1;
    }
}
