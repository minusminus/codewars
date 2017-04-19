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
            //_pobj.Part(2).ShouldBe("Range: 1 Average: 1.50 Median: 1.50");
            //_pobj.Part(3).ShouldBe("Range: 2 Average: 2.00 Median: 2.00");
            //_pobj.Part(4).ShouldBe("Range: 3 Average: 2.50 Median: 2.50");
            _pobj.Part(5).ShouldBe("Range: 5 Average: 3.50 Median: 3.50");
        }
    }
}