using Kata.Web.Toolkit.Functional;

namespace Kata.Web.Auth.UseCases;

using CreateUserResult = Either<CreateUserSuccess, CreateUserProblem>;
using SaveUserResult = Either<SaveUserSuccess, SaveUserProblem>;

public delegate Task<SaveUserResult> SaveUserHandler(string Id, string Username);
public delegate Task<CreateUserResult> CreateUserHandler(SaveUserHandler save, CreateUserRequest request);

public record SaveUserSuccess;
public record CreateUserProblem;
public record CreateUserSuccess(string Id);

public record CreateUserRequest(string Username);
public record SaveUserProblem;
public record UsernameNotUnique: SaveUserProblem;

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


