namespace Kata.Web.Toolkit.Functional;

public static class EitherLinqExtensions
{
    public static Either<R,P> Select<T,R,P>(this Either<T,P> either, Func<T,R> mapper) => either.Map(mapper);
    public static Either<T,P> Where<T,P>(this Either<T,P> either, Func<T,bool> predicate) => either.Bind(ok => predicate(ok) ? Either<T,P>.Ok(ok) : Either<T,P>.Problem(default!));
    public static Either<R,P> SelectMany<T, P, U, R>(this Either<T,P> either, Func<T, Either<U,P>> bind, Func<T, U, R> map) =>
        either.Bind(obj => bind(obj).Map(result => map(obj, result)));
}
