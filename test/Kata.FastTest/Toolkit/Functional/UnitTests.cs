using static Kata.Web.Toolkit.Functional.Unit;

namespace Kata.FastTest.Toolkit.Functional;

public class UnitTests
{
    [Test]
    public void UnitFunctionReturnsSingletonUnit()
    {
        var unit = U();
        Assert.That(unit, Is.SameAs(U()));
    }

    [Test]
    public void UnitFunctionForActionWithOneParamReturnsSingletonUnit()
    {
        var unit = U(() => { });
        Assert.That(unit, Is.SameAs(U()));
    }
}
