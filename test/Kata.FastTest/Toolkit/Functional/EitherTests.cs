using Kata.Web.Toolkit.Functional;
using static Kata.Web.Toolkit.Functional.Unit;

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

    public record EitherTestProblem;

    [Test]
    public void MapOverOkValuesIsOk()
    {
        var either = Either<string, EitherTestProblem>.Ok("ok");
        var result = either.Map(ok => ok.Length);

        result.Match(
            ok=> U(() => Assert.That(ok, Is.EqualTo(2), "mapped correctly over ok value")),
            _ => U(() => Assert.Fail("Expected ok, got problem"))
        );
    }

    [Test]
    public void MapOverProblemValuesIsProblem()
    {

        var problem = new EitherTestProblem();
        var either = Either<string, EitherTestProblem>.Problem(problem);
        var result = either.Map(ok => ok.Length);

        result.Match(
            _ => U(() => Assert.Fail("Expected problem, got ok")),
            p => U(() => Assert.That(p, Is.SameAs(problem), "problem is the same"))
        );
    }

    [Test]
    public void SelectOverOkIsSameAsMapOverOk()
    {
        var either = Either<string, EitherTestProblem>.Ok("ok");
        var result = either.Select(ok => ok.Length);

        result.Match(
            ok => U(() => Assert.That(ok, Is.EqualTo(2), "mapped correctly over ok value")),
            _ => U(() => Assert.Fail("Expected ok, got problem"))
        );
    }


    [Test]
    public void EitherSupportsSelectInLinqComprehension()
    {
        var result = from ok in Either<string, EitherTestProblem>.Ok("ok")
                     select ok.Length;

        result.Match(
            ok => U(() => Assert.That(ok, Is.EqualTo(2), "mapped correctly over ok value")),
            _ => U(() => Assert.Fail("Expected ok, got problem"))
        );
    }


    [Test]
    public void EitherSupportsWhereInLinqComprehension()
    {
        var truePredicate = from ok in Either<string, EitherTestProblem>.Ok("ok")
                     where ok.Length == 2
                     select ok;

        truePredicate.Match(
            ok => U(() => Assert.That(ok, Is.EqualTo("ok"), "ok value is the same")),
            _ => U(() => Assert.Fail("Expected ok, got problem"))
        );

        var falsePredicate = from ok in Either<string, EitherTestProblem>.Ok("ok")
                             where ok.Length == 3
                             select ok;

        falsePredicate.Match(
            _ => U(() => Assert.Fail("Expected problem, got ok")),
            p => U(() => Assert.Pass("Expected problem when predicate is false"))
        );
    }


    [Test]
    public void EitherSupportsSelectManyInLinqComprehension()
    {
        var okResult = from ok in Either<string, EitherTestProblem>.Ok("ok")
                     from ok2 in Either<int, EitherTestProblem>.Ok(40)
                     select ok.Length + ok2;

        okResult.Match(
            ok => U(() => Assert.That(ok, Is.EqualTo(42), "mapped correctly over ok value")),
            _ => U(() => Assert.Fail("Expected ok, got problem"))
        );

        var probResult = from ok in Either<string, EitherTestProblem>.Ok("ok")
                     from ok2 in Either<int, EitherTestProblem>.Problem(new EitherTestProblem())
                     select ok.Length + ok2;

        probResult.Match(
            _ => U(() => Assert.Fail("Expected problem, got ok")),
            p => U(() => Assert.Pass("Expected problem outcome"))
        );
    }

}
