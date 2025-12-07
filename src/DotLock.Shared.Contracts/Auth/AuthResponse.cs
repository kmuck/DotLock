namespace DotLock.Shared.Contracts.Identity;

public record AuthResponse(
    string UserId,
    string Email,
    string Token,
    DateTime ExpiresAt
);