using R13;
using NUnit.Framework;
using Shouldly;

namespace N_R13
{
    [TestFixture]
    public class RotWithShiftTests
    {
        private RotWithShift TestObj = new RotWithShift();

        [Test]
        public void BasicKataTests()
        {
            TestObj.Decypher("guvf vf n grfg", 13).ShouldBe("this is a test");
            TestObj.Decypher("aopz pz h zbwly zljyla jvkl", 19).ShouldBe("this is a super secret code");
            TestObj.Decypher("bfyhm tzy, ymnx tsj mfx uzshyzfynts.", 21).ShouldBe("watch out, this one has punctuation.");
        }
    }
}
