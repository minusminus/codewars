using System.Collections.Generic;
using System.Text;

namespace R13
{
    public class Rot13
    {
        private static Dictionary<char, char> tblDecoder = new Dictionary<char, char>() {
            {'A','N'},  {'B','O'},  {'C','P'},  {'D','Q'},  {'E','R'},  {'F','S'},  {'G','T'},  {'H','U'},  {'I','V'},
            {'J','W'},  {'K','X'},  {'L','Y'},  {'M','Z'},  {'N','A'},  {'O','B'},  {'P','C'},  {'Q','D'},  {'R','E'},
            {'S','F'},  {'T','G'},  {'U','H'},  {'V','I'},  {'W','J'},  {'X','K'},  {'Y','L'},  {'Z','M'},  {'a','n'},
            {'b','o'},  {'c','p'},  {'d','q'},  {'e','r'},  {'f','s'},  {'g','t'},  {'h','u'},  {'i','v'},  {'j','w'},
            {'k','x'},  {'l','y'},  {'m','z'},  {'n','a'},  {'o','b'},  {'p','c'},  {'q','d'},  {'r','e'},  {'s','f'},
            {'t','g'},  {'u','h'},  {'v','i'},  {'w','j'},  {'x','k'},  {'y','l'},  {'z','m'}
        };

        public string Decode(string encoded)
        {
            StringBuilder res = new StringBuilder();
            foreach(char c in encoded)
            {
                char decoded;
                if (tblDecoder.TryGetValue(c, out decoded))
                    res.Append(decoded);
                else
                    res.Append(c);
            }
            return res.ToString();
        }

        public string Encode(string message)
        {
            StringBuilder res = new StringBuilder();
            foreach (char c in message)
            {
                char decoded;
                if (tblDecoder.TryGetValue(c, out decoded))
                    res.Append(decoded);
                else
                    res.Append(c);
            }
            return res.ToString();
        }
    }
}
