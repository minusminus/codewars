using CodeWars.Solutions2.Tools;

namespace CodeWars.Solutions2.Tests.Tools;

internal class ModuloTests
{
    [TestCase(10, 2, 0)]
    [TestCase(10, 20, 10)]
    [TestCase(-10, 2, 0)]
    [TestCase(-10, 20, 10)]
    public void Mod_Int__ReturnsCorrectly(int a, int b, int expected)
    {
        a.Mod(b).ShouldBe(expected);
    }

    [TestCase(10, 2, 0)]
    [TestCase(10, 20, 10)]
    [TestCase(-10, 2, 0)]
    [TestCase(-10, 20, 10)]
    public void Mod_Long__ReturnsCorrectly(long a, long b, long expected)
    {
        a.Mod(b).ShouldBe(expected);
    }
}
