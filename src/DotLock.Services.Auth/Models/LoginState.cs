namespace DotLock.Services.Auth.Models;

public class LoginState
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string ServerLoginState { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }

    public User User { get; set; } = default!;
}