namespace Kata.Web.Toolkit.Functional;

public class Either<TOk, TProblem>
{
    private readonly TOk? _ok;
    private readonly TProblem? _problem;
    private readonly bool _isOk;

    private Either(TOk ok)
    {
        _ok = ok;
        _problem = default;
        _isOk = true;
    }

    private Either(TProblem problem)
    {
        _ok = default;
        _problem = problem;
        _isOk = false;
    }

    public static Either<TOk, TProblem> Ok(TOk ok) => new(ok);
    public static Either<TOk, TProblem> Problem(TProblem problem) => new(problem);
    public static implicit operator Either<TOk, TProblem>(TOk ok) => Ok(ok);
    public static implicit operator Either<TOk, TProblem>(TProblem problem) => Problem(problem);


    public R Match<R>(Func<TOk, R> ok, Func<TProblem, R> problem) => _isOk ? ok(_ok!) : problem(_problem!);


    public Either<R, TProblem> Map<R>(Func<TOk, R> mapper) =>
        Match(
            ok => Either<R, TProblem>.Ok(mapper(ok!)),
            Either<R, TProblem>.Problem
        );


    public Either<R, TProblem> Bind<R>(Func<TOk, Either<R, TProblem>> binder) =>
        Match(
            ok => binder(ok!),
            Either<R, TProblem>.Problem
        );


    public override string ToString() =>
        Match(ok => $"{ok}", problem => $"{problem}");


}
