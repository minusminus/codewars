using CodeWars.Solutions2.Tools;

namespace CodeWars.Solutions2.BlaineIsAPain.Data;

/// <summary>
/// Extensions to State class.
/// </summary>
public static class StateExtensions
{
    public static int FindNodeBeforePositionClockwise(this State state, int position)
    {
        for (int i = state.TrackNodes.Count - 1; i >= 0; i--)
            if (state.TrackNodes[i].TrackPosition <= position) return i;
        return state.TrackNodes.Count - 1;
    }

    public static int FindNodeBeforePositionCounterclockwise(this State state, int position)
    {
        for (int i = 0; i < state.TrackNodes.Count; i++)
            if (state.TrackNodes[i].TrackPosition >= position) return i;
        return 0;
    }

    public static int NextNodeIndexClockwise(this State state, int currentNodeIndex) =>
        currentNodeIndex.IncIndex(state.TrackNodes.Count);

    public static int NextNodeIndexCounterclockwise(this State state, int currentNodeIndex) =>
        currentNodeIndex.DecIndex(state.TrackNodes.Count);
}
