using Kata.Web.Auth;
using Marten;

namespace Kata.Web.Adapter.Database.Marten;

public partial class Database
{
    public async Task<IEnumerable<AuthUser>> FetchAllAuthUsers()
    {
        await using var session = _store.QuerySession();
        var allOfThem = await session.Query<AuthUser>().ToListAsync();
        return allOfThem;
    }
}
