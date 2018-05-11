using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscrapers
{
    public static class SkyscrapersCounters
    {
        public static int NewData;
        public static int ReduceRCIters;
        public static int ReduceRCLoops;
        public static int ReduceRCLoopsRemoves;
        public static long ReduceRCReductions;
        public static long ReduceRCDoublesCut;
        public static int CheckDataCorrectCalls;
        public static int CorrectData;
        public static int FirstCorrectDataInCall;
        public static int IncorrectDataCount;
        public static long SetRowsCols;
        public static long CountBits;
        public static int Level;

        public static void Clear()
        {
            NewData = 0;
            ReduceRCIters = 0;
            ReduceRCLoops = 0;
            ReduceRCLoopsRemoves = 0;
            ReduceRCDoublesCut = 0;
            ReduceRCReductions = 0;
            CheckDataCorrectCalls = 0;
            CorrectData = 0;
            FirstCorrectDataInCall = 0;
            IncorrectDataCount = 0;
            SetRowsCols = 0;
            CountBits = 0;
            Level = 0;
        }
    }
}
