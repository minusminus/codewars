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

        [Test]
        public void Test_3of3()
        {
            DoTestNofK("abc", 3, new List<string>() { "abc" });
        }

        [Test]
        public void Test_3of1()
        {
            DoTestNofK("abc", 1, new List<string>() { "a", "b", "c" });
        }

        [Test]
        public void Test_5of2()
        {
            DoTestNofK("abcde", 2, new List<string>() { "ab", "ac", "bc", "ad", "bd", "cd", "ae", "be", "ce", "de" });
        }

        [Test]
        public void Test_5of4()
        {
            DoTestNofK("abcde", 4, new List<string>() { "abcd", "acde", "abde", "abce", "bcde" });
        }
    }
}
