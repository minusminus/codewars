using CodeWars.Solutions2.Tools;

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

        if (PositionsCollideOnStraightTrack(positionLonger, positionShorter, state.TrackLength)) return true;

        //collision on crossing

        return false;
    }

    private static bool PositionsCollideOnStraightTrack(in TrainPosition longer, in TrainPosition shorter, int trackLength)
    {
        for (int i = longer.End; i != (longer.Start + 1).Mod(trackLength); i = (i + 1).Mod(trackLength))
            if ((i == shorter.Start) || (i == shorter.End)) return true;
        return false;
    }
}
