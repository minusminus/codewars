using CodeWars.Solutions2.BlaineIsAPain.Data;
using CodeWars.Solutions2.Tools;

namespace CodeWars.Solutions2.BlaineIsAPain;

/// <summary>
/// Detects collisions on current state.
/// </summary>
public static class CollisionDetector
{
    public static bool Detect(State state)
    {
        var positionShorter = state.TrainShorter.PositionOnTrack(state.TrackLength);
        var positionLonger = state.TrainLonger.PositionOnTrack(state.TrackLength);

        return TrainsCollideOnStraightTrack(positionLonger, positionShorter)
            || TrainsCollideOnCrossings(state, positionLonger, positionShorter);
    }

    private static bool TrainsCollideOnStraightTrack(in TrainPosition longer, in TrainPosition shorter) =>
        //trains collide on straight track if:
        //shorter Start or End is on longer
        longer.PositionOnTrain(shorter.Start) || longer.PositionOnTrain(shorter.End);

    private static bool TrainsCollideOnCrossings(State state, in TrainPosition longer, in TrainPosition shorter)
    {
        //nodes are scanned backward from shorter train's last:
        //- if shorter train is on a specified node
        //- and the node is crossing
        //for such node all crossing track positions are checked on longer train
        //if longer train is on any then trains collide

        int lastNodeIndex = (state.TrainShorter.LastNodeIndex == -1) ? FindNodeBeforePosition(state, state.TrainShorter.Direction, state.TrainShorter.StartingPosition) : state.TrainShorter.LastNodeIndex;
        while (shorter.PositionOnTrain(state.TrackNodes[lastNodeIndex].TrackPosition))
        {
            if (state.TrackNodes[lastNodeIndex].IsCrossing)
                foreach (var position in state.TrackCrossings[state.TrackNodes[lastNodeIndex].TrackNodeKey].TrackPositions)
                    if (longer.PositionOnTrain(position)) return true;
            lastNodeIndex = GetPrevNodeIndex(state.TrainShorter.Direction, lastNodeIndex, state.TrackNodes.Count);
        }
        return false;
    }

    private static int FindNodeBeforePosition(State state, TrainDirection trainDirection, int position)
    {
        if (trainDirection == TrainDirection.Clockwise)
        {
            for (int i = state.TrackNodes.Count - 1; i >= 0; i--)
                if (state.TrackNodes[i].TrackPosition <= position) return i;
            return state.TrackNodes.Count - 1;
        } 
        else
        {
            for (int i = 0; i < state.TrackNodes.Count; i++)
                if (state.TrackNodes[i].TrackPosition >= position) return i;
            return 0;
        }
    }

    private static int GetPrevNodeIndex(TrainDirection trainDirection, int nodeIndex, int nodesCount) => 
        (nodeIndex - (trainDirection == TrainDirection.Clockwise ? 1 : -1)).Mod(nodesCount - 1);
}
