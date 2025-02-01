using Marten;
using Weasel.Core;
using Kata.Web.Auth.UseCases;
using Kata.Web.Toolkit.Http;
using Kata.Web.Adapter.Database.Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMarten(opts => {
    // TODO: understand the new .net configuration better so this is not so clunky
    opts.Connection("host=localhost;database=kata;password=example;username=postgres");
    opts.UseSystemTextJsonForSerialization();
    
    if(builder.Environment.IsDevelopment())
    {
        opts.AutoCreateSchemaObjects = AutoCreate.All;
    }
});

builder.Services.AddSingleton<Database>();
builder.Services.AddScoped<SaveUserHandler>(sp => sp.GetRequiredService<Database>().SaveUser);
builder.Services.AddScoped<FetchAllAuthUsersHandler>(sp => sp.GetRequiredService<Database>().FetchAllAuthUsers);

var app = builder.Build();

// TODO: handle route registration somehow in the use case.
app.MapPost("/auth/users", Command.CreateUser).AddEndpointFilter<EitherEndpointFilter<CreateUserSuccess, CreateUserProblem>>();
app.MapGet("/auth/users", Query.AllUsers).AddEndpointFilter<EitherEndpointFilter<QueryAllUsersSuccess, QueryAllUsersProblem>>();

app.Run();
