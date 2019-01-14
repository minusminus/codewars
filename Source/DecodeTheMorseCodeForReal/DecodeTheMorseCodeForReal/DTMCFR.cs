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

        public string decodeBitsAdvanced(string bits)
        {
            return "";
        }

        public string decodeMorse(string morseCode)
        {
            // Map morse code using map Preloaded.MORSE_CODE
            return "";
        }
    }
}
