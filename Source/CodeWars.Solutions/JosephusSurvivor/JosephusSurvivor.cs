namespace JosephusSurvivor
{
    public class JosephusSurvivor
    {
        public int JosSurvivorRecursive(int n, int k)
        {
            if (n == 1) return 1;
            return (JosSurvivorRecursive(n - 1, k) + k - 1)%n + 1;
        }

        public int JosSurvivorNonRecursive(int n, int k)
        {
            int res = 1;
            for (int i = 1; i < n; i++)
            {
                res = (res + k - 1)%(i + 1) + 1;
            }
            return res;
        }

        public int JosSurvivor(int n, int k)
        {
            //return JosSurvivorRecursive(n, k);
            return JosSurvivorNonRecursive(n, k);
        }
    }
}
