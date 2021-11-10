using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeWars.Katas.HeroSuffixQuest;
using NUnit.Framework;

namespace CodeWars.Katas.Tests.HeroSuffixQuest
{
    [TestFixture]
    public class HeroTests
    {
        private readonly Random _random = new Random();
        private readonly char[] _randomizerAlphabet = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

        [TestCase("", 1, "")]
        [TestCase("abcd", -1, "")]
        [TestCase("abcd", 0, "abcd")]
        [TestCase("abcd", 1, "bcd")]
        [TestCase("abcd", 2, "cd")]
        [TestCase("abcd", 3, "d")]
        [TestCase("abcd", 4, "")]
        [TestCase("dcba", -1, "")]
        [TestCase("dcba", 0, "a")]
        [TestCase("dcba", 1, "ba")]
        [TestCase("dcba", 2, "cba")]
        [TestCase("dcba", 3, "dcba")]
        [TestCase("dcba", 4, "")]
        [Order(1)]  //these test should run first
        public void FindPassword__ReturnsCorrectly(string engravedText, int numberOnTheDoor, string expected)
        {
            Assert.AreEqual(expected, Hero.FindPassword(engravedText, numberOnTheDoor));
        }

        [TestCase(10, 1000)]    //17 ms
        [TestCase(10, 10000)]   //215 ns
        [TestCase(10, 20000)]   //480 ms
        [TestCase(10, 30000)]   //750 ms
        [TestCase(10, 40000)]   //1 sec
        //[TestCase(10, 50000)]   //1.3 sec
        //[TestCase(10, 100000)]  //2.8 sec
        //[TestCase(10, 1000000)]   //40 sec
        [TestCase(100, 1000)]   //155 ms
        [TestCase(100, 2000)]   //340 ms
        [TestCase(100, 3000)]   //530 ms
        [TestCase(100, 4000)]   //740 ms
        [TestCase(100, 5000)]   //940 ms
        //[TestCase(100, 10000)]  //2.2 sec
        //[TestCase(100, 100000)] //28 sec
        public void AreYouFasterThanWater(int numberOfDoors, int textLength)
        {
            for(int door = 0; door<numberOfDoors; door++)
            {
                string engravedText = GetRandomString(textLength);
                int numberOnTheDoor = GetRandomNumberOnTheDoor(textLength);
                string expected = Hero.FindPassword(engravedText, numberOnTheDoor);
                Assert.AreEqual(expected, Hero.FindPassword(engravedText, numberOnTheDoor));
            }
        }

        private string GetRandomString(int length)
        {
            char[] stringChars = new char[length];
            for (int i = 0; i < length; i++)
                stringChars[i] = _randomizerAlphabet[_random.Next(_randomizerAlphabet.Length)];
            return new string(stringChars);
        }

        private int GetRandomNumberOnTheDoor(int textLength) =>
            _random.Next(textLength);
    }
}
