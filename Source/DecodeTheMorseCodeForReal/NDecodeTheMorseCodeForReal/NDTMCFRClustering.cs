using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DecodeTheMorseCodeForReal;
using NUnit.Framework;
using Shouldly;

namespace NDecodeTheMorseCodeForReal
{
    [TestFixture]
    public class NDTMCFRClustering
    {
        private readonly DTMCFRClustering _testObj = new DTMCFRClustering();

        private void CheckNormalizeData(DTMCFRDataToAnalysis[] data, double[] expected)
        {
            _testObj.NormalizeData(data);
            for (int i = 0; i < data.Length; i++)
                data[i].NormalizedLength.ShouldBe(expected[i], $"{i}");
        }

        [Test]
        public void NormalizeDataTest()
        {
            CheckNormalizeData(new DTMCFRDataToAnalysis[]
            {
                new DTMCFRDataToAnalysis() {Length = 1},
                new DTMCFRDataToAnalysis() {Length = 2},
                new DTMCFRDataToAnalysis() {Length = 3}
            },
                new double[] {0, 0.5, 1});

            CheckNormalizeData(new DTMCFRDataToAnalysis[]
            {
                new DTMCFRDataToAnalysis() {Length = 1},
                new DTMCFRDataToAnalysis() {Length = 2},
                new DTMCFRDataToAnalysis() {Length = 5}
            },
                new double[] {0, 0.25, 1});

            CheckNormalizeData(new DTMCFRDataToAnalysis[]
            {
                new DTMCFRDataToAnalysis() {Length = 1},
                new DTMCFRDataToAnalysis() {Length = 2},
                new DTMCFRDataToAnalysis() {Length = 3},
                new DTMCFRDataToAnalysis() {Length = 4},
                new DTMCFRDataToAnalysis() {Length = 5}
            },
                new double[] {0, 0.25, 0.5, 0.75, 1});

            CheckNormalizeData(new DTMCFRDataToAnalysis[]
            {
                new DTMCFRDataToAnalysis() {Length = 1},
                new DTMCFRDataToAnalysis() {Length = 2},
                new DTMCFRDataToAnalysis() {Length = 3},
                new DTMCFRDataToAnalysis() {Length = 4},
                new DTMCFRDataToAnalysis() {Length = 21}
            },
                new double[] {0, 1.0*(1.0/20.0), 2.0*(1.0/20.0), 3.0*(1.0/20.0), 1});
        }

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
                new DTMCFRDataToAnalysis() {Length = 1},
                new DTMCFRDataToAnalysis() {Length = 2},
                new DTMCFRDataToAnalysis() {Length = 3},
                new DTMCFRDataToAnalysis() {Length = 4},
                new DTMCFRDataToAnalysis() {Length = 5}
            };  //normalized: 0, 0.25, 0.5, 0.75, 1
            _testObj.NormalizeData(data);

            CheckMoveBorders(data, new int[2] { 0, 0 }, new double[3] { 0.25, 0.5, 0.75 }, new int[2] { 2, 3 }, 2);
            CheckMoveBorders(data, new int[2] { 2, 3 }, new double[3] { 0.25, 0.5, 0.75 }, new int[2] { 2, 3 }, 0);
        }
    }
}
