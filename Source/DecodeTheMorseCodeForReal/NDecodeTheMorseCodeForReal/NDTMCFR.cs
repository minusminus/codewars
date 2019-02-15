﻿using System;
using DecodeTheMorseCodeForReal;
using NUnit.Framework;
using Shouldly;

namespace NDecodeTheMorseCodeForReal
{
    [TestFixture]
    public class NDTMCFR
    {
        private readonly DTMCFR _testObj = new DTMCFR();

        private void ExecTest(string bits, string expected)
        {
            _testObj.decodeMorse(_testObj.decodeBitsAdvanced(bits)).ShouldBe(expected);
        }

        [Test]
        public void BasicKataTest()
        {
            ExecTest(
                "0000000011011010011100000110000001111110100111110011111100000000000111011111111011111011111000000101100011111100000111110011101100000100000",
                "HEY JUDE");
        }

        [Test]
        public void SingleWord()
        {
            ExecTest(
                "000000001101101001110000011000000111111010011111001111110000000000",
                "HEY");
        }

        [Test]
        public void SingleWord2()
        {
            ExecTest(
                "00000000000111011111111011111011111000000101100011111100000111110011101100000100000",
                "JUDE");
        }

        [Test]
        public void DashesOnly()
        {
            ExecTest(
                "000000001111011110111100011110111101111000111101111011110000000000",
                "OOO");
        }

        [Test]
        public void DashesOnlyShorter()
        {
            ExecTest(
                "000000001111011110001111011110001111011110000000000",
                "MMM");
        }

        [Test]
        public void DotsOnly()
        {
            ExecTest(
                //"000000001101101101100011011011011000110110110110000000000000"))  //ten przyklad nie pasuje do podzialu 1-3-7, przerwa pomiedzy znakami jest za krotka
                "00000000110011001100110000110011001100110000110011001100110000000000000",
                "HHH");
        }

        [Test]
        public void ESpaceETest()
        {
            ExecTest(
                "10000001",
                "E E");
        }

        [Test]
        public void EmptyString()
        {
            ExecTest(
                "",
                "");
        }

        [Test]
        public void OnlyZeroes()
        {
            ExecTest(
                "00000000000000000000",
                "");
        }

        [Test]
        public void KataShortMessages()
        {
            ExecTest(
                "0",
                "");
            ExecTest(
                "000000000000000000000000000000000000000000",
                "");
            ExecTest(
                "1",
                "E");
            ExecTest(
                "101",
                "I");
            ExecTest(
                "1001",
                "EE");
        }

        [Test]
        public void KataLongMessage()
        {
            ExecTest(
                "1100110011001100000011000000111111001100111111001111110000000000000011001111110011111100111111000000110011001111110000001111110011001100000011",
                "HEY JUDE");
            ExecTest(
                "00000000000111111100000011010001110111000000001110000000000000000001111111011111100001101111100000111100111100011111100000001011100000011111110010001111100110000011111100101111100000000000000111111100001111010110000011000111110010000011111110001111110011111110000010001111110001111111100000001111111101110000000000000010110000111111110111100000111110111110011111110000000011111001011011111000000000000111011111011111011111000000010001001111100000111110111111110000001110011111100011111010000001100001001000000000000000000111111110011111011111100000010001001000011111000000100000000101111101000000000000011111100000011110100001001100000000001110000000000000001101111101111000100000100001111111110000000001111110011111100011101100000111111000011011111000111111000000000000000001111110000100110000011111101111111011111111100000001111110001111100001000000000000000000000000000000000000000000000000000000000000",
                "");
        }

        [Test]
        public void KataMultipleBitsPerDot()
        {
            ExecTest(
                "111",
                "E");
            ExecTest(
                "1111111",
                "E");
            ExecTest(
                "110011",
                "I");
            ExecTest(
                "111110000011111",
                "I");
            ExecTest(
                "11111100111111",
                "M");
        }

