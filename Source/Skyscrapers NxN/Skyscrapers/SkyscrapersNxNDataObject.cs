using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscrapers
{
    public class SkyscrapersNxNDataObject
    {
        public readonly List<int[]> PrecalcData;
        public readonly List<int> Idx;

        public SkyscrapersNxNDataObject(List<int[]> precalcData )
        {
            PrecalcData = precalcData;
            Idx = Enumerable.Range(0, PrecalcData.Count).ToList();
        }
    }
}
