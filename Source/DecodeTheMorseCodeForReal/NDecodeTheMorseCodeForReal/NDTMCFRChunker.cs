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
    public class NDTMCFRChunker
    {
        private readonly DTMCFRChunker _testObj = new DTMCFRChunker();

        private List<DTMCFRDataChunk> CreateDefaultChunksList()
        {
            //110110100111000001
            return new List<DTMCFRDataChunk>()
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
            };
        }

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
            List<DTMCFRDataChunk> expected = CreateDefaultChunksList();

            CheckChunkBits("0000000011011010011100000100000", expected);
            CheckChunkBits("00000000110110100111000001", expected);
            CheckChunkBits("11011010011100000100000", expected);
        }
    }
}
