namespace DotLock.Shared.Contracts.Identity;

public record LoginRequest(
    string Email,
    string Password
);