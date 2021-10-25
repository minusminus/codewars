using System;

namespace R13
{

    /// <summary>
    /// Kata: Casear Cypher
    /// https://www.codewars.com/kata/casear-cypher
    /// </summary>
    public class RotWithShift
    {
        private static char[] letters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        public string Decypher(string secretCode, int shifts)
        {
            char[] res = secretCode.ToCharArray();
            for(int i=0; i<res.Length; i++)
            {
                int j = Array.IndexOf(letters, res[i]);
                if (j > -1)
                    res[i] = letters[(j + shifts) % letters.Length];
            }
            return new string(res);
        }
    }
}
