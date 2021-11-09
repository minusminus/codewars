using NUnit.Framework;
using Shouldly;
using CodeWars.Solutions.GetThePlusMethod;

namespace CodeWars.Solutions.Tests.GetThePlusMethod
{
    [TestFixture]
    public class GetThePlusMethodTests
    {
        [Test]
        public void MethodFunc__ReturnsCorrectly([Range(1, 4)] int i)
        {
            const int n = 100;
            CodeWars.Solutions.GetThePlusMethod.GetThePlusMethod.MethodFunc(i)(n).ShouldBe(n + i);
        }

    }
}
