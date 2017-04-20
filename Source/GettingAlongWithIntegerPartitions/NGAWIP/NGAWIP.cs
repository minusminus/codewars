using System;
using NUnit.Framework;
using Shouldly;

namespace NGAWIP
{
    [TestFixture]
    public class NGAWIP
    {
        private GAWIP.GAWIP _pobj = new GAWIP.GAWIP();

        [Test]
        public void BasicKataTests()
        {
            _pobj.Part(2).ShouldBe("Range: 1 Average: 1.50 Median: 1.50");
            _pobj.Part(3).ShouldBe("Range: 2 Average: 2.00 Median: 2.00");
            _pobj.Part(4).ShouldBe("Range: 3 Average: 2.50 Median: 2.50");
            _pobj.Part(5).ShouldBe("Range: 5 Average: 3.50 Median: 3.50");
        }

        [Test]
        public void Test1()
        {
            _pobj.Part(1).ShouldBe("Range: 0 Average: 1.00 Median: 1.00");
        }

        [Test]
        public void Test_6_7()
        {
            _pobj.Part(6).ShouldBe("Range: 7 Average: 4.14 Median: 4.00");
            _pobj.Part(7).ShouldBe("Range: 11 Average: 5.80 Median: 5.50");
        }

        [Test]
        public void Test8()
        {
            //powinno byc:
            //prod(8) -> [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 15, 16, 18]
            //sum=116, cnt=14, max=18
            _pobj.Part(8).ShouldBe("Range: 17 Average: 8.28 Median: 7.50");
        }

        [Test]
        public void Test10()
        {
            _pobj.Part(10).ShouldBe("Range: 47 Average: 15.95 Median: 11.50");
        }

        [Test]
        public void Test50()
        {
            _pobj.Part(50).ShouldBe("Range: 593711 Average: 35074.03 Median: 1628.50");
        }
    }
}