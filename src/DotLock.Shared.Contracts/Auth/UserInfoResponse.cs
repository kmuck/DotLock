namespace DotLock.Shared.Contracts.Identity;

public record UserInfoResponse(
    string UserId,
    string Email
);