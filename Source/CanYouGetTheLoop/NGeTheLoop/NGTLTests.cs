using System;
using GetTheLoop;
using NUnit.Framework;
using Shouldly;

namespace NGeTheLoop
{
    [TestFixture]
    public class NGTLTests
    {
        private void TestSingleChain(int taillength, int looplength)
        {
            LDNode n, n2;
            n = LoopDetector.createChain(taillength, looplength);
            for (int i = 0; i < taillength; i++)
            {
                //Console.WriteLine($"tail {i}: {n.ID}");
                n = n.next;
            }
            n2 = n;
            for (int i = 0; i < looplength - 1; i++)
            {
                //Console.WriteLine($"loop {i}: {n2.next.ID}");
                n2 = n2.next;
            }
            //n2.next.ID.ShouldBe(n.ID);
            n2.ShouldBe(n);
        }

        [Test]
        public void TestChainGen()
        {
            TestSingleChain(3, 4);
            TestSingleChain(30, 400);
            TestSingleChain(1, 3);
            TestSingleChain(3904, 1087);
        }

        private GetTheLoop.GTL _pobj = new GetTheLoop.GTL();

        [Test]
        public void KataTests()
        {
            LDNode n;
            n = LoopDetector.createChain(1, 3);
            _pobj.getLoopSize(n).ShouldBe(3);
            n = LoopDetector.createChain(3, 30);
            _pobj.getLoopSize(n).ShouldBe(30);
            n = LoopDetector.createChain(3904, 1087);
            _pobj.getLoopSize(n).ShouldBe(1087);
        }
    }
}