using Microsoft.EntityFrameworkCore;

using DotLock.Services.Auth.Data;

var builder = WebApplication.CreateBuilder(args);

// OpenAPI documentation
builder.Services.AddOpenApi();

// Database configuration
var connectionString = builder.Configuration.GetConnectionString("auth-db");
builder.Services.AddDbContext<AuthDbContext>(o => o.UseNpgsql(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.Run();
