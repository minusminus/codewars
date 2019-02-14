using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecodeTheMorseCodeForReal
{
    public class DTMCFR
    {
        public int FindChunkClass(int chunkLength, DTMCFRDataToAnalysis[] arr)
        {
            return arr.First(x => x.Length == chunkLength).Cluster;
        }

        public string DecodeChunks2(List<DTMCFRDataChunk> chunks, DTMCFRDataToAnalysis[] arr0, DTMCFRDataToAnalysis[] arr1)
        {
            string[] clusterStrings0 = new string[3] {"", new string(' ', 1), new string(' ', 3)};
            string[] clusterStrings1 = new string[2] {".", "-"};

            StringBuilder res = new StringBuilder();

            chunks.ForEach(x =>
            {
                switch (x.Symbol)
                {
                    case '0':
                        res.Append(clusterStrings0[FindChunkClass(x.Length, arr0)]);
                        break;
                    case '1':
                        res.Append(clusterStrings1[FindChunkClass(x.Length, arr1)]);
                        break;
                }
            });

            return res.ToString();
        }

        public string decodeBitsAdvanced(string bits)
        {
            DTMCFRChunker chunker = new DTMCFRChunker();
            List<DTMCFRDataChunk> chunks = chunker.ChunkBits(bits);
            DTMCFRDataToAnalysis[] arr0 = chunker.GetArrayToAnalysis(chunks, '0');
            DTMCFRDataToAnalysis[] arr1 = chunker.GetArrayToAnalysis(chunks, '1');

            DTMCFRClustering2 clustering = new DTMCFRClustering2();
            double[] means0 = new double[3] {1.0/14.0, 6.0/14.0, 13.0/14.0};
            clustering.Cluster(arr0, means0); //pause between: dots/dashes 1 unit, characters 3 units, words 7 units [7 units long: 1/7 * 1/2, 6/14, 13/14]
            double[] means1 = new double[2] {1.0/6.0, 5.0/6.0};
            clustering.Cluster(arr1, means1);    //dot 1 unit, dash 3 units [3 units long, dot in half of first unit - 1/3 * 1/2, dash in half of last - 5/6]
            DTMCFRReclustering reclustering = new DTMCFRReclustering();
            reclustering.Recluster(arr0, means0, arr1, means1);

            return DecodeChunks2(chunks, arr0, arr1);
        }


        public string decodeMorse(string morseCode)
        {
            // Map morse code using map Preloaded.MORSE_CODE
            string[] tbl = morseCode.Replace("   ", " | ")
                .Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder res = new StringBuilder();
            foreach (string s in tbl)
                res.Append(s == "|" ? ' ' : Preloaded.MORSE_CODE[s]);
            return res.ToString();
        }
    }
}
