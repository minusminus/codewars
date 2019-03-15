using R13;
using NUnit.Framework;
using Shouldly;

namespace N_R13
{
    [TestFixture]
    public class Rot13Tests
    {
        private Rot13 TestObj = new Rot13();

        [Test]
        public void DecodeBasicTest()
        {
            TestObj.Decode("EBG13 rknzcyr.").ShouldBe("ROT13 example.");
            TestObj.Decode("Guvf vf zl svefg EBG13 rkprepvfr!").ShouldBe("This is my first ROT13 excercise!");
        }

        [Test]
        public void EncodeBasicTest()
        {
            TestObj.Encode("test").ShouldBe("grfg");
            TestObj.Encode("Test").ShouldBe("Grfg");
        }
    }
}
