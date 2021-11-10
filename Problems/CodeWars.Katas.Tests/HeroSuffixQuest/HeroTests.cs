using System;
using System.Collections.Generic;
using CodeWars.Katas.HeroSuffixQuest;
using NUnit.Framework;

namespace CodeWars.Katas.Tests.HeroSuffixQuest
{
    [TestFixture]
    public class HeroTests
    {
        private const int MaxTextLength = 100000;

        private readonly Random _random = new Random();
        private readonly char[] _randomizerAlphabet = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        private readonly char[] _preinitializedRandomChars = new char[MaxTextLength];

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            for (int i = 0; i < _preinitializedRandomChars.Length; i++)
                _preinitializedRandomChars[i] = _randomizerAlphabet[_random.Next(_randomizerAlphabet.Length)];
        }

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
        [Order(1)]  //these tests should be run first
        public void FindPassword__ReturnsCorrectly(string engravedText, int numberOnTheDoor, string expected)
        {
            Assert.AreEqual(expected, Hero.FindPassword(engravedText, numberOnTheDoor));
        }

        [TestCase(1000)]    //17 ms
        [TestCase(10000)]   //205 ns
        [TestCase(20000)]   //450 ms
        [TestCase(30000)]   //705 ms
        [TestCase(40000)]   //980 ms
        //[TestCase(50000)]   //1.3 sec
        //[TestCase(100000)]  //2.8 sec
        //[TestCase(1000000)]   //40 sec
        public void AreYouFasterThanWater_10Doors(int textLength)
        {
            TestWithRandomString(10, textLength);
        }

        [TestCase(1000)]   //155 ms
        [TestCase(2000)]   //330 ms
        [TestCase(3000)]   //520 ms
        [TestCase(4000)]   //710 ms
        [TestCase(5000)]   //925 ms
        //[TestCase(10000)]  //2.2 sec
        //[TestCase(100000)] //28 sec
        public void AreYouFasterThanWater_100Doors(int textLength)
        {
            TestWithRandomString(100, textLength);
        }

        [Test]
        public void GetRandomStringTest([Values(10, 100)] int iterations, [Values(1000, 10000, 40000)] int textLength)
        {
            List<string> tmp = new List<string>();
            for (int i = 0; i < iterations; i++)
                tmp.Add(GetRandomString(textLength));
            Assert.IsNotEmpty(tmp);
        }

        [Test]
        public void GetRandomStringPreinitializedTest([Values(10, 100)] int iterations, [Values(1000, 10000, 40000)] int textLength)
        {
            List<string> tmp = new List<string>();
            for (int i = 0; i < iterations; i++)
                tmp.Add(GetRandomStringPreinitialized(textLength));
            Assert.IsNotEmpty(tmp);
        }

        private void TestWithRandomString(int numberOfDoors, int textLength)
        {
            Assert.LessOrEqual(textLength, MaxTextLength);
            for (int door = 0; door < numberOfDoors; door++)
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

        private string GetRandomStringPreinitialized(int length)
        {
            int randomStart = _random.Next(_preinitializedRandomChars.Length - length);
            char[] stringChars = new char[length];
            for (int i = 0; i < length; i++)
                stringChars[i] = _preinitializedRandomChars[randomStart + i];
            return new string(stringChars);
        }

        private int GetRandomNumberOnTheDoor(int textLength) =>
            _random.Next(textLength);
    }
}
