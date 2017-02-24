using System;
using NUnit.Framework;
using Shouldly;
using DecodeTheMorseCode;

namespace N_DecodeTheMorseCode
{
    [TestFixture]
    public class N_MorseCodeDecoder
    {
        private static MorseCodeDecoder _test = new MorseCodeDecoder();

        [Test]
        public void OriginalTest()
        {
            _test.Decode(".... . -.--   .--- ..- -.. .").ShouldBe("HEY JUDE");
        }

        [Test]
        public void Tests1()
        {
            _test.Decode("").ShouldBe("");
            _test.Decode("----- .---- ..---   ...-- ....- .....   -.... --... ---..   ----.").ShouldBe("012 345 678 9");
            _test.Decode(". -- .. .-.. .. .-").ShouldBe("EMILIA");
        }
    }
}