using CodeWars.Solutions2.BlaineIsAPain.Data;

namespace CodeWars.Solutions2.BlaineIsAPain;

/// <summary>
/// Moves trains one unit on track.
/// On station waits till end of timeout.
/// </summary>
public static class TrainsMover
{
    public static void Move(State state)
    {
        SetTrainLastNode(state, state.TrainLonger);
        SetTrainLastNode(state, state.TrainShorter);
        AdvanceTrain(state, state.TrainLonger);
        AdvanceTrain(state, state.TrainShorter);
        CheckTrainOnNextNode(state, state.TrainLonger);
        CheckTrainOnNextNode(state, state.TrainShorter);
        state.TimePassed++;
    }

    private static void SetTrainLastNode(State state, Train train)
    {
        if (train.LastNodeIndex > -1) return;
        train.LastNodeIndex = (train.Direction == TrainDirection.Clockwise)
            ? state.FindNodeBeforePositionClockwise(train.CurrentPosition)
            : state.FindNodeBeforePositionCounterclockwise(train.CurrentPosition);
    }

    private static void AdvanceTrain(State state, Train train)
    {
        if (train.WaitTimeOnStation > 0)
            train.WaitTimeOnStation--;
        else
            train.CurrentPosition = train.MoveForward(1, state.TrackLength);
    }

    private static void CheckTrainOnNextNode(State state, Train train)
    {
        int nextNodeIndex = (train.Direction == TrainDirection.Clockwise)
            ? state.NextNodeIndexClockwise(train.LastNodeIndex)
            : state.NextNodeIndexCounterclockwise(train.LastNodeIndex);
        if (train.CurrentPosition == state.TrackNodes[nextNodeIndex].TrackPosition)
        {
            train.LastNodeIndex = nextNodeIndex;
            if (state.TrackNodes[nextNodeIndex].IsStation && (!train.IsExpress))
                train.WaitTimeOnStation = train.CarsCount;
        }
    }
}
