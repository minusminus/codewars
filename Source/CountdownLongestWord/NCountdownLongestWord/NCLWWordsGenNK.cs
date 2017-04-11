using System;
using System.Collections.Generic;
using CountdownLongestWord;
using NUnit.Framework;
using Shouldly;

namespace NCountdownLongestWord
{
    [TestFixture]
    public class NCLWWordsGenNK
    {
        private void DoTestNofK(string teststr, int k, List<string> expected)
        {
            List<string> testres = new List<string>();
            CLWWordsGenNK _pobj = new CLWWordsGenNK(teststr);
            
            _pobj.GenerateValues(k, (s)=> {testres.Add(s);});

            testres.Sort();
            expected.Sort();
            testres.ShouldBe(expected);
        }

        [Test]
        public void Test_5of3()
        {
            DoTestNofK("abcde", 3, new List<string>() {"abc", "abd", "acd", "bcd", "abe", "ace", "bce", "ade", "bde", "cde"});
        }
    }
}
