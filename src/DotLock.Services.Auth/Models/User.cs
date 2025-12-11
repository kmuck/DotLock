namespace DotLock.Services.Auth.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public string RegistrationRecord { get; set; } = default!;
    public string? Roles { get; set; }  // ex: "User,Admin"
}