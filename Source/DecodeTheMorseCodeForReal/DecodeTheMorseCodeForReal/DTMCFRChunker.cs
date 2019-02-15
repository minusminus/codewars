using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecodeTheMorseCodeForReal
{
    /// <summary>
    /// Mechanizm dzielacy ciag 0 i 1 na fragmenty. Liczy ilosc i normalizuje cala tablice.
    /// </summary>
    public class DTMCFRChunker
    {
        public List<DTMCFRDataChunk> ChunkBits(string bits)
        {
            List<DTMCFRDataChunk> res = new List<DTMCFRDataChunk>();

            foreach (char c in bits)
            {
                if ((res.Count == 0) || (res.Last().Symbol != c))
                    res.Add(new DTMCFRDataChunk() { Symbol = c, Length = 0 });
                res.Last().Length++;
            }
            if (res.First().Symbol == '0') res.RemoveAt(0);
            if (res.Any() && (res.Last().Symbol == '0')) res.RemoveAt(res.Count - 1);

            return res;
        }

        public void NormalizeData(DTMCFRDataToAnalysis[] data, List<DTMCFRDataChunk> chunks)
        {
            int lmax = chunks.Max(x => x.Length);
            int lmin = chunks.Min(x => x.Length);
            //if (lmax < lmin*7) lmax = lmin*7;
            double normCoef = 1.0;
            if ((lmax - lmin) > 0) normCoef = 1.0/(lmax - lmin);

            for (int i = 0; i < data.Length; i++)
                data[i].NormalizedLength = (data[i].Length - lmin) * normCoef;
        }

        public DTMCFRDataToAnalysis[] GetArrayToAnalysis(List<DTMCFRDataChunk> chunks, char symbol)
        {
            DTMCFRDataToAnalysis[] res = chunks.Where(x => x.Symbol == symbol)
                .GroupBy(x => x.Length)
                .OrderBy(x => x.Key)
                .Select(x => new DTMCFRDataToAnalysis() { Length = x.Key, Cluster = -1})
                .ToArray();
            NormalizeData(res, chunks);
            return res;
        }
    }
}
