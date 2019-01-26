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

        public int MoveBorders(DTMCFRDataToAnalysis[] data, int[] borders, double[] means)
        {
            int changed = 0;

            int[] newborders = new int[borders.Length];
            int bix = 0;
            newborders[bix] = 0;
            int currmean = 0;
            for (int i = 1; i < data.Length; i++)
            {
                newborders[bix] = i;
                if (Math.Abs(data[i].NormalizedLength - means[currmean]) > Math.Abs(data[i].NormalizedLength - means[currmean + 1]))
                {
                    if (newborders[bix] != borders[bix]) changed++;
                    currmean++;
                    bix++;
                }
                if (bix == newborders.Length) break;
            }

            newborders.CopyTo(borders, 0);
            return changed;
        }

        public void CalculateMeans(DTMCFRDataToAnalysis[] data, int[] borders, double[] means)
        {
            means[0] = 0;
            for (int i = 0; i < borders[0]; i++)
                means[0] += data[i].NormalizedLength;
            means[0] /= borders[0];

            for (int j = 1; j < borders.Length - 1; j++)
            {

            }

            means[means.Length - 1] = 0;
            for (int i = borders[borders.Length - 1]; i < data.Length; i++)
                means[means.Length - 1] = data[i].NormalizedLength;
            means[means.Length - 1] /= data.Length - borders[borders.Length - 1];
        }

        public int[] Cluster(DTMCFRDataToAnalysis[] data, double[] means)
        {
            NormalizeData(data);

            int[] borders = InitializeBorders(means.Length);
            while (MoveBorders(data, borders, means) > 0)
                CalculateMeans(data, borders, means);
            return borders;
        }
    }
}
