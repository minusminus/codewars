using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace NCountdownLongestWord
{
    [TestFixture]
    public class NCountdownLongestWord
    {
        private static readonly string[] _words10k = File.ReadAllLines(
            Path.Combine(
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), 
                @"\CountdownLongestWord\google-10000-english-no-swears.txt")
            );

        private readonly CountdownLongestWord.CountdownLongestWord _pobj = new CountdownLongestWord.CountdownLongestWord();
        private readonly CountdownLongestWord.CLWWordsDict _wd = new CountdownLongestWord.CLWWordsDict();


        [Test]
        public void TestTryGetValue()
        {
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            dict.Add("abcd", new List<string>() {"abcd", "bacd"});

            foreach (List<string> value in dict.Values)
                foreach (string s in value)
                    Console.Write($"{s},");
            Console.WriteLine();

            List<string> ls;
            if (dict.TryGetValue("abcd", out ls))
            {
                ls.Add("abdc");
            }

            foreach (List<string> value in dict.Values)
                foreach (string s in value)
                    Console.Write($"{s},");
            Console.WriteLine();

            if (dict.TryGetValue("abcd", out ls))
            {
                ls.Add("badc");
            }

            foreach (List<string> value in dict.Values)
                foreach (string s in value)
                    Console.Write($"{s},");
            Console.WriteLine();
        }

        [Test]
        public void TestTryGetInt()
        {
            Dictionary<char, int> dict = new Dictionary<char, int>();
            dict.Add('1', 1);
            int i;

            foreach (KeyValuePair<char, int> pair in dict)
                Console.Write($"{pair.Key}={pair.Value},");
            Console.WriteLine();

            if (dict.TryGetValue('1', out i)) i = 10;
            foreach (KeyValuePair<char, int> pair in dict)
                Console.Write($"{pair.Key}={pair.Value},");
            Console.WriteLine();

            if (dict.TryGetValue('1', out i)) dict['1'] = 10;
            foreach (KeyValuePair<char, int> pair in dict)
                Console.Write($"{pair.Key}={pair.Value},");
            Console.WriteLine();

            dict['2'] = 2;
            foreach (KeyValuePair<char, int> pair in dict)
                Console.Write($"{pair.Key}={pair.Value},");
            Console.WriteLine();

            //dict['3'] = dict['3'] + 2;
            //foreach (KeyValuePair<char, int> pair in dict)
            //    Console.Write($"{pair.Key}={pair.Value},");
            //Console.WriteLine();
        }

        [Test]
        public void CLWWordsDictStringConversions()
        {
            _wd.StringToSortedLetters("dbac").ShouldBe("abcd");
            _wd.LettersToString(new char[] {'a', 'b', 'c', 'd'}).ShouldBe("abcd");
        }

        [Test]
        public void WordDictTest()
        {
            string[] words = new string[4] {"abcd", "ab", "zyx", "utyg"};

            _wd.PrepareWordsDict(words);

            _wd.WordsDict.Sum(x => x.Value.Count).ShouldBe(words.Length);
            _wd.WordsStartsWith.Sum(x => x.Value).ShouldBe(words.Length);
            _wd.WordsLengthCount.Sum(x => x.Value).ShouldBe(words.Length);
            _wd.WordsDict.Count.ShouldBe(4);
            _wd.WordsStartsWith.Count.ShouldBe(3);
            foreach (KeyValuePair<char, int> pair in _wd.WordsStartsWith)
            {
                Console.WriteLine($"{pair.Key}={pair.Value}");
            }
        }

        [Test]
        public void WordDictTestFromFile()
        {
            string[] words = File.ReadAllLines(@"d:\projects\codewars_github\tools---numbertheory\Source\CountdownLongestWord\google-10000-english-no-swears.txt");
            _wd.PrepareWordsDict(words);

            _wd.WordsDict.Sum(x => x.Value.Count).ShouldBe(words.Length);
            _wd.WordsStartsWith.Sum(x => x.Value).ShouldBe(words.Length);
            _wd.WordsLengthCount.Sum(x => x.Value).ShouldBe(words.Length);
            Console.WriteLine($"sorted letters words: {_wd.WordsDict.Count} (of {words.Count()})");
            Console.WriteLine($"starts with: {_wd.WordsStartsWith.Count}");
            //int i = 1;
            //foreach (KeyValuePair<char, int> pair in _wd.WordsStartsWith)
            //{
            //    Console.WriteLine($"{i}: {pair.Key}={pair.Value} (of {words.Count()}, {100.0 * (float)pair.Value / (float)words.Count()} %)");
            //    i++;
            //}
            Console.WriteLine($"starts with 3 letters: {_wd.WordsStartsWith3Letters.Count} (total words {_wd.WordsStartsWith3Letters.Sum(x => x.Value)})");
            //i = 1;
            //foreach (KeyValuePair<string, int> pair in _wd.WordsStartsWith3Letters)
            //{
            //    Console.WriteLine($"{i}: {pair.Key}={pair.Value} (of {words.Count()}, {100.0 * (float)pair.Value / (float)words.Count()} %)");
            //    i++;
            //}
            Console.WriteLine($"words length: {_wd.WordsLengthCount.Count}");
            foreach (KeyValuePair<int, int> pair in _wd.WordsLengthCount)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
        }

        [Test]
        public void KataBasicTests()
        {
            _pobj.LongestWord("POVMERKIA", _words10k).ShouldBe(new string[] { "IMPROVE", "VAMPIRE" });

            //_pobj.LongestWord("DVAVPALEM").ShouldBe(new string[] { "VAMPED", "VALVED", "PALMED" });
            _pobj.LongestWord("DVAVPALEM", _words10k).ShouldBe(new string[] { "PAMELA" });
        }

        [Test]
        public void ShortBasicTests()
        {
            _pobj.LongestWord("TVAK", _words10k).ShouldBe(new string[] {"VAT"});
        }

        [Test]
        public void NoRepeatingWordsTest()
        {
            _pobj.LongestWord("EAEEAYITB", _words10k).ShouldBe(new string[] { "BEAT", "BETA", "BITE", "BYTE", "EBAY", "IEEE" });
        }
    }
}