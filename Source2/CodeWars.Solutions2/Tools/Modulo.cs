namespace CodeWars.Solutions2.Tools;

/// <summary>
/// Mathematically correct modulo.
/// </summary>
public static class Modulo
{
    public static int Mod(this int a, int b)
    {
        int r = a % b;
        return r < 0 ? r + b : r;
    }

    public static long Mod(this long a, long b)
    {
        long r = a % b;
        return r < 0 ? r + b : r;
    }
}