        [Test]
        public void KataExtraZeros()
        {
            ExecTest(
                "01110",
                "E");
        }

        [Test]
        public void KataFinalTest()
        {
            ExecTest(
                "00000000000000011111111000000011111111111100000000000111111111000001111111110100000000111111111111011000011111111011111111111000000000000000000011111111110000110001111111111111000111000000000001111111111110000111111111100001100111111111110000000000111111111111011100001110000000000000000001111111111010111111110110000000000000001111111111100001111111111110000100001111111111111100000000000111111111000000011000000111000000000000000000000000000011110001111100000111100000000111111111100111111111100111111111111100000000011110011111011111110000000000000000000000111111111110000000011111000000011111000000001111111111110000000001111100011111111000000000111111111110000011000000000111110000000111000000000011111111111111000111001111111111001111110000000000000000000001111000111111111100001111111111111100100000000001111111100111111110111111110000000011101111111000111000000001001111111000000001111111111000000000111100001111111000000000000011111111100111111110111111111100000000000111111110000001100000000000000000000111111101010000010000001111111100000000011111000111111111000000111111111110011111111001111111110000000011000111111110000111011111111111100001111100001111111100000000000011110011101110001000111111110000000001111000011111110010110001111111111000000000000000000111111111110000000100000000000000000011110111110000001000011101110000000000011111111100000011111111111100111111111111000111111111000001111111100000000000001110111111111111000000110011111111111101110001111111111100000000111100000111100000111111111100000111111111111000000011111111000000000001000000111100000001000001111100111111111110000000000000000000010001111111100000011111111100000000000000100001111111111110111001111111111100000111111100001111111111000000000000000000000000011100000111111111111011110000000010000000011111111100011111111111100001110000111111111111100000000000000111110000011111001111111100000000000011100011100000000000011111000001111111111101000000001110000000000000000000000000000111110010000000000111111111000011111111110000000000111111111111101111111111100000000010000000000000011111111100100001100000000000000111100111100000000001100000001111111111110000000011111111111000000000111100000000000000000000111101111111111111000000000001111000011111000011110000000001100111111100111000000000100111000000000000111110000010000011111000000000000001111111111100000000110111111111100000000000000111111111111100000111000000000111111110001111000000111111110111111000000001111000000000010000111111111000011110001111111110111110000111111111111000000000000000000000000111111111110000000111011111111100011111110000000001111111110000011111111100111111110000000001111111111100111111111110000000000110000000000000000001000011111111110000000001111111110000000000000000000000011111111111111000000111111111000001111111110000000000111111110000010000000011111111000011111001111111100000001110000000011110000000001011111111000011111011111111110011011111111111000000000000000000100011111111111101111111100000000000000001100000000000000000011110010111110000000011111111100000000001111100011111111111101100000000111110000011110000111111111111000000001111111111100001110111111111110111000000000011111111101111100011111111110000000000000000000000000010000111111111100000000001111111110111110000000000000000000000110000011110000000000001111111111100110001111111100000011100000000000111110000000011111111110000011111000001111000110000000011100000000000000111100001111111111100000111000000001111111111000000111111111100110000000001111000001111111100011100001111111110000010011111111110000000000000000000111100000011111000001111000000000111111001110000000011111111000100000000000011111111000011001111111100000000000110111000000000000111111111111000100000000111111111110000001111111111011100000000000000000000000000",
                "E");
            //11111111 0000000 111111111111 00000000000 111111111 00000 111111111 0 1 00000000 111111111111 0 11 0000 11111111 0 11111111111 0000000000000000000
            //8        7       12           11          9         5     9         1 1 8        12           1 2  4    8        1 11          19
            //-- --. -.-- MGY (8-cluster 1)
            //.- --.-..- A? (8-cluster 0)
            //.- --. -..- AGX (8- 1-0, 0-1)
            //-- --.-.-- M?  (8- 1-1, 0-0)
            //- - --. -.-- TTGY (7-cluster 1)
        }
    }
}