using DecodeTheMorseCodeForReal;
using NUnit.Framework;
using Shouldly;

namespace NDecodeTheMorseCodeForReal
{
    [TestFixture]
    public class NDTMCFRClustering
    {
        private readonly DTMCFRClustering _testObj = new DTMCFRClustering();

        private void CheckMoveBorders(DTMCFRDataToAnalysis[] data, int[] borders, double[] means, int[] expectedborders, int expectedchange)
        {
            _testObj.MoveBorders(data, borders, means).ShouldBe(expectedchange);
            for (int i = 0; i < borders.Length; i++)
                borders[i].ShouldBe(expectedborders[i]);
        }

        [Test]
        public void MoveBordersTest()
        {
            DTMCFRDataToAnalysis[] data = new DTMCFRDataToAnalysis[]
            {
                new DTMCFRDataToAnalysis() {Length = 1, NormalizedLength = 0},
                new DTMCFRDataToAnalysis() {Length = 2, NormalizedLength = 0.25},
                new DTMCFRDataToAnalysis() {Length = 3, NormalizedLength = 0.5},
                new DTMCFRDataToAnalysis() {Length = 4, NormalizedLength = 0.75},
                new DTMCFRDataToAnalysis() {Length = 5, NormalizedLength = 1}
            };  //normalized: 0, 0.25, 0.5, 0.75, 1

            CheckMoveBorders(data, new int[2] { 0, 0 }, new double[3] { 0.25, 0.5, 0.75 }, new int[2] { 2, 3 }, 2);
            CheckMoveBorders(data, new int[2] { 2, 3 }, new double[3] { 0.25, 0.5, 0.75 }, new int[2] { 2, 3 }, 0);
        }

        private void CheckCalculateMeans(DTMCFRDataToAnalysis[] data, int[] borders, double[] expectedmeans)
        {
            double[] means = new double[expectedmeans.Length];
            _testObj.CalculateMeans(data, borders, means);
            for (int i = 0; i < means.Length; i++)
                means[i].ShouldBe(expectedmeans[i]);
        }

        [Test]
        public void CalculateMeansTest()
        {
            DTMCFRDataToAnalysis[] data = new DTMCFRDataToAnalysis[]
            {
                new DTMCFRDataToAnalysis() {Length = 1, NormalizedLength = 0},
                new DTMCFRDataToAnalysis() {Length = 2, NormalizedLength = 0.25},
                new DTMCFRDataToAnalysis() {Length = 3, NormalizedLength = 0.5},
                new DTMCFRDataToAnalysis() {Length = 4, NormalizedLength = 0.75},
                new DTMCFRDataToAnalysis() {Length = 5, NormalizedLength = 1}
            };  //normalized: 0, 0.25, 0.5, 0.75, 1

            CheckCalculateMeans(data, new int[2] {2, 3}, new double[3] {0.125, 0.5, 0.875});  //indeksy: 0+1, 2, 3+4
            CheckCalculateMeans(data, new int[1] { 2 }, new double[2] { 0.125, 0.75 });  //indeksy: 0+1, 2+3+4
        }

        [Test]
        public void ClusterTest()
        {
            DTMCFRDataToAnalysis[] data = new DTMCFRDataToAnalysis[]
            {
                new DTMCFRDataToAnalysis() {Length = 1, NormalizedLength = 0},
                new DTMCFRDataToAnalysis() {Length = 2, NormalizedLength = 0.25},
                new DTMCFRDataToAnalysis() {Length = 3, NormalizedLength = 0.5},
                new DTMCFRDataToAnalysis() {Length = 4, NormalizedLength = 0.75},
                new DTMCFRDataToAnalysis() {Length = 5, NormalizedLength = 1}
            };  //normalized: 0, 0.25, 0.5, 0.75, 1

            int[] borders = _testObj.Cluster(data, new double[3] {0.2, 0.6, 0.9});
            borders.Length.ShouldBe(2);
            borders[0].ShouldBe(2);
            borders[1].ShouldBe(4);
        }
    }
}
