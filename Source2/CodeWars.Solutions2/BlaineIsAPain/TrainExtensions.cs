using CodeWars.Solutions2.Tools;

namespace CodeWars.Solutions2.BlaineIsAPain;

/// <summary>
/// Extensions to Train class.
/// </summary>
public static class TrainExtensions
{
    public static int DirectionCoeff(this Train train) => 
        (train.Direction == TrainDirection.Clockwise) ? 1 : -1;

    public static int MoveForward(this Train train, int units, int trackLength) => 
        (train.CurrentPosition + train.DirectionCoeff() * units).Mod(trackLength);

    public static int MoveBackward(this Train train, int units, int trackLength) =>
        (train.CurrentPosition - train.DirectionCoeff() * units).Mod(trackLength);

    public static TrainPosition PositionOnTrack(this Train train, int trackLength)
    {
        int start = train.CurrentPosition;
        int end = train.MoveBackward(train.CarsCount, trackLength);

        return (train.Direction == TrainDirection.Clockwise)
            ? new TrainPosition(start, end) 
            : new TrainPosition(end, start);
    }
}
