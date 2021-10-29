using NUnit.Framework;
using Shouldly;
using CodeWars.Solutions.DecodeTheMorseCodeAdvanced;

namespace CodeWars.Solutions.Tests.DecodeTheMorseCodeAdvanced
{
    [TestFixture]
    public class MorseCodeDecoderAdvancedTests
    {
        [TestCase("1100110011001100000011000000111111001100111111001111110000000000000011001111110011111100111111000000110011001111110000001111110011001100000011",
            ".... . -.--   .--- ..- -.. .")]
        [TestCase("00110011001100110000001100000011111100110011111100111111000000000000001100111111001111110011111100000011001100111111000000111111001100110000001100",
            ".... . -.--   .--- ..- -.. .")]
        [TestCase("10101010001000111010111011100000001011101110111000101011100011101010001",
            ".... . -.--   .--- ..- -.. .")]
        public void DecodeBits__DecodesCorrectly(string bits, string expected)
        {
            MorseCodeDecoderAdvanced.DecodeBits(bits).ShouldBe(expected);
        }

        [TestCase(".... . -.--   .--- ..- -.. .", "HEY JUDE")]
        public void DecodeMorse__DecodesCorrectly(string morseCode, string expected)
        {
            MorseCodeDecoderAdvanced.DecodeMorse(morseCode).ShouldBe(expected);
        }
    }
}
