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
            if(CheckForNextNode(track, trackLength, index, out TrackNode? nextNode))
                trackNodes.Add(nextNode!);

            char currentTrackPoint = track.TrackAtIndex(index);
            GoToNextTrackPoint(track, index, previousTrackPoint);
            previousTrackPoint = currentTrackPoint;

            trackLength++;
        } while ((index.Row != startingPoint.Row) || (index.Column != startingPoint.Column));
    }

    private static bool CheckForNextNode(in string[] track, in int currentTrackPosition, TrackIndex index, out TrackNode? nextNode)
    {
        nextNode = null;
        char trackElement = track.TrackAtIndex(index);
        if (!NodeSymbols.Contains(trackElement)) return false;
        nextNode = new(new TrackNodeKey(index.Row, index.Column), currentTrackPosition, trackElement == Station, IsCrossing(track, index));
        return true;
    }

    private static bool IsCrossing(in string[] track, TrackIndex index)
    {
        int tracksAround = 0;
        for (int row = -1; row <= 1; row++)
            for (int column = -1; column <= 1; column++)
                if ((row != 0) || (column != 0))
                    tracksAround += CharIsTrack(track, index.Row + row, index.Column + column) ? 1 : 0;
        return (tracksAround > 2);

        static bool CharIsTrack(in string[] track, int row, int column) =>
            track.TrackAtIndex(row, column) != EmptyTrackChar;
    }

    private static void GoToNextTrackPoint(in string[] track, TrackIndex index, in char previousTrackPoint)
    {

    }

    private static char TrackAtIndex(this string[] track, TrackIndex index) =>
        track.TrackAtIndex(index.Row, index.Column);

    private static char TrackAtIndex(this string[] track, int row, int column) =>
        (row >= 0) && (row < track.Length) && (column >= 0) && (column < track[row].Length)
            ? track[row][column]
            : EmptyTrackChar;
}
