using Microsoft.EntityFrameworkCore;
using DotLock.Services.Auth.Models;

namespace DotLock.Services.Auth.Data;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<LoginState> LoginStates => Set<LoginState>();
}
