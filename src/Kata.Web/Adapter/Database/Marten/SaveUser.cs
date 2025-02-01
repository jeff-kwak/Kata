using Kata.Web.Auth;
using Kata.Web.Auth.UseCases;
using Kata.Web.Toolkit.Functional;

namespace Kata.Web.Adapter.Database.Marten;

using SaveUserResult = Either<SaveUserSuccess, SaveUserProblem>;

public sealed partial class Database
{
    public async Task<SaveUserResult> SaveUser(string id, string username)
    {
        await using var session = _store.LightweightSession();
        var user = new AuthUser(id, username);
        session.Store(user);
        await session.SaveChangesAsync();
        return new SaveUserSuccess();
    }
}
