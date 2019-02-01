using System;
using DecodeTheMorseCodeForReal;
using NUnit.Framework;
using Shouldly;

namespace NDecodeTheMorseCodeForReal
{
    [TestFixture]
    public class NDTMCFR
    {
        private readonly DTMCFR _testObj = new DTMCFR();

        [Test]
        public void BasicKataTest()
        {
            _testObj.decodeMorse(
                _testObj.decodeBitsAdvanced(
                    "0000000011011010011100000110000001111110100111110011111100000000000111011111111011111011111000000101100011111100000111110011101100000100000"))
                .ShouldBe("HEY JUDE");
        }

        [Test]
        public void SingleWord()
        {
            _testObj.decodeMorse(
                _testObj.decodeBitsAdvanced(
                    "000000001101101001110000011000000111111010011111001111110000000000"))
                .ShouldBe("HEY");
        }
    }
}