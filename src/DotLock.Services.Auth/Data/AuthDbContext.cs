using Microsoft.EntityFrameworkCore;
using DotLock.Services.Auth.Models;

namespace DotLock.Services.Auth.Data;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<LoginState> LoginStates => Set<LoginState>();
}
