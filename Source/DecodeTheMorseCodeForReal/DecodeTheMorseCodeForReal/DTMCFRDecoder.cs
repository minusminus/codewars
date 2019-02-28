using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecodeTheMorseCodeForReal
{
    public class DTMCFRDecoder
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
                if (x.Symbol == '0')
                    res.Append(clusterStrings0[FindChunkClass(x.Length, arrAll)]);
                else
                    res.Append(clusterStrings1[FindChunkClass(x.Length, arrAll)]);
            });
            return res.ToString();
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
    }
}
