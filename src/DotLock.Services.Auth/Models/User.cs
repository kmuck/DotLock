namespace DotLock.Services.Auth.Models;

public class User
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string RegistrationRecord { get; set; }
    public string? Roles { get; set; }
}