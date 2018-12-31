using System;
using NUnit.Framework;
using Shouldly;
using SqIntoSq;

namespace NSqIntoSq
{
    [TestFixture]
    public class NSIS
    {
        private readonly SIS _testObj = new SIS();

        [Test]
        public void TestSampleKata()
        {
            _testObj.Decompose(11).ShouldBe("1 2 4 10");
        }
    }
}