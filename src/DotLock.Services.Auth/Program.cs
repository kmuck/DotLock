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
    bool exists = await db.Users.AnyAsync(u => u.Username == request.Username);
    if (exists)
        return Results.Conflict("User already exists");

    return Results.Ok(new RegisterStartResponse(opaque.CreateRegistrationResponse(request.Username, request.RegistrationRequest)));
});

app.MapPost("/register/finish", async (RegisterFinishRequest request, AuthDbContext db) =>
{
    bool exists = await db.Users.AnyAsync(u => u.Username == request.Username);
    if (exists)
        return Results.Conflict("User already exists");

    // Could check the registration record here.. 
    // .. but not required by the OPAQUE protocol

    db.Users.Add(new User {
        Username = request.Username,
        RegistrationRecord = request.RegistrationRecord
    });

    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapPost("/login/start", async (LoginStartRequest request, OpaqueService opaque, AuthDbContext db) =>
{
    var user = await db.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
    if (user is null)
        return Results.Unauthorized();

    var start = opaque.StartLogin(request.Username, request.StartLoginRequest, user.RegistrationRecord);

    var loginState = new LoginState
    {
        Id = Guid.NewGuid(),
        UserId = user.Id,
        ServerLoginState = start.ServerLoginState,
        ExpiresAt = DateTime.UtcNow.AddMinutes(5)
    };

    db.LoginStates.Add(loginState);
    await db.SaveChangesAsync();

    return Results.Ok(new LoginStartResponse(start.LoginResponse, loginState.Id));
});

app.MapPost("/login/finish", async (LoginFinishRequest request, OpaqueService opaque, JwtService jwt, AuthDbContext db) =>
{
    var loginState = await db.LoginStates
        .Include(ls => ls.User)
        .SingleOrDefaultAsync(ls =>
            ls.Id == request.LoginStateId &&
            ls.ExpiresAt > DateTime.UtcNow &&
            ls.User.Username == request.Username);

    if (loginState is null)
        return Results.Unauthorized();

    try { 
        opaque.FinishLogin(loginState.ServerLoginState, request.FinishLoginRequest); 
    }
    catch { 
        return Results.Unauthorized(); 
    }

    db.LoginStates.Remove(loginState);
    await db.SaveChangesAsync();

    var roles = loginState.User.Roles?
        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    var payload = new Dictionary<string, object>
    {
        ["sub"] = loginState.User.Id.ToString(),
        ["name"] = loginState.User.Username,
        ["roles"] = roles ?? Array.Empty<string>(),
        ["iss"] = "auth-service",
        ["aud"] = "api",
        ["iat"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
        ["exp"] = DateTimeOffset.UtcNow.AddMinutes(15).ToUnixTimeSeconds()
    };

    string token = jwt.GenerateJwt(payload);

    return Results.Ok(new LoginFinishResponse(token));
});

app.MapGet("/.well-known/jwks.json", (JwtService jwt) =>
{
    return Results.Json(jwt.GetJwks());
});

app.Run();
