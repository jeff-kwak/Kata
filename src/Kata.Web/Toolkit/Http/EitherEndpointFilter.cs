using Kata.Web.Toolkit.Functional;

namespace Kata.Web.Toolkit.Http;

public class EitherEndpointFilter<T,E>: IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next(context);

        if (result is Either<T, E> either)
        {
            return either.Match(
                ok => Results.Ok(ok),
                problem => Results.BadRequest(problem)
            );
        }

        return result;
    }
}
