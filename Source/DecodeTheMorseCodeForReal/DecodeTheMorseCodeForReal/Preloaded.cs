using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecodeTheMorseCodeForReal
{
    class Preloaded
    {
        public static readonly Dictionary<string, char> MORSE_CODE = new Dictionary<string, char>
        {
            {".-", 'A'},
            {"-...", 'B'},
            {"-.-.", 'C'},
            {"-..", 'D'},
            {".", 'E'},
            {"..-.", 'F'},
            {"--.", 'G'},
            {"....", 'H'},
            {"..", 'I'},
            {".---", 'J'},
            {"-.-", 'K'},
            {".-..", 'L'},
            {"--", 'M'},
            {"-.", 'N'},
            {"---", 'O'},
            {".--.", 'P'},
            {"--.-", 'Q'},
            {".-.", 'R'},
            {"...", 'S'},
            {"-", 'T'},
            {"..-", 'U'},
            {"...-", 'V'},
            {".--", 'W'},
            {"-..-", 'X'},
            {"-.--", 'Y'},
            {"--..", 'Z'},
            {"-----", '0'},
            {".----", '1'},
            {"..---", '2'},
            {"...--", '3'},
            {"....-", '4'},
            {".....", '5'},
            {"-....", '6'},
            {"--...", '7'},
            {"---..", '8'},
            {"----.", '9'}
        };
    }
}
