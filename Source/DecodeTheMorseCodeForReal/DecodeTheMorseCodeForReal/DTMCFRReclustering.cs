using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecodeTheMorseCodeForReal
{
    /// <summary>
    /// Mechanizm dopasowujacy znalezione klastry
    /// </summary>
    public class DTMCFRReclustering
    {
        public void Recluster(DTMCFRDataToAnalysis[] arr0, double[] means0, DTMCFRDataToAnalysis[] arr1, double[] means1)
        {
            EmptyCluster1For0(arr0, means0);
            ChangeClusters0BasedOn1(arr0, arr1);
        }

        private void EmptyCluster1For0(DTMCFRDataToAnalysis[] arr0, double[] means0)
        {
            //sa przerwy miedzy elementami i miedzy slowami, nie ma miedzy znakami
            if ((means0[1] == -1) && (means0[0] != -1) && (means0[2] != -1))
            {
                for (int i = 0; i < arr0.Length; i++)
                    if (arr0[i].Cluster == 2)
                        arr0[i].Cluster = 1;
                means0[1] = means0[2];
                means0[2] = -1;
            }
        }

        private int GetMaxBorderForCluster(DTMCFRDataToAnalysis[] arr, int cluster)
        {
            var filtered = arr.Where(x => x.Cluster == cluster);
            if(!filtered.Any()) return -1;
            return filtered.Max(x => x.Length);
        }

        private void ChangeClusters0BasedOn1(DTMCFRDataToAnalysis[] arr0, DTMCFRDataToAnalysis[] arr1)
        {
            //klastry 0 i 1 dla zer powinny zgadzac sie z jedynkami (zgodnie ze specyfikacja maja dlugosci 1 i 3 jednostki)
            int oneBorder0 = GetMaxBorderForCluster(arr1, 0);
            int oneBorder1 = GetMaxBorderForCluster(arr1, 1);

            for (int i = 0; i < arr0.Length; i++)
            {
                if ((oneBorder0 > -1) && (arr0[i].Cluster > 0) && (arr0[i].Length <= oneBorder0)) arr0[i].Cluster = 0;
                else if ((oneBorder1 > -1) && (arr0[i].Cluster > 1) && (arr0[i].Length <= oneBorder1)) arr0[i].Cluster = 1;
            }
        }
    }
}
