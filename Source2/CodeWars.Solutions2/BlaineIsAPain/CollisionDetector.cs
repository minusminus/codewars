﻿using CodeWars.Solutions2.Tools;

namespace CodeWars.Solutions2.BlaineIsAPain;

/// <summary>
/// Detects collisions on track.
/// </summary>
public static class CollisionDetector
{
    public static bool Detect(State state, out int collisionAfter)
    {
        collisionAfter = 0;
        if (CollisionInCurrentState(state)) return true;

        return false;
    }

    private static bool CollisionInCurrentState(State state)
    {
        var positionShorter = state.TrainShorter.PositionOnTrack(state.TrackLength);
        var positionLonger = state.TrainLonger.PositionOnTrack(state.TrackLength);

        return PositionsCollideOnStraightTrack(positionLonger, positionShorter)
            || PositionsCollideOnCrossings(state, positionLonger, positionShorter);
    }

    private static bool PositionsCollideOnStraightTrack(in TrainPosition longer, in TrainPosition shorter) =>
        //trains collide on straight track if:
        //shorter Start or End is on longer
        longer.PositionOnTrain(shorter.Start) || longer.PositionOnTrain(shorter.End);

    private static bool PositionsCollideOnCrossings(State state, in TrainPosition longer, in TrainPosition shorter)
    {
        //nodes are scanned backward from shorter train's last:
        //- if shorter train is on a specified node
        //- and the node is crossing
        //for such node all crossing track positions are checked on longer train
        //if longer train is on any then trains collide

        int lastNodeIndex = (state.TrainShorter.LastNodeIndex == -1) ? state.TrackNodes.Count - 1 : state.TrainShorter.LastNodeIndex;
        while ((lastNodeIndex >= 0) && shorter.PositionOnTrain(state.TrackNodes[lastNodeIndex].TrackPosition))
        {
            if (state.TrackNodes[lastNodeIndex].IsCrossing)
                foreach (var position in state.TrackCrossings[state.TrackNodes[lastNodeIndex].TrackNodeKey].TrackPositions)
                    if (longer.PositionOnTrain(position)) return true;
            lastNodeIndex--;
        }
        return false;
    }
}