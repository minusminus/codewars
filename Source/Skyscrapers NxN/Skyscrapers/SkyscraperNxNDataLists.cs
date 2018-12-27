using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscrapers
{
    public class SkyscraperNxNDataLists
    {
        public readonly SkyscrapersNxNDataObject[] Lists;

        public SkyscraperNxNDataLists(int[] constraints, int n, SkyscrapersPrecalcData precalc)
        {
            Lists = new SkyscrapersNxNDataObject[4 * n];  //tablica list zainicjalizowana nulami

            //zainicjalizowanie list dla wszystkich ograniczen
            for (int i = 0; i < Lists.Length; i++)
                if (constraints[i] != 0)
                    Lists[i] = new SkyscrapersNxNDataObject(precalc.GetList(constraints[i]));
        }

        public SkyscraperNxNDataLists(SkyscraperNxNDataLists src)
        {
            Lists = new SkyscrapersNxNDataObject[src.Lists.Length];  //tablica list zainicjalizowana nulami

            //skopiowanie list ze zrodla
            for (int i = 0; i < src.Lists.Length; i++)
                if (src.Lists[i] != null)
                    Lists[i] = new SkyscrapersNxNDataObject(src.Lists[i]);
        }
    }
}
