using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownLongestWord
{
    public class CountdownLongestWord
    {
        private readonly string[] _words = File.ReadAllLines(@"d:\projects\codewars_github\tools---numbertheory\Source\CountdownLongestWord\google-10000-english-no-swears.txt");

        private readonly CLWWordsDict _clw = new CLWWordsDict();

        public string[] LongestWord(string letters)
        {
            if (String.IsNullOrEmpty(letters)) return null;
            if (_clw.WordsDict.Count == 0) _clw.PrepareWordsDict(_words);

            char[] splitted = _clw.StringToSortedLetters(letters);
                
            return null;
        }
    }
}
