using CodeWars.Solutions2.BlaineIsAPain.Data;

namespace CodeWars.Solutions2.BlaineIsAPain;

/// <summary>
/// Loads track from string into TrackNodes and TrackCrossings, calculates track length.
/// 
/// Zero point (top left track point) can be only '/'
/// </summary>
public static class TrackLoader
{
    private struct TrackIndex
    {
        public int Row, Column;

        public TrackIndex(int row, int column) => 
            (Row, Column) = (row, column);
    }

    private const char EmptyTrackChar = ' ';
    private const char Station = 'S';
    private static readonly HashSet<char> NodeSymbols = new() { Station, '+', 'X' };
    private static readonly List<(int row, int column)> ClockwiseIndexes = new() { (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1) };

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
        TrackIndex currentIndex = new(startingPoint.Row, startingPoint.Column);
        TrackIndex previousIndex = new(currentIndex.Row, currentIndex.Column - 1);
        int trackPosition = 0;

        do
        {
            char currentTrackPoint = track.TrackPointAtIndex(currentIndex);
            
            if (IsNodeOnCurrentTrackPosition(currentTrackPoint, track, trackPosition, currentIndex, out TrackNode? nextNode))
                trackNodes.Add(nextNode!);

            (currentIndex, previousIndex) = GoToNextTrackPoint(track, currentIndex, previousIndex);

            trackPosition++;
        } while ((currentIndex.Row != startingPoint.Row) || (currentIndex.Column != startingPoint.Column));
    }

    private static bool IsNodeOnCurrentTrackPosition(in char currentTrackPoint, in string[] track, in int currentTrackPosition, TrackIndex index, out TrackNode? nextNode)
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

    private static (TrackIndex newCurrent, TrackIndex newPrevious) GoToNextTrackPoint(in string[] track, TrackIndex currentPoint, TrackIndex previousPoint)
    {
        int x = currentPoint.Column - previousPoint.Column;
        int y = currentPoint.Row - previousPoint.Row;

        if (track.TrackPointAtIndex(currentPoint.Row + y, currentPoint.Column + x).IsTrack())
            return (new TrackIndex(currentPoint.Row + y, currentPoint.Column + x), currentPoint);
        //nie ma tracka na wektorze - mamy zakręt i szukamy tracka 2 pola w kierunku określonym przez wektor i zakręt:
        //wektor w górę (y<0) i zakręt '/' - Cw, zakręt '\' - cCw
        //wektor w dół (y>0) i zakręt '/' - Cw, zakręt '\' - cCw
        //wektor w prawo (x>0) i zakręt '/' - cCw, zakręt '\' - cW
        //wektor w lewo (x<0) i zakręt '/' - cCw, zakręt '\' - cW

        //trzeba jeszcze rozpatrzeć sytuację ze skrętem na skrzyżowanie (idąc z lewego dołu w górę), gdzie wektor pokaże track
        //ale trzeba wejść na skrzyżowanie po prawej:
        //   | /   ||  \ |     ||
        //  /+/   /+/   \+\    \+\
        // / |    ||     | \    ||
        //jeżeli dla zakrętu wektor wskazuje na track, to:
        //- jeżeli nie ma obok skrzyżowania, to idziemy na wektor
        //- jeżeli jest skrzyżowanie to na skrzyżowanie
        //skrzyżowania szukamy 1 pole w kierunku określanym przez wektor i zakręt j.w.
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
