using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecodeTheMorseCodeForReal
{
    /// <summary>
    /// assumptions:
    /// pause between: dots/dashes 1 unit, characters 3 units, words 7 units
    /// 
    /// algorithm:
    /// - group input bits into chunks of 0 and 1, and remove first and last 0 if exists
    /// - normalize lengths of chunks
    ///     - that minimal length is one unit
    ///     - if still maximum 1 length is more then 4.5, normalize all that maximum 1 length is 4.5
    /// - cluster all chunks into 3 clusters with initial means of 1, 3, and 7
    /// - move border between clusters 0 and 1 left (toward 0) until message is decodable
    /// </summary>
    public class DTMCFR
    {
        public string decodeBitsAdvanced(string bits)
        {
            if (string.IsNullOrEmpty(bits)) return "";

            DTMCFRChunker chunker = new DTMCFRChunker();
            List<DTMCFRDataChunk> chunks = chunker.ChunkBits(bits);
            if (!chunks.Any()) return "";
            if (chunks.Count == 1) return ".";
            DTMCFRDataToAnalysis[] arr = chunker.GetArrayToAnalysis(chunks);

            DTMCFRClustering2 clustering = new DTMCFRClustering2();
            double[] means = new double[3] { 1.0, 3.0, 7.0 };   //initial means
            clustering.Cluster(arr, means);
            //Console.WriteLine($"means: {means[0]} | {means[1]} | {means[2]}");

            DTMCFRDecoder decoder = new DTMCFRDecoder();
            return decoder.TryDecodeAndMoveOneLeft(chunks, arr);
        }

        public string decodeMorse(string morseCode)
        {
            // Map morse code using map Preloaded.MORSE_CODE
            if (string.IsNullOrEmpty(morseCode) || string.IsNullOrWhiteSpace(morseCode)) return "";

            DTMCFRDecoder decoder = new DTMCFRDecoder();
            string res;
            decoder.TryDecodeMorse(morseCode, out res);
            return res;
        }
    }
}
