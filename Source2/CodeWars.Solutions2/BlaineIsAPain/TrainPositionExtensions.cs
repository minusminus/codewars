namespace CodeWars.Solutions2.BlaineIsAPain;

/// <summary>
/// Extensions for TrainPosition.
/// </summary>
public static class TrainPositionExtensions
{
    public static bool PositionOnTrain(this in TrainPosition trainPosition, int position) => 
        (trainPosition.Start > trainPosition.End)
            //train is not on start position
            ? (trainPosition.Start >= position) && (trainPosition.End <= position)
            //train goes through start position
            : (trainPosition.Start >= position) || (trainPosition.End <= position);
}
