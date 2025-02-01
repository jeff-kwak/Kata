using Kata.Web.Toolkit.Functional;
using Marten;

namespace Kata.Web.Auth.UseCases;

using QueryAllUsersResult = Either<QueryAllUsersSuccess, QueryAllUsersProblem>;

public record QueryAllUsersRequest;
public record QueryAllUsersProblem;

public record QueryAllUsersSuccess(IEnumerable<AuthUser> AuthUsers);

// TODO: experiment with IAsyncEnumerable
public delegate Task<QueryAllUsersResult> QueryAllUsersHandler(FetchAllAuthUsersHandler fetch);


public static partial class Query
{
    public static QueryAllUsersHandler AllUsers =>
        async (fetch) =>
            new QueryAllUsersSuccess(await fetch());
}


public delegate Task<IEnumerable<AuthUser>> FetchAllAuthUsersHandler();

public sealed partial class MartenDatabase
{
    public async Task<IEnumerable<AuthUser>> FetchAllAuthUsers()
    {
        await using var session = _store.QuerySession();
        var allOfThem = await session.Query<AuthUser>().ToListAsync();
        return allOfThem;
    }
}
