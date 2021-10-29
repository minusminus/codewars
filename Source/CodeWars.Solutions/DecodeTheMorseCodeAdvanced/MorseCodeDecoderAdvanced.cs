/*
 * https://www.codewars.com/kata/54b72c16cd7f5154e9000457
 * Decode the Morse code, advanced
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars.Solutions.DecodeTheMorseCodeAdvanced
{
    /// <summary>
    /// assumptions:
    /// dot 1 unit, dash 3 units
    /// pause between: dots/dashes 1 unit, characters 3 units, words 7 units
    /// unit length is stable through transmission
    /// </summary>
    public class MorseCodeDecoderAdvanced
    {
        private enum UnitLength { shortUnit, longUnit, veryLongUnit };

        private class Chunk
        {
            public char Character;
            public int Length;
            public UnitLength UnitLength;

            public Chunk(char character, int length)
            {
                Character = character;
                Length = length;
                UnitLength = UnitLength.shortUnit;
            }
        }

        public static string DecodeBits(string bits)
        {
            Chunk[] chunks = SplitToChunks(RemoveLeadingAndTrailingZeros(bits)).ToArray();
            AssignUnitLengths(chunks, FindUnitLength(chunks));
            return ConvertChunksToMorseCode(chunks);
        }

        public static string DecodeMorse(string morseCode) =>
            string.Join(
                "",
                morseCode
                    .Replace("   ", " | ")
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => (s == "|") ? " " : MorseCode.Get(s))
            );

        private static string RemoveLeadingAndTrailingZeros(string value) =>
            value.Trim('0');

        private static IEnumerable<Chunk> SplitToChunks(string bits)
        {
            if (string.IsNullOrWhiteSpace(bits)) yield break;

            Chunk currentChunk = new Chunk('1', 0);
            foreach (char c in bits)
            {
                if (currentChunk.Character == c)
                    currentChunk.Length++;
                else
                {
                    yield return currentChunk;
                    currentChunk = new Chunk(c, 1);
                }
            }
            yield return currentChunk;
        }

        private static int FindUnitLength(Chunk[] chunks) => 
            chunks.Min(ch => ch.Length);

        private static void AssignUnitLengths(Chunk[] chunks, int unitLength)
        {
            foreach (Chunk chunk in chunks)
                chunk.UnitLength = GetChunkUnitLength(chunk, unitLength);
        }

        private static UnitLength GetChunkUnitLength(Chunk chunk, int unitLength)
        {
            switch(chunk.Length / unitLength)
            {
                case 1: return UnitLength.shortUnit;
                case 3: return UnitLength.longUnit;
                default: return UnitLength.veryLongUnit;
            }
        }

        private static string ConvertChunksToMorseCode(Chunk[] chunks) =>
            string.Join("", chunks.Select(MapChunkToMorseCodeChar));

        private static string MapChunkToMorseCodeChar(Chunk chunk)
        {
            if (chunk.Character == '1')
                return (chunk.UnitLength == UnitLength.shortUnit) ? "." : "-";
            switch (chunk.UnitLength)
            {
                case UnitLength.shortUnit: return string.Empty;
                case UnitLength.longUnit: return new string(' ', 1);
                default: return new string(' ', 3);
            }
        }
    }
}
