using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecodeTheMorseCodeForReal
{
    /// <summary>
    /// Mechanizm dzielacy przygotowane tablice sekwencji 0 i 1 na N części. Bazuje na "k-means clustering".
    /// Zaklada ze wejsciowe tablice sa posortowane.
    /// </summary>
    public class DTMCFRClustering
    {
        public void NormalizeData(DTMCFRDataToAnalysis[] data)
        {
            double normCoef = 1.0/(data[data.Length - 1].Length - data[0].Length);

            data[0].NormalizedLength = 0;
            data[data.Length - 1].NormalizedLength = 1;
            for (int i = 1; i <= data.Length - 2; i++)
                data[i].NormalizedLength = (data[i].Length - data[0].Length)*normCoef;
        }

        public int[] InitializeBorders(int meansCount)
        {
            int[] borders = new int[meansCount - 1];
            for (int i = 0; i < borders.Length; i++)
                borders[i] = 0;
            return borders;
        }

        public int MoveBorders(int[] borders, double[] means)
        {
            int changed = 0;

            return changed;
        }

        public void Cluster(DTMCFRDataToAnalysis[] data, double[] means)
        {
            NormalizeData(data);

            int[] borders = InitializeBorders(means.Length);
        }
    }
}
