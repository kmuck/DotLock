using Microsoft.EntityFrameworkCore;


using DotLock.Services.Auth.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var connectionString = builder.Configuration.GetConnectionString("auth-db");
builder.Services.AddDbContext<AuthDbContext>(o => o.UseNpgsql(connectionString));

var app = builder.Build();



app.MapGet("ping", () => "pong");


app.Run();
