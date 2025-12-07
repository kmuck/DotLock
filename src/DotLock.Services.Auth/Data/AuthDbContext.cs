using Microsoft.EntityFrameworkCore;
using DotLock.Services.Auth.Models;

namespace DotLock.Services.Auth.Data;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    // public DbSet<Session> Sessions => Set<Session>(); // si tu stockes les sessions en DB
}
