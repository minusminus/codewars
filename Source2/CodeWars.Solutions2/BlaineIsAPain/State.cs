namespace CodeWars.Solutions2.BlaineIsAPain;

/// <summary>
/// Simulated system state.
/// 
/// Train1 is always shorter than Train2.
/// </summary>
public class State
{
    public readonly Train TrainShorter;
    public readonly Train TrainLonger;
    public readonly List<TrackNode> TrackNodes = new();
    public readonly Dictionary<TrackNodeKey, TrackCrossingInfo> TrackCrossings = new();
    public int TrackLength;
    public int TimePassed;

    public State(Train train1, Train train2)
    {
        if(train1.Length <=  train2.Length)
        {
            TrainShorter = train1;
            TrainLonger = train2;
        }
        else
        {
            TrainShorter = train2;
            TrainLonger = train1;
        }
        TimePassed = 0;
    }
}
