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

        [Test]
        public void SingleWord2()
        {
            _testObj.decodeMorse(
                _testObj.decodeBitsAdvanced(
                    "00000000000111011111111011111011111000000101100011111100000111110011101100000100000"))
                .ShouldBe("JUDE");
        }

        [Test]
        public void DashesOnly()
        {
            _testObj.decodeMorse(
                _testObj.decodeBitsAdvanced(
                    "000000001111011110111100011110111101111000111101111011110000000000"))
                .ShouldBe("OOO");
        }

        [Test]
        public void DashesOnlyShorter()
        {
            _testObj.decodeMorse(
                _testObj.decodeBitsAdvanced(
                    "000000001111011110001111011110001111011110000000000"))
                .ShouldBe("MMM");
        }

        [Test]
        public void DotsOnly()
        {
            _testObj.decodeMorse(
                _testObj.decodeBitsAdvanced(
                    "000000001101101101100011011011011000110110110110000000000000"))
                .ShouldBe("HHH");
        }

        [Test]
        public void ESpaceETest()
        {
            _testObj.decodeMorse(
                _testObj.decodeBitsAdvanced(
                    "10000001"))
                .ShouldBe("E E");
        }

        [Test]
        public void EmptyString()
        {
            _testObj.decodeMorse(
                _testObj.decodeBitsAdvanced(
                    ""))
                .ShouldBe("");
        }

        [Test]
        public void OnlyZeroes()
        {
            _testObj.decodeMorse(
                _testObj.decodeBitsAdvanced(
                    "00000000000000000000"))
                .ShouldBe("");
        }
    }
}