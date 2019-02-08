using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecodeTheMorseCodeForReal
{
    public class DTMCFR
    {
        private void ConvertBordersToLengths(int[] borders, DTMCFRDataToAnalysis[] arr)
        {
            for (int i = 0; i < borders.Length; i++)
                borders[i] = arr[borders[i]].Length;
        }

        private int FindChunkClass(int chunkLength, int[] borders)
        {
            int res = Array.FindIndex(borders, i => chunkLength < i);
            if (res == -1) res = borders.Length;
            return res;
        }

        public string DecodeChunks(List<DTMCFRDataChunk> chunks, int[] borders0, int[] borders1)
        {
            StringBuilder res = new StringBuilder();

            chunks.ForEach(x =>
            {
                switch (x.Symbol)
                {
                    case '0':
                        switch (FindChunkClass(x.Length, borders0))
                        {
                            case 0:
                                break;
                            case 1:
                                res.Append(new string(' ', 1));
                                break;
                            case 2:
                                res.Append(new string(' ', 3));
                                break;
                        }
                        break;
                    case '1':
                        switch (FindChunkClass(x.Length, borders1))
                        {
                            case 0:
                                res.Append('.');
                                break;
                            case 1:
                                res.Append('-');
                                break;
                        }
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
            //DTMCFRDataToAnalysis[] arr = chunker.GetArrayToAnalysis(chunks);

            DTMCFRClustering clustering = new DTMCFRClustering();
            int[] borders0 = clustering.Cluster(arr0, new double[3] { 1.0 / 14.0, 6.0 / 14.0, 13.0 / 14.0 }); //pause between: dots/dashes 1 unit, characters 3 units, words 7 units [7 units long: 1/7 * 1/2, 6/14, 13/14]
            int[] borders1 = clustering.Cluster(arr1, new double[2] { 1.0 / 6.0, 5.0 / 6.0 });    //dot 1 unit, dash 3 units [3 units long, dot in half of first unit - 1/3 * 1/2, dash in half of last - 5/6]
            ConvertBordersToLengths(borders0, arr0);
            ConvertBordersToLengths(borders1, arr1);
            //int[] borders = clustering.Cluster(arr, new double[3] { 1.0 / 14.0, 6.0 / 14.0, 13.0 / 14.0 }); //pause between: dots/dashes 1 unit, characters 3 units, words 7 units [7 units long: 1/7 * 1/2, 6/14, 13/14]
            //ConvertBordersToLengths(borders, arr);

            return DecodeChunks(chunks, borders0, borders1);
            //return DecodeChunks(chunks, borders);
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
