using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberTheory
{
    /// <summary>
    /// Klasa obsługi liczb pierwszych
    /// </summary>
    public class PrimeNumbers
    {
        /// <summary>
        /// Funkcja oznaczająca liczby pierwsze w przedziale [0,upperBound].
        /// Zwraca maske bitową liczb pierwszych.
        /// </summary>
        /// <param name="upperBound"></param>
        /// <returns></returns>
        public BitArray EratosthenesSieve(int upperBound)
        {
            BitArray res = new BitArray(upperBound + 1);
            int ubsq = (int)Math.Sqrt(upperBound);

            for(int m=2; m<=ubsq; m++)
                if (!res[m])
                {
                    for (int i = m*m; i <= upperBound; i += m)
                        res[i] = true;
                }
            return res;
        }

        /// <summary>
        /// Konwertuje bitową maskę liczb pierwszych (np z sita) na listę intów
        /// </summary>
        /// <param name="bits"></param>
        /// <returns></returns>
        public List<int> GetPrimesListFromBits(BitArray bits)
        {
            List<int> res = new List<int>();
            for (int i = 2; i < bits.Count; i++)
                if (!bits[i]) res.Add(i);
            return res;
        }

        public List<int> PrimesListToN(int n)
        {
            return GetPrimesListFromBits( EratosthenesSieve(n) );
        }
    }
}
