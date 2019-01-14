using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DecodeTheMorseCodeForReal;
using NUnit.Framework;
using Shouldly;

namespace NDecodeTheMorseCodeForReal
{
    [TestFixture]
    public class NDTMCFRInternalTests
    {
        private readonly DTMCFR _testObj = new DTMCFR();

        private void CheckChunkBits(string bits, List<DTMCFRDataChunk> expected)
        {
            List<DTMCFRDataChunk> data = _testObj.ChunkBits(bits);
            data.Count.ShouldBe(expected.Count);
            for (int i = 0; i < data.Count; i++)
            {
                data[i].Symbol.ShouldBe(expected[i].Symbol, $"{i}");
                data[i].Length.ShouldBe(expected[i].Length, $"{i}");
            }
        }

        [Test]
        public void ChunkTests()
        {
            CheckChunkBits("0000000011011010011100000100000",
                new List<DTMCFRDataChunk>()
                {
                    new DTMCFRDataChunk() {Symbol = '1', Length = 2},
                    new DTMCFRDataChunk() {Symbol = '0', Length = 1},
                    new DTMCFRDataChunk() {Symbol = '1', Length = 2},
                    new DTMCFRDataChunk() {Symbol = '0', Length = 1},
                    new DTMCFRDataChunk() {Symbol = '1', Length = 1},
                    new DTMCFRDataChunk() {Symbol = '0', Length = 2},
                    new DTMCFRDataChunk() {Symbol = '1', Length = 3},
                    new DTMCFRDataChunk() {Symbol = '0', Length = 5},
                    new DTMCFRDataChunk() {Symbol = '1', Length = 1},
                });

            CheckChunkBits("00000000110110100111000001",
                new List<DTMCFRDataChunk>()
                {
                    new DTMCFRDataChunk() {Symbol = '1', Length = 2},
                    new DTMCFRDataChunk() {Symbol = '0', Length = 1},
                    new DTMCFRDataChunk() {Symbol = '1', Length = 2},
                    new DTMCFRDataChunk() {Symbol = '0', Length = 1},
                    new DTMCFRDataChunk() {Symbol = '1', Length = 1},
                    new DTMCFRDataChunk() {Symbol = '0', Length = 2},
                    new DTMCFRDataChunk() {Symbol = '1', Length = 3},
                    new DTMCFRDataChunk() {Symbol = '0', Length = 5},
                    new DTMCFRDataChunk() {Symbol = '1', Length = 1},
                });

            CheckChunkBits("11011010011100000100000",
                new List<DTMCFRDataChunk>()
                {
                    new DTMCFRDataChunk() {Symbol = '1', Length = 2},
                    new DTMCFRDataChunk() {Symbol = '0', Length = 1},
                    new DTMCFRDataChunk() {Symbol = '1', Length = 2},
                    new DTMCFRDataChunk() {Symbol = '0', Length = 1},
                    new DTMCFRDataChunk() {Symbol = '1', Length = 1},
                    new DTMCFRDataChunk() {Symbol = '0', Length = 2},
                    new DTMCFRDataChunk() {Symbol = '1', Length = 3},
                    new DTMCFRDataChunk() {Symbol = '0', Length = 5},
                    new DTMCFRDataChunk() {Symbol = '1', Length = 1},
                });
        }
    }
}
