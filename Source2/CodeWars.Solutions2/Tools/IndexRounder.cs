namespace CodeWars.Solutions2.Tools;

/// <summary>
/// Cyclic index inc/dec.
/// </summary>
public static class IndexRounder
{
    public static int IncIndex(this int index, int length) => 
        (index < length - 1) ? index + 1 : 0;

    public static int DecIndex(this int index, int length) =>
        (index > 0) ? index - 1 : length - 1;
}
