namespace CodeWars.Solutions.BecomeImmortal
{
    public class BecomeImmortalBF
    {
        public static long ElderAge(long m, long n, long l, long t)
        {
            long sum = 0;
            for (long x = 0; x < m; x++)
                for (long y = 0; y < n; y++)
                    sum += SubtractL(x ^ y, l);
            return sum % t;
        }

        private static long SubtractL(long value, long l) =>
        //value < (2 * l) ? 0 : value - l;
            value - l < 0 ? 0 : value - l;
    }
}
