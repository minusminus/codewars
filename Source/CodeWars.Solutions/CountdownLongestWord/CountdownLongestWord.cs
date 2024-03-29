﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CountdownLongestWord
{
    public class CountdownLongestWord
    {
        //private readonly string[] _words = File.ReadAllLines(@"d:\projects\codewars_github\tools---numbertheory\Source\CountdownLongestWord\google-10000-english-no-swears.txt");
        //private string[] _words;

        private readonly CLWWordsDict _clw = new CLWWordsDict();

        private readonly List<string> _generatedWords = new List<string>();

        //public CountdownLongestWord(string[] wordsdict)
        //{
        //    _words = wordsdict;
        //    _clw.PrepareWordsDict(_words);
        //}

        private void CheckGeneratedString(string s)
        {
            List<string> sl;
            if (_clw.WordsDict.TryGetValue(s, out sl))
            {
                //Console.WriteLine(string.Join(",", sl.ToArray()));
                //_generatedWords.AddRange(sl);
                foreach (string str in sl)
                {
                    if(_generatedWords.IndexOf(str) ==-1)
                        _generatedWords.Add(str);
                }
            }
        }

        public string[] LongestWord(string letters, string[] wordsdict)
        {
            if (String.IsNullOrEmpty(letters)) return null;
            _generatedWords.Clear();
            if (_clw.WordsDict.Count == 0) _clw.PrepareWordsDict(wordsdict);

            char[] splitted = _clw.StringToSortedLetters(letters);
            CLWWordsGenNK gen = new CLWWordsGenNK(splitted);

            for (int k = splitted.Length; k > 0; k--)
            {
                int n;
                if(_clw.WordsLengthCount.TryGetValue(k, out n))
                    if (n > 0)
                    {
                        gen.GenerateValues(k, CheckGeneratedString);
                        if (_generatedWords.Count > 0) break;
                    }
            }

            if (_generatedWords.Count == 0)
                return null;
            return _generatedWords.OrderBy(i => i).ToArray();
        }
    }
}
