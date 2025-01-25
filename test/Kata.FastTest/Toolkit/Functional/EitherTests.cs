using Kata.Web.Toolkit.Functional;

namespace Kata.FastTest.Toolkit.Functional;

public class EitherTests
{
    [Test]
    public void MatchRunsOkArmWhenOk()
    {
        var either = Either<string, int>.Ok("ok");
        var result = either.Match(ok => ok, problem => "this is a problem");
        Assert.That(result, Is.EqualTo("ok"));
    }

    [Test]
    public void MatchRunsProblemArmWhenProblem()
    {
        var either = Either<string, int>.Problem(42);
        var result = either.Match(ok => ok, problem => "this is a problem");
        Assert.That(result, Is.EqualTo("this is a problem"));
    }

    [Test]
    public void ImplicitlyConvertOkToEither()
    {
        Either<string, int> either = "ok";
        var result = either.Match(ok => ok, problem => "this is a problem");
        Assert.That(result, Is.EqualTo("ok"));
    }

    [Test]
    public void ImplicitlyConvertProblemToEither()
    {
        Either<string, int> either = 42;
        var result = either.Match(ok => ok, problem => "this is a problem");
        Assert.That(result, Is.EqualTo("this is a problem"));
    }

}
