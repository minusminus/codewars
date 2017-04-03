using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NCountdownLongestWord
{
    [TestFixture]
    public class NCountdownLongestWord
    {
        private readonly CountdownLongestWord.CountdownLongestWord _pobj = new CountdownLongestWord.CountdownLongestWord();

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
    }
}