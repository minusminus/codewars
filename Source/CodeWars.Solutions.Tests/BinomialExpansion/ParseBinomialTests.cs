using System;
using NUnit.Framework;
using Shouldly;

namespace NBE
{
    [TestFixture]
    public class ParseBinomialTests
    {
        private readonly BE.BE _testObj = new BE.BE();

        private void CheckBinomialParse(string btxt, Int64 a, Int64 b, Int64 n, char x, string info)
        {
            BE.BinomialData res = _testObj.ParseBinomial(btxt);
            res.a.ShouldBe(a, info);
            res.b.ShouldBe(b, info);
            res.n.ShouldBe(n, info);
            res.x.ShouldBe(x, info);
        }

        [Test]
        public void NoAPlus()
        {
            CheckBinomialParse("(x+1)^0", 1, 1, 0, 'x', "1");
            CheckBinomialParse("(x+1)^1", 1, 1, 1, 'x', "2");
            CheckBinomialParse("(y+23)^15", 1, 23, 15, 'y', "3");
            CheckBinomialParse("(-x+1)^2", -1, 1, 2, 'x', "4");
            CheckBinomialParse("(x+0)^7", 1, 0, 7, 'x', "5");
        }

        [Test]
        public void NoAMinus()
        {
            CheckBinomialParse("(x-1)^0", 1, -1, 0, 'x', "1");
            CheckBinomialParse("(x-1)^1", 1, -1, 1, 'x', "2");
            CheckBinomialParse("(y-23)^15", 1, -23, 15, 'y', "3");
            CheckBinomialParse("(-x-1)^2", -1, -1, 2, 'x', "4");
            CheckBinomialParse("(x-0)^7", 1, 0, 7, 'x', "5");
        }

        [Test]
        public void APlus()
        {
            CheckBinomialParse("(1x+1)^0", 1, 1, 0, 'x', "1");
            CheckBinomialParse("(3x+1)^1", 3, 1, 1, 'x', "2");
            CheckBinomialParse("(4y+23)^15", 4, 23, 15, 'y', "3");
            CheckBinomialParse("(-5x+1)^2", -5, 1, 2, 'x', "4");
            CheckBinomialParse("(6x+0)^7", 6, 0, 7, 'x', "5");
        }

        [Test]
        public void AMinus()
        {
            CheckBinomialParse("(1x-1)^0", 1, -1, 0, 'x', "1");
            CheckBinomialParse("(3x-1)^1", 3, -1, 1, 'x', "2");
            CheckBinomialParse("(4y-23)^15", 4, -23, 15, 'y', "3");
            CheckBinomialParse("(-5x-1)^2", -5, -1, 2, 'x', "4");
            CheckBinomialParse("(6x-0)^7", 6, 0, 7, 'x', "5");
        }
    }
}
