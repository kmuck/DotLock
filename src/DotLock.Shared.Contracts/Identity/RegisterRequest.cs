namespace DotLock.Shared.Contracts.Identity;

public record RegisterRequest(
    string Email,
    string Password
);