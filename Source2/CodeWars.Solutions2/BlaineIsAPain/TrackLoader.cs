using CodeWars.Solutions2.BlaineIsAPain.Data;
using System.Data.Common;

namespace CodeWars.Solutions2.BlaineIsAPain;

/// <summary>
/// Loads track from string into TrackNodes and TrackCrossings, calculates track length.
/// </summary>
public static class TrackLoader
{
    private class TrackIndex
    {
        public int Row, Column;

        public TrackIndex(int row, int column) => 
            (Row, Column) = (row, column);
    }

    private const char EmptyTrackChar = ' ';
    private const char Station = 'S';
    private static readonly HashSet<char> NodeSymbols = new() { Station, '+', 'X' };
    private static readonly List<(int row, int column)> ClockwiseIndexes = new() { (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1) };
    private static readonly Dictionary<char, (int row, int column)> NextTrackPositions = new()
    {
        {'-', () }
    };

    public static void Load(string trackDescription)
    {
        string[] track = SplitIntoLines(trackDescription);
        TrackIndex startingPoint = new(0, FindZeroTrackPoint(track));
        ReadTrack(track, startingPoint);
    }

    private static string[] SplitIntoLines(in string trackDescription) => 
        trackDescription.Split('\n', StringSplitOptions.RemoveEmptyEntries);

    private static int FindZeroTrackPoint(in string[] track) => 
        track[0].TakeWhile((c) => c == EmptyTrackChar).Count();

    private static void ReadTrack(in string[] track, TrackIndex startingPoint)
    {
        List<TrackNode> trackNodes = new();
        TrackIndex index = new(startingPoint.Row, startingPoint.Column);
        int trackLength = 0;
        char previousTrackPoint = EmptyTrackChar;

        do
        {
            char currentTrackPoint = track.TrackPointAtIndex(index);
            
            if (CheckForNextNode(currentTrackPoint, track, trackLength, index, out TrackNode? nextNode))
                trackNodes.Add(nextNode!);

            if (previousTrackPoint == EmptyTrackChar)
                GoToNextFromStartingPoint(track, index);

            if (!currentTrackPoint.IsTrackNode())
                previousTrackPoint = currentTrackPoint;

            trackLength++;
        } while ((index.Row != startingPoint.Row) || (index.Column != startingPoint.Column));
    }

    private static bool CheckForNextNode(in char currentTrackPoint, in string[] track, in int currentTrackPosition, TrackIndex index, out TrackNode? nextNode)
    {
        nextNode = null;
        if (!NodeSymbols.Contains(currentTrackPoint)) return false;
        nextNode = new(new TrackNodeKey(index.Row, index.Column), currentTrackPosition, currentTrackPoint == Station, CountTracksAround(track, index) > 2);
        return true;
    }

    private static int CountTracksAround(in string[] track, TrackIndex index)
    {
        int tracksAround = 0;
        foreach (var (row, column) in ClockwiseIndexes)
            tracksAround += track.TrackPointAtIndex(index.Row + row, index.Column + column).IsTrack() ? 1 : 0;
        return tracksAround;
    }

    private static void GoToNextFromStartingPoint(in string[] track, TrackIndex index)
    {
        foreach (var (row, column) in ClockwiseIndexes.Take(5))
            if (track.TrackPointAtIndex(index.Row + row, index.Column + column).IsTrack())
            {
                index.Row += row;
                index.Column += column;
                return;
            }
    }

    private static bool IsTrack(this char trackPoint) =>
        trackPoint != EmptyTrackChar;

    private static bool IsTrackNode(this char trackPoint) =>
        NodeSymbols.Contains(trackPoint);

    private static char TrackPointAtIndex(this string[] track, TrackIndex index) =>
        track.TrackPointAtIndex(index.Row, index.Column);

    private static char TrackPointAtIndex(this string[] track, int row, int column) =>
        (row >= 0) && (row < track.Length) && (column >= 0) && (column < track[row].Length)
            ? track[row][column]
            : EmptyTrackChar;
}
