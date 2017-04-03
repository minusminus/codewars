using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CountdownLongestWord
{
    public class CLWWordsDict
    {
        /// <summary>
        /// slownik slow dla posortowanych liter
        /// </summary>
        public Dictionary<string, List<string>> WordsDict { get; } = new Dictionary<string, List<string>>();
        /// <summary>
        /// licznik slow zaczynajacych sie od danej litery (dla slow z posortowanych liter)
        /// </summary>
        public Dictionary<char, int> WordsStartsWith { get; } = new Dictionary<char, int>();

        public char[] StringToSortedLetters(string s)
        {
            char[] res = s.ToCharArray();
            Array.Sort(res);
            return res;
        }

        public string LettersToString(char[] letters)
        {
            return letters.ToString();
        }

        private void PrepareWordsDict(string[] words)
        {
            foreach (string word in words)
            {
                string w = word.ToUpper();
                string s = LettersToString(StringToSortedLetters(w));
                    //slownik slow dla posortowanych liter
                List<string> ls;
                if (WordsDict.TryGetValue(s, out ls))
                {
                    ls.Add(word);
                }
                else
                {
                    ls = new List<string>() { w };
                    WordsDict.Add(s, ls);
                }
                    //licznik slow zaczynajacych sie od okreslonej litery
                int i;
                if (WordsStartsWith.TryGetValue(s[0], out i))
                {
                    WordsStartsWith[s[0]] = i + 1;
                }
                else
                {
                    WordsStartsWith.Add(s[0], 1);
                }
            }
        }
    }
}
