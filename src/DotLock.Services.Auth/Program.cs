using Microsoft.EntityFrameworkCore;

using DotLock.Services.Auth.Data;
using DotLock.Services.Auth.Models;
using DotLock.Services.Auth.Services;

using DotLock.Shared.Contracts.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddDbContext<AuthDbContext>(o => 
    o.UseNpgsql(builder.Configuration.GetConnectionString("auth-db")));

builder.Services.AddSingleton<JwtService>();
builder.Services.AddSingleton<OpaqueService>();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapPost("/register/start", async (RegisterStartRequest request, OpaqueService opaque, AuthDbContext db) =>
{

});

app.MapPost("/register/finish", async (RegisterFinishRequest request, AuthDbContext db) =>
{

});

app.MapPost("/login/start", async (LoginStartRequest request, OpaqueService opaque) =>
{

});

app.MapPost("/login/finish", async (LoginFinishRequest request, OpaqueService opaque, JwtService jwt, AuthDbContext db) =>
{

});

app.MapGet("/.well-known/jwks.json", (JwtService jwt) =>
{
    return Results.Json(jwt.GetJwks());
});

app.Run();
