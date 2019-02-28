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

        public string DecodeChunks(List<DTMCFRDataChunk> chunks, DTMCFRDataToAnalysis[] arrAll)
        {
            string[] clusterStrings0 = new string[3] { "", new string(' ', 1), new string(' ', 3) };
            string[] clusterStrings1 = new string[2] { ".", "-" };

            StringBuilder res = new StringBuilder();
            chunks.ForEach(x =>
            {
                switch (x.Symbol)
                {
                    case '0':
                        res.Append(clusterStrings0[FindChunkClass(x.Length, arrAll)]);
                        break;
                    case '1':
                        res.Append(clusterStrings1[FindChunkClass(x.Length, arrAll)]);
                        break;
                }
            });
            //Console.WriteLine(res.ToString());
            return res.ToString();
        }


        public string decodeBitsAdvanced(string bits)
        {
            if (string.IsNullOrEmpty(bits)) return "";

            DTMCFRChunker chunker = new DTMCFRChunker();
            List<DTMCFRDataChunk> chunks = chunker.ChunkBits(bits);
            if (!chunks.Any()) return "";
            if (chunks.Count == 1) return ".";
            DTMCFRDataToAnalysis[] arr = chunker.GetArrayToAnalysis(chunks);

            DTMCFRClustering2 clustering = new DTMCFRClustering2();
            double[] means = new double[3] { 1.0, 3.0, 7.0 };
            clustering.Cluster(arr, means); //pause between: dots/dashes 1 unit, characters 3 units, words 7 units [7 units long: 1/7 * 1/2, 6/14, 13/14]
            //Console.WriteLine($"means: {means[0]} | {means[1]} | {means[2]}");

            //chunks.ForEach(x => Console.Write($"({x.Symbol}, {x.Length}) "));
            //Console.WriteLine("");
            //foreach (var x in arr)
            //    Console.Write($"{x.Length}={x.Cluster} ");
            //Console.WriteLine("");

            return TryDecodeAndMoveOneLeft(chunks, arr);
        }

        public string TryDecodeAndMoveOneLeft(List<DTMCFRDataChunk> chunks, DTMCFRDataToAnalysis[] arr)
        {
            string morse = DecodeChunks(chunks, arr);
            string s;
            while (!TryDecodeMorse(morse, out s))
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].Cluster == 1)
                    {
                        if (i == 0) return "";
                        arr[i - 1].Cluster = 1;
                        break;
                    }
                }
                morse = DecodeChunks(chunks, arr);
            }
            return morse;
        }

        public bool TryDecodeMorse(string morseCode, out string decoded)
        {
            decoded = "";
            string[] tbl = morseCode.Replace("   ", " | ")
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder res = new StringBuilder();
            foreach (string s in tbl)
            {
                if (s == "|")
                    res.Append(' ');
                else
                {
                    string c;
                    if (!Preloaded.MORSE_CODE.TryGetValue(s, out c)) return false;
                    res.Append(c);
                }
            }
            decoded = res.ToString();
            return true;
        }

        public string decodeMorse(string morseCode)
        {
            //foreach (var pair in Preloaded.MORSE_CODE)
            //    Console.Write($"{{\"{pair.Key}\", \"{pair.Value}\"}},");
            //Console.WriteLine("");

            // Map morse code using map Preloaded.MORSE_CODE
            if (string.IsNullOrEmpty(morseCode) || string.IsNullOrWhiteSpace(morseCode)) return "";

            string res;
            TryDecodeMorse(morseCode, out res);
            return res;
        }
    }
}
