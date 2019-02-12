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
    public class DTMCFRClustering2
    {
        public int AssignToClusters(DTMCFRDataToAnalysis[] data, double[] means)
        {
            int changed = 0;

            int[] meansix = means.Select((mean, i) => new {mean, i})
                .Where(x => x.mean > -1)
                .Select(x => x.i)
                .ToArray();

            int currmean = 0;
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = currmean; j < meansix.Length - 1; j++)
                {
                    if (Math.Abs(data[i].NormalizedLength - means[meansix[currmean]]) <= Math.Abs(data[i].NormalizedLength - means[meansix[currmean + 1]])) break;
                    currmean++;
                }
                if (data[i].Cluster != meansix[currmean]) changed++;
                data[i].Cluster = meansix[currmean];
            }

            return changed;
        }

        public void CalculateMeans(DTMCFRDataToAnalysis[] data, double[] means)
        {
            for (int i = 0; i < means.Length; i++) means[i] = -1;
            data.GroupBy(x => new {ID = x.Cluster})
                .Select(g => new {ID = g.Key.ID, Avg = g.Average(p => p.NormalizedLength)})
                .ToList()
                .ForEach(x => means[x.ID] = x.Avg);
        }

        public void Cluster(DTMCFRDataToAnalysis[] data, double[] means)
        {
            while (AssignToClusters(data, means) > 0)
                CalculateMeans(data, means);
        }
    }
}
