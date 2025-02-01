using Kata.Web.Toolkit.Functional;
using Marten;

namespace Kata.Web.Auth.UseCases;

using CreateUserResult = Either<CreateUserSuccess, CreateUserProblem>;
using SaveUserResult = Either<SaveUserSuccess, SaveUserProblem>;

public record CreateUserProblem;
public record CreateUserSuccess(string Id);

public record CreateUserRequest(string Username);

public delegate Task<CreateUserResult> CreateUserHandler(SaveUserHandler save, CreateUserRequest request);

public static class Command
{
    public static CreateUserHandler CreateUser =>
        async (save, request) =>
        {
            var id = Guid.NewGuid().ToString();
            var result = await save(id, request.Username);
            return new CreateUserSuccess(Guid.NewGuid().ToString());
        };
}


public record SaveUserSuccess;
public record SaveUserProblem;
public record UsernameNotUnique: SaveUserProblem;

public delegate Task<SaveUserResult> SaveUserHandler(string Id, string Username);

public sealed partial class MartenDatabase(IDocumentStore Store)
{
    private readonly IDocumentStore _store = Store; 

    public async Task<SaveUserResult> SaveUser(string id, string username)
    {
        await using var session = _store.LightweightSession();
        var user = new AuthUser(id, username);
        session.Store(user);
        await session.SaveChangesAsync();
        return new SaveUserSuccess();
    }
}