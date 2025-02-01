using Kata.Web.Toolkit.Functional;

namespace Kata.Web.Auth.UseCases;

using QueryAllUsersResult = Either<QueryAllUsersSuccess, QueryAllUsersProblem>;

public delegate Task<QueryAllUsersResult> QueryAllUsersHandler(FetchAllAuthUsersHandler fetch);
public delegate Task<IEnumerable<AuthUser>> FetchAllAuthUsersHandler(); // TODO: experiment with IAsyncEnumerable

public record QueryAllUsersRequest;
public record QueryAllUsersProblem;
public record QueryAllUsersSuccess(IEnumerable<AuthUser> AuthUsers);

public static partial class Query
{
    public static QueryAllUsersHandler AllUsers =>
        async (fetch) =>
            new QueryAllUsersSuccess(await fetch());
}

