using Marten;
using Weasel.Core;
using Kata.Web.Auth.UseCases;
using Kata.Web.Toolkit.Http;

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

builder.Services.AddSingleton<MartenDatabase>();
builder.Services.AddScoped<SaveUserHandler>(sp => sp.GetRequiredService<MartenDatabase>().SaveUser);
builder.Services.AddScoped<FetchAllAuthUsersHandler>(sp => sp.GetRequiredService<MartenDatabase>().FetchAllAuthUsers);

var app = builder.Build();

// TODO: handle route registration somehow in the use case.
app.MapPost("/auth/users", Command.CreateUser).AddEndpointFilter<EitherEndpointFilter<CreateUserSuccess, CreateUserProblem>>();
app.MapGet("/auth/users", Query.AllUsers).AddEndpointFilter<EitherEndpointFilter<QueryAllUsersSuccess, QueryAllUsersProblem>>();

app.Run();
