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
            //if (lmax / lmin < 7) lmax = lmin * 7;

            double normCoef = 1.0;
            if ((lmax - lmin) > 0) normCoef = 1.0/(lmax - lmin);

            for (int i = 0; i < data.Length; i++)
                data[i].NormalizedLength = (data[i].Length - lmin) * normCoef;
        }

        public void NormalizeData2(DTMCFRDataToAnalysis[] data, List<DTMCFRDataChunk> chunks)
        {
            //lmin zredukowane do 1 - wszystkie wartosci znormalizowane przez lmin
            //dalej normalizacja do przedzialow o dl 7
            //najdluzsza jedynka musi byc zredukowana do 4.5 zeby pasowala do drugiego przedzialu
            const double maxOneScale = 4.5;

            double lmax = chunks.Max(x => x.Length);
            double lmin = chunks.Min(x => x.Length);
            int maxOneLength = chunks.Where(x => x.Symbol == '1').Max(x => x.Length);

            double lengthNorm = 1.0 / lmin;
            if (maxOneLength * lengthNorm > maxOneScale) lengthNorm = maxOneScale / maxOneLength;

            for (int i = 0; i < data.Length; i++)
                data[i].NormalizedLength = data[i].Length * lengthNorm;
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

        public DTMCFRDataToAnalysis[] GetArrayToAnalysis(List<DTMCFRDataChunk> chunks)
        {
            DTMCFRDataToAnalysis[] res = chunks
                .GroupBy(x => x.Length)
                .OrderBy(x => x.Key)
                .Select(x => new DTMCFRDataToAnalysis() { Length = x.Key, Cluster = -1 })
                .ToArray();
            NormalizeData2(res, chunks);
            return res;
        }
    }
}
