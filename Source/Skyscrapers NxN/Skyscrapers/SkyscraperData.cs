using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscrapers
{
    class SkyscraperData
    {
        //public int size;
        public readonly int[,] Data;

        public SkyscraperData(int N)
        {
            //size = N;
            Data = new int[N, N];
        }

        public SkyscraperData(SkyscraperData obj)
        {
            //size = obj.size;
            Data = (int[,]) obj.Data.Clone();
        }
    }
}
