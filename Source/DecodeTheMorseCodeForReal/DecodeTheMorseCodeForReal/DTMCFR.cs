using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecodeTheMorseCodeForReal
{
    public class DTMCFR
    {
        public List<DTMCFRDataChunk> ChunkBits(string bits)
        {
            List<DTMCFRDataChunk> res = new List<DTMCFRDataChunk>();

            foreach (char c in bits)
            {
                if ((res.Count == 0) || (res.Last().Symbol != c))
                    res.Add(new DTMCFRDataChunk() {Symbol = c, Length = 0});
                res.Last().Length++;
            }
            if (res.First().Symbol == '0') res.RemoveAt(0);
            if (res.Last().Symbol == '0') res.RemoveAt(res.Count - 1);

            return res;
        }

        public DTMCFRDataToAnalysis[] GetArrayToAnalysis(List<DTMCFRDataChunk> chunks, char symbol )
        {
            return chunks.Where(x => x.Symbol == symbol)
                .GroupBy(x => x.Length)
                .OrderBy(x => x.Key)
                .Select(x => new DTMCFRDataToAnalysis() {Length = x.Key})
                .ToArray();
        }

        public string decodeBitsAdvanced(string bits)
        {
            List<DTMCFRDataChunk> chunks = ChunkBits(bits);
            DTMCFRDataToAnalysis[] arr0 = GetArrayToAnalysis(chunks, '0');
            DTMCFRDataToAnalysis[] arr1 = GetArrayToAnalysis(chunks, '1');

            DTMCFRClustering clustering = new DTMCFRClustering();
            int[] borders0 = clustering.Cluster(arr0, new double[2] {0, 0});
            int[] borders1 = clustering.Cluster(arr1, new double[3] {0, 0, 0});

            return "";
        }

        public string decodeMorse(string morseCode)
        {
            // Map morse code using map Preloaded.MORSE_CODE
            return "";
        }
    }
}
